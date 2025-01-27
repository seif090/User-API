using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text;
using User.Application.Mapping;
using User.Application.Services;
using User.Core.Entities;
using User.Core.Interfaces;
using User.Infrastructure.Data;
using User.Infrastructure.Extensions;
using User.Infrastructure.Repositories;
using User.Infrastructure.Services;
using User.Infrastructure.DataSeeding;
class Program { 
public static void Main(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    // Add Infrastructure Layer
    builder.Services.AddInfrastructure(builder.Configuration);

    // Add Application Layer
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddScoped<IUserService, UserService>(); // Register IUserService with UserService
    builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register IUserRepository with UserRepository
    builder.Services.AddScoped<IRoleRepository, RoleRepository>(); // Register IRoleRepository with RoleRepository
    builder.Services.AddScoped<TokenService>(); // Register TokenService

    // Configure JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    builder.Services.AddSingleton(jwtSettings);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

    // Add Authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    });

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Seed Data
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DataSeeder.SeedData(context); // Corrected namespace for DataSeeder
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
}