using System.Text.Json.Serialization;
using Evently.API.Contexts;
using Evently.API.Filters;
using Evently.API.Middlewares;
using Evently.Infrastructure.EFCore;
using Evently.Infrastructure.EFCore.Data;
using Evently.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Evently.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AuthOptions.MakeOptions(builder.Configuration, Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<UserContext>();

        builder.Services.AddInfrastructure();
        builder.Services.AddServices();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = AuthOptions.SymmetricSecurityKey
                };
            });

        builder.Services.AddControllers().AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            
            c.OperationFilter<EndpointAuthRequirementFilter>();
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options => { options.RouteTemplate = "api/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Survey Backend V1");
                options.RoutePrefix = "api/swagger";
            });
        }
        
        app.UseMiddleware<ExceptionsMiddleware>();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll"); // переместить сюда
        
        app.MapControllers();

        app.Run();
    }
}