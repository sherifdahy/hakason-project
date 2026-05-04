using App.BLL.Authentication;
using App.BLL.ClassOptions;
using App.BLL.Providers;
using App.BLL.Services;
using App.DAL.Data;
using App.Domain.ClassOptions;
using App.Domain.Entities.Identity;
using App.Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.API;

public static class AddDependencyInjection
{
    public static void AddConfiguration(this IServiceCollection services,IConfiguration configuration)
    {
        services
            .AddDbContextConfig(configuration)
            .AddIdentityConfig()
            .AddCorsConfig()
            .AddAuthConfig(configuration)
            .AddFluentValidationConfig()
            .AddScelerConfig()
            .AddServicesConfig();
    }

    private static IServiceCollection AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IEmailSender, EmailService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IInvitationService, InvitationService>();

        services.AddOptions<MailSettings>()
            .BindConfiguration(nameof(MailSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;

    }
    private static IServiceCollection AddScelerConfig(this IServiceCollection services)
    {
        services.AddSingleton<BearerSecuritySchemeTransformer>();

        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        return services;
    }

    private static IServiceCollection AddDbContextConfig(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("default");

        return services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }


    private static IServiceCollection AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options=>
        {
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        return services;
    }
    private static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
        });
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            // validation 
            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
            };
        });

        return services;
    }
    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssembly(typeof(AuthService).Assembly);

        return services;
    }

}
