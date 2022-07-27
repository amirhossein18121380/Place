using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Place.Application.Identities;
using Place.Application.Identities.Interface;
using Place.Domain.Interface;
using Place.Domain.Models;
using Place.Infrastructure.DAL;
using Place.Infrastructure.Tools;

namespace Place.API.ExtensionMethods;

public static class CustomExtensionMethods
{

    public static IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Register the Swagger generator, defining 1 or more Swagger documents
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vi.Api", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "توکن خود را وارد کنید",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    System.Array.Empty<string>()
                }
            });
        });
        services.Configure<Configurations>(options =>
            {
                options.SQLConnectionString = configuration.GetSection("SQL:ConnectionString").Value;
                options.SQLDataBaseName = configuration.GetSection("SQL:Database").Value;
                options.JWTSecurityKey = configuration.GetSection("JWTSecurityKey").Value;
                options.TokenAudience = configuration.GetSection("TokenAudience").Value;
                options.TokenIssuer = configuration.GetSection("TokenIssuer").Value;
                options.TokenExpireTimeinHour = configuration.GetSection("TokenExpireTimeinHour").Value;
                options.SMSAPIKey = configuration.GetSection("Kavehnegar:APIKey").Value;
                options.SMSSenderNumber = configuration.GetSection("Kavehnegar:SenderNumber").Value;
            }
        );

        services.Configure<CookiePolicyOptions>(options =>
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        });

        #region First Token

        //configure jwt authentication
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWTSecurityKey").Value);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        services.AddTransient<IPasswordHasher<User>, Place.Common.PasswordHasher<User>>();

        #endregion

        services
            .AddMediatR(typeof(Program), typeof(Place.Application.Configuration.Commands.CommandBase))
            .AddMediatR(typeof(Program), typeof(Place.Application.Configuration.Commands.CommandBase<>))
            .AddTransient<IUserDal, UserDal>()
            .AddTransient<IPlaceDal, PlaceDal>()
            .AddTransient<IReservationDal, ReservationDal>()
            .AddTransient<IJWTHelper, JWTHelper>()
            .AddAutoMapper(typeof(Program))
            .AddControllersWithViews();

    
        
        var container = new ContainerBuilder();
        container.Populate(services);

        return new AutofacServiceProvider(container.Build());
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env, IConfiguration configuration)
    {

        ConfigurationHelper.Configure(app.Configuration);

        var pathBase = configuration["PATH_BASE"];

        if (app.Environment.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "Catalog.API V1");
                });
        }

        app.UseExceptionHandler(
            options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "text/html";
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (null != exceptionObject)
                    {
                        var errorMessage = $"{exceptionObject.Error.Message}";
                        await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                        //await JsonSerializer.SerializeAsync(context.Response.Body, errorMessage);
                    }
                });
            }
        );

      

        app.UseCors("CorsPolicy");
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseHttpsRedirection();

    }

    public class Configurations
    {
        public string MongoDBConnectionString { get; set; }
        public string SQLConnectionString { get; set; }
        public string SQLDataBaseName { get; set; }
        public string MongoDataBaseName { get; set; }
        public string SMSAPIKey { get; set; }
        public string SMSSenderNumber { get; set; }
        public string Secret { get; set; }
        public string JWTSecurityKey { get; set; }
        public string TokenExpireTimeinHour { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
    }
}

