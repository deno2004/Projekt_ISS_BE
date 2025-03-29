using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt_ISS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }

        public string? ProfileDocument { get; set; }
    }
}
