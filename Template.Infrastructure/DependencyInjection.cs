using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Template.Application.Interfaces;
using Template.Infrastructure.Database;
using Template.Infrastructure.Enums;
using Template.Infrastructure.Services;

namespace Template.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(WebApplicationBuilder builder)
        {
            services
                .AddCors(corsOptions =>
                {
                    corsOptions.AddPolicy(nameof(Policy.AllowAny), policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
                })
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "TODO",
                        ValidAudience = "TODO",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TODO"))
                    };
                });
        
            return services
                .AddPersistence(builder.Configuration)
                .AddSerilog(builder)
                .AddServices();
        }

        private IServiceCollection AddPersistence(IConfiguration configuration)
        {
            return services
                .AddDbContext<AppDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        npgSqlOptionsBuilder =>
                        {
                            npgSqlOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                })
                .AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        }

        private IServiceCollection AddServices()
        {
            return services
                .AddScoped<IService, Service>();
        }

        private IServiceCollection AddSerilog(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();
            });

            return services;
        }
    }
}