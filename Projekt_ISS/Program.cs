using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projekt_ISS.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("tasktracker.db")));

// JWT Authentication
var key = Encoding.ASCII.GetBytes("YourSuperSecretKey12345");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// 3️⃣ Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll");

// 4️⃣ Enable Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseRouting();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


app.MapControllers();
app.Run();
