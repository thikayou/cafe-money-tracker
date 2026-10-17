using CafeTracker.Data;
using CafeTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using CafeTracker.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>();

// Load the private key (PEM text from user-secrets) once.
var privateRsa = RSA.Create();
privateRsa.ImportFromPem(builder.Configuration["Jwt:PrivateKey"]!);
var signingKey = new RsaSecurityKey(privateRsa);

// Derive a public-only key for verification. It cannot sign anything.
var publicRsa = RSA.Create();
publicRsa.ImportRSAPublicKey(privateRsa.ExportRSAPublicKey(), out _);
var validationKey = new RsaSecurityKey(publicRsa);

builder.Services.AddSingleton(signingKey);
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = validationKey,
            ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
        };
    });
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
