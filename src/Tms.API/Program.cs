using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Configurations;
using Tms.Infrastructure;
using Tms.Persistence.Data;
using Tms.Application;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register configuration sections using Options Pattern
builder.Services.Configure<JWTConfiguration>(
    builder.Configuration.GetSection("JWTConfiguration"));

builder.Services.Configure<DbConfiguration>(
    builder.Configuration.GetSection("DbConfiguration"));

// Configure DbContext using deferred service resolution
builder.Services.AddDbContext<TmsDbContext>((serviceProvider, options) =>
{
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DbConfiguration>>().Value;
    options.UseLazyLoadingProxies()
           .UseSqlServer(dbSettings.ConnectionString);
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JWTConfiguration");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TmsDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();