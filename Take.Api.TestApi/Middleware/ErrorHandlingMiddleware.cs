using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Take.Api.TestApi.Facades.Strategies.ExceptionHandlingStrategies;
using Take.Api.TestApi.Models;

using Lime.Protocol;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using Serilog;
using Serilog.Context;
using Take.Api.BancoPanCartoes.Models.Exceptions;
using RestEase;
using Take.Api.BancoPanCartoes.Facades.Helpers.Interfaces;

namespace Take.Api.TestApi.Middleware
{
    /// <summary>
    /// Wraps all controller actions with a try-catch latch to avoid code repetition
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IErrorResponseHelper _errorResponseHelper;
        private const string CORRELATION_ID = "CorrelationId";
        private const string ERROR_HANDLING_MIDDLEWARE = "ErrorHandlingMiddleware";

        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ErrorHandlingMiddleware(RequestDelegate next, 
                                       ILogger logger,
                                       IErrorResponseHelper errorResponseHelper)
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _next = next;
            _logger = logger;
            _errorResponseHelper = errorResponseHelper;
        }

        /// <summary>
        /// Invoke Method, to validate requisition errors
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = string.Empty;
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = default;
            }
            var requestId = Guid.NewGuid();

            using (LogContext.PushProperty(CORRELATION_ID, requestId.ToString()))
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(requestBody, context, ex);
                }
            }
        }

        private async Task HandleExceptionAsync(string requestBody, HttpContext context, Exception exception)
        {
            const string METHOD_NAME = "HandleExceptionAsync";

            if (exception is BaseException baseException)
            {
                exception = new BaseException(baseException.Message, baseException);

                if (baseException.Message.Contains("Internal Server Error"))
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            else if (exception is ApiException apiException)
            {
                context.Response.ContentType = MediaType.ApplicationJson;
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(_errorResponseHelper.GetErrorResponse(apiException.Content, apiException.StatusCode)));
            }

            else
            {
                _logger.Error(exception, "{@Middleware} | {@Method} | [requestId:{@requestId}] [{@user}] Error: {@exception}", 
                    ERROR_HANDLING_MIDDLEWARE, METHOD_NAME, 
                    context.Request.Headers[Constants.BLIP_USER_HEADER], exception.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            _logger.Error(exception, "{@Middleware} | {@Method} | [requestId:{@requestId}] [traceId:{@traceId}]{@user} Error. Headers: {@headers}. Query: {@query}. Path: {@path}. Body: {@requestBody}",
                ERROR_HANDLING_MIDDLEWARE, METHOD_NAME, 
                          context.TraceIdentifier, context.Request.Headers[Constants.BLIP_USER_HEADER],
                          context.Request.Headers, context.Request.Query, context.Request.Path, requestBody);

            context.Response.ContentType = MediaType.ApplicationJson;
            await context.Response.WriteAsync(JsonConvert.SerializeObject($"{exception.Message}| traceId: {context.TraceIdentifier}"));
        }
    }
}
