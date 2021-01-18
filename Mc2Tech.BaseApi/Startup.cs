using AutoMapper;
using FluentValidation;
using Mc2Tech.FluentSwagger.Config;
using Mc2Tech.Pipelines.Audit;
using Mc2Tech.Pipelines.Audit.DAL;
using Mc2Tech.Pipelines.Logging;
using Mc2Tech.Pipelines.Timeout;
using Mc2Tech.Pipelines.Transaction;
using Mc2Tech.Pipelines.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleSoft.Mediator;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2Tech.BaseApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public readonly SwaggerConfig SwaggerConfig;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            var swaggerConfigExtractor = new SwaggerConfigExtractor(Configuration, typeof(Startup).Assembly);
            SwaggerConfig = swaggerConfigExtractor.SwaggerConfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IdentityModelEventSource.ShowPII = true;
            #region Authentication configuration
            var azureAdOptions = new AzureADOptions();
            Configuration.Bind("AzureAd", azureAdOptions);
            var tokenOptions = new TokenOptions();
            Configuration.Bind("Authentication", tokenOptions);

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                options.IncludeErrorDetails = true;
                options.Authority = $"{azureAdOptions.Instance}{azureAdOptions.TenantId}/v2.0";
                //options.TokenValidationParameters.ValidateAudience = false;
                //options.TokenValidationParameters.ValidateIssuer = false;
            });
            #endregion

            var connectionString = Configuration.GetConnectionString("AuditDbContext");
            services.AddDbContext<AuditDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(actionContext.ModelState);
            });

            services.AddMediator(o =>
            {
                o.AddPipeline<LoggingPipeline>();
                o.AddPipeline<TimeoutPipeline>();
                o.AddPipeline<ValidationPipeline>();
                o.AddPipeline<AuditPipeline>();

                foreach (var implementationType in typeof(Startup)
                    .Assembly
                    .ExportedTypes
                    .Where(t => t.IsClass && !t.IsAbstract))
                {
                    foreach (var serviceType in implementationType
                        .GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                    {
                        o.Services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient));
                    }
                }

                o.AddHandlersFromAssemblyOf<Startup>();
            });

            services.AddControllers();
            services.AddApiVersioning();

            services.AddAutoMapper(
                cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.AllowNullDestinationValues = true;
                },
                AppDomain.CurrentDomain.GetAssemblies()
            );

            services.AddSwaggerGenAadJwtBearer(azureAdOptions);
            services.AddSwaggerGenDocs(SwaggerConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (ValidationException e)
                {
                    var response = ctx.Response;
                    if (response.HasStarted)
                        throw;

                    ctx.RequestServices
                        .GetRequiredService<ILogger<Startup>>()
                        .LogWarning(e, "Invalid data has been submitted");

                    response.Clear();
                    response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    var resultError = new
                    {
                        Message = "Invalid data has been submitted",
                        ModelState = e.Errors.ToDictionary(error => $"{error.ErrorCode}-{error.PropertyName}", error => error.ErrorMessage)
                    };

                    await response.WriteJsonAsync(resultError);
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(SwaggerConfig.DocUrl, SwaggerConfig.Description);
                    options.OAuthClientId(Configuration["AzureAd:ClientId"]);
                    options.OAuthClientSecret("client-secret");
                    options.OAuthRealm("client-realm");
                    options.OAuthAppName("Mc2Tech");
                    options.OAuthUsePkce();
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
