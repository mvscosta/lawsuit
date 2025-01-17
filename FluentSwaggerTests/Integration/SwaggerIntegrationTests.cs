﻿using System.Diagnostics;
using Mc2Tech;
using FluentSwaggerTests.Swagger;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FluentSwaggerTests.Integration
{
    public class SwaggerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string VersionName = "beta";

        private static readonly string SwaggerDocPath = $"/swagger/{VersionName}/swagger.json";

        private readonly SwaggerRuntimeValidator _swaggerRuntimeValidator;

        public SwaggerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _swaggerRuntimeValidator = new SwaggerRuntimeValidator(factory.CreateClient(), SwaggerDocPath);
        }

        [Fact]
        public async void VerifySwaggerIsRunningCorrectly_ServerWithSwaggerSetup_SwaggerIsRunningCorrectly()
        {
            // Arrange
            var expectedSwaggerRuntimeInfo = new SwaggerRuntimeInfo
            {
                Title = $"Law Suits API {VersionName}",
                Description = "Mc2tech.SoftPlan Law Suits API.",
                Version = GetProductVersion()
            };

            // Act & Assert
            await _swaggerRuntimeValidator.VerifySwaggerIsRunningCorrectly(expectedSwaggerRuntimeInfo);
        }

        private static string GetProductVersion()
        {
            var productionCodeAssembly = typeof(Startup).Assembly;

            return FileVersionInfo.GetVersionInfo(productionCodeAssembly.Location).ProductVersion;
        }
    }
}