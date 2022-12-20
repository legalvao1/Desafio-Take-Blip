﻿using System.Threading.Tasks;

using Take.Api.TestApi.Facades.Strategies.ExceptionHandlingStrategies;
using Take.Api.TestApi.Tests.Setup.Controller;

using NSubstitute;

using Serilog;

using Shouldly;

using Xunit;

namespace Take.Api.TestApi.Tests.Strategies.ExceptionHandlingStrategies
{
    public class ApiExceptionHandlingStrategyTests
    {
        private readonly ILogger _logger;

        public ApiExceptionHandlingStrategyTests()
        {
            _logger = Substitute.For<ILogger>();
        }

        private ApiExceptionHandlingStrategy CreateApiExceptionHandlingStrategy()
        {
            return new ApiExceptionHandlingStrategy(_logger);
        }

        [Fact]
        public async Task HandleAsyncExpectedBehaviorAsync()
        {
            var apiExceptionHandlingStrategy = CreateApiExceptionHandlingStrategy();

            var context = ControllerSetup.HttpContext;
            var exception = ControllerSetup.ApiException;

            var result = await apiExceptionHandlingStrategy.HandleAsync(
                context,
                exception);

            result.Response.StatusCode.ShouldBe((int)exception.StatusCode);
        }
    }
}
