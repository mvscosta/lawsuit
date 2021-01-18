using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mc2Tech.FluentSwagger.Config
{
    public static class SwaggerGenExtension
    {
        public static IServiceCollection AddSwaggerGenAadJwtBearer(this IServiceCollection services, AzureADOptions azureAdOptions, Action<SwaggerGenOptions> setupAction = null)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{azureAdOptions.Instance}{azureAdOptions.TenantId}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{azureAdOptions.Instance}{azureAdOptions.TenantId}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Sign In Permissions" },
                                { "profile", "User Profile Permissions" },
                                { $"api://{azureAdOptions.TenantId}/{azureAdOptions.ClientId}/user_impersonation", "Application API Permissions" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new [] { "readAccess", "writeAccess" }
                    }
                });

                if (setupAction != null)
                {
                    setupAction(options);
                }
            });

            return services;
        }

        public static IServiceCollection AddSwaggerGenDocs(this IServiceCollection services, SwaggerConfig swaggerConfig, Action<SwaggerGenOptions> setupAction = null)
        {
            services.AddSwaggerGen(options =>
            {
                options.CreateSwaggerDoc(swaggerConfig);
                options.IncludeComments(swaggerConfig.XmlFileName);

                if (setupAction != null)
                {
                    setupAction(options);
                }
            });

            return services;
        }

        private static void CreateSwaggerDoc(this SwaggerGenOptions options, SwaggerConfig swaggerConfig)
        {
            options.SwaggerDoc(swaggerConfig.VersionName,
                new OpenApiInfo
                {
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    Version = swaggerConfig.VersionNumber
                });
        }

        private static void IncludeComments(this SwaggerGenOptions options, string fileName)
        {
            options.IncludeXmlComments(GetXmlCommentsPath(fileName));
        }

        private static string GetXmlCommentsPath(string fileName)
        {
            return $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{fileName}";
        }
    }
}
