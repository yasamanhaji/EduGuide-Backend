using Base.Application;
using Base.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Base.Api.Registration
{
    public static class BaseApiRegistration
    {
        public static void RegisterBaseApi(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var services = builder.Services;

            builder.RegisterBaseApplication(configuration);

            services.AddControllers();
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            //services.Configure<ScalarOptions>(options =>
            //{
            //    options.
            //})
            #region Swagger
            services.ConfigureSwagger();
            #endregion 

            services.AddCors(options =>
            {
                options.AddPolicy("policy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();

                });
                options.AddPolicy("chat", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)  // Temporary for testing
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
                    //builder.WithOrigins("http://localhost:5173")
                    //.AllowAnyHeader()
                    //.AllowAnyMethod()
                    //.AllowCredentials();

                    //builder.WithOrigins("http://62.60.213.13")
                    //.AllowAnyHeader()
                    //.AllowAnyMethod()
                    //.AllowCredentials();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = false,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });
            //services.
            services.RegisterBaseInfra(configuration);
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EduGuide API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        } 
    }
}
