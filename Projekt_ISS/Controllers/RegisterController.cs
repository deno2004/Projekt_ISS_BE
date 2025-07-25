﻿using Microsoft.AspNetCore.Mvc;
using Projekt_ISS.Data;
using Projekt_ISS.Models;

namespace Projekt_ISS.Controllers
{
    public class RegisterController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UserRegisterController : ControllerBase
        {
            private readonly TaskTrackerContext _context;

            public UserRegisterController(TaskTrackerContext context)
            {
                _context = context;
            }

            // POST: api/userregister/register
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromForm] UserRegistrationRequest request)
            {
                // Validate user input
                if (request == null ||
                    string.IsNullOrEmpty(request.Email) ||
                    string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("User information is missing.");
                }

                // Normalize email to avoid duplicate entries with different cases
                string normalizedEmail = request.Email.ToLower();

                // Check if the email already exists
                if (_context.Users.Any(u => u.Email.ToLower() == normalizedEmail))
                {
                    return BadRequest("Email already exists.");
                }

                // Handle file upload
                string profileDocumentPath = null;
                if (request.FileUpload != null && request.FileUpload.Length > 0)
                {
                    try
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFilename = $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}_{Path.GetFileName(request.FileUpload.FileName)}";
                        string filePath = Path.Combine(uploadsFolder, uniqueFilename);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.FileUpload.CopyToAsync(stream);
                        }

                        profileDocumentPath = $"/uploads/{uniqueFilename}";
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, "Error uploading file: " + ex.Message);
                    }
                }

                // Wrap in a transaction to ensure atomicity
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Create a new user object
                        var newUser = new User
                        {
                            FirstName = request.FullName,
                            Email = normalizedEmail,
                            Password = request.Password,
                            ProfileDocument = profileDocumentPath
                        };

                        // Add the user to the database
                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();

                        // Commit the transaction
                        await transaction.CommitAsync();

                        return Ok(new { message = "User registered successfully." });
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, "Error creating user: " + ex.Message);
                    }
                }
            }

            // GET: https://localhost:7062/api/userregister/get/1 or 2 or 3
            [HttpGet("get/{id}")]
            public IActionResult GetUser(int id)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }

            // GET: https://localhost:7062/api/userregister/getall
            [HttpGet("getAll")]
            public IActionResult GetAllUsers()
            {
                var users = _context.Users
                    .ToList();

                if (!users.Any())
                {
                    return NotFound("No users found.");
                }

                var result = users.Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Password,
                    user.ProfileDocument
                }).ToList();

                return Ok(result);
            }
        }

        // Request model for user registration
        public class UserRegistrationRequest
        {
            public string? FullName { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public IFormFile? FileUpload { get; set; }
        }
    }
}
