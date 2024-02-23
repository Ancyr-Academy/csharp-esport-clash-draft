using System.Text;
using EsportClash.Identity.DbContext;
using EsportClash.Identity.Models;
using EsportClash.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EsportClash.Identity;

public static class IdentityServicesRegistration {
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration) {
    services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

    services.AddDbContext<AppIdentityDbContext>(options => {
      options.UseNpgsql(configuration.GetConnectionString("EsportClashDatabaseConnectionString"));
    });

    services.AddIdentity<AppUser, IdentityRole>()
      .AddEntityFrameworkStores<AppIdentityDbContext>()
      .AddDefaultTokenProviders();

    services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o => {
      o.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = configuration["JwtSettings:Issuer"],
        ValidAudience = configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
      };
    });


    services.AddTransient<AuthService>();

    return services;
  }
}