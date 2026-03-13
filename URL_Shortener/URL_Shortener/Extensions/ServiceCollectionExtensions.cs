using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using URL_Shortener.Data.Repository;
using URL_Shortener.Services;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();
            services.AddScoped<IUrlBuilderService, UrlBuilderService>();
            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var securityKey = jwtSettings.GetValue<string>("SecurityKey");

            if (securityKey.IsNullOrEmpty())
            {
                throw new InvalidOperationException ("Secret security key is not found in configuration");
            }

            services.AddAuthentication(options =>
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
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt"))
                            context.Token = context.Request.Cookies["jwt"];

                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
    }
}
