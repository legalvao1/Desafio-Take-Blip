﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Take.Api.TestApi.Facades.Strategies.ExceptionHandlingStrategies;
using Take.Api.TestApi.Models;
using Take.Api.TestApi.Models.UI;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RestEase;

using Serilog;
using Serilog.Exceptions;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Services;
using System.Net.Http;

namespace Take.Api.TestApi.Facades.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        private const string APPLICATION_KEY = "Application";
        private const string SETTINGS_SECTION = "Settings";

        /// <summary>
        /// Registers project's specific services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSingletons(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();

            // Dependency injection
            services.AddSingleton(settings)
                    .AddSingleton(settings.BlipBotSettings)
                    .AddSingleton<IHomeFacade, HomeFacade>();


            var restEase = RestClient.For<IHomeService>(new HttpClient());
            services.AddSingleton(restEase);

            services.AddSingleton(provider =>
            {
                var logger = provider.GetService<ILogger>();
                return new Dictionary<Type, ExceptionHandlingStrategy>
                {
                    { typeof(ApiException), new ApiExceptionHandlingStrategy(logger) },
                    { typeof(NotImplementedException), new NotImplementedExceptionHandlingStrategy(logger) }
                };
            });

            // SERILOG settings
            services.AddSingleton<ILogger>(new LoggerConfiguration()
                     .ReadFrom.Configuration(configuration)
                     .Enrich.WithMachineName()
                     .Enrich.WithProperty(APPLICATION_KEY, Constants.PROJECT_NAME)
                     .Enrich.WithExceptionDetails()
                     .CreateLogger());
        }
    }
}
