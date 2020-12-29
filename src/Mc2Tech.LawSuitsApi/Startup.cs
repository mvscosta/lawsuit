using AutoMapper;
using EFCoreSecondLevelCacheInterceptor;
using FluentValidation;
using Mc2Tech.BaseApi;
using Mc2Tech.LawSuitsApi.DAL;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using System.Net.Http;
using Mc2Tech.Crosscutting.ServiceClients;
using Mc2Tech.LawSuitsApi.ServiceClient;

namespace Mc2Tech
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public readonly SwaggerConfig SwaggerConfig;
        public readonly ApiEndpoints ApiEndpoints;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ApiEndpoints = new ApiEndpoints(Configuration);

            var swaggerConfigExtractor = new SwaggerConfigExtractor(Configuration, typeof(Startup).Assembly);
            SwaggerConfig = swaggerConfigExtractor.SwaggerConfig;
        }

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

            services.AddEFSecondLevelCache(options =>
                   options.UseMemoryCacheProvider().DisableLogging(true)
            );


            var connectionStringApiDb = Configuration.GetConnectionString("ApiDbContext");
            var connectionStringAuditDb = Configuration.GetConnectionString("SituationDbContext");
            var connectionStringSituationDb = Configuration.GetConnectionString("SituationDbContext");
            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionStringApiDb));
            services.AddDbContext<AuditDbContext>(options => options.UseSqlServer(connectionStringAuditDb));
            services.AddDbContext<SituationDbContext>(
                options => options
                    .UseSqlServer(connectionStringSituationDb)

            );
            //services.AddDbContext<ApiDbContext>(o =>
            //{
            //    o.UseInMemoryDatabase("ApiDbContext").ConfigureWarnings(warn =>
            //    {
            //        // since InMemoryDatabase does not support transactions
            //        // for test purposes we are going to ignore this exception
            //        warn.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            //    });
            //});

            services.AddScoped<IPersonsApiServiceClient>(provider => new PersonsApiServiceClient(ApiEndpoints, provider.GetService<IHttpClientFactory>()));

            services.AddHttpClient(ApiEndpoints.PersonsApiHttpClientName, c =>
            {
                c.BaseAddress = new Uri(ApiEndpoints.PersonsApiUri);
            }).ConfigurePrimaryHttpMessageHandler(x => new DefaultHttpClientHandler());


            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(actionContext.ModelState);
            });

            services.Mc2TechBaseApiInjections();

            int? customTimeout = Configuration.GetValue<int?>("CustomTimeoutPipeline");
            services.AddMediator(o =>
            {
                o.AddPipeline(new TimeoutPipeline(customTimeout));
                o.AddPipeline<LoggingPipeline>();
                o.AddPipeline<ValidationPipeline>();
                o.AddPipeline<TransactionPipeline<ApiDbContext>>();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
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
                endpoints.MapControllers()
            );
        }
    }
}