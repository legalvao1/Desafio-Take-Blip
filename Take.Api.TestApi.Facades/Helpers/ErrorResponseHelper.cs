using System.Net;

using Take.Api.BancoPanCartoes.Facades.Helpers.Interfaces;
using Take.Api.BancoPanCartoes.Models.Exceptions;

namespace Take.Api.BancoPanCartoes.Facades.Helpers
{
    public class ErrorResponseHelper : IErrorResponseHelper
    {
        public ErrorResponse GetErrorResponse(string content, HttpStatusCode status)
        {
            switch (status)
            {
                default:
                    return new ErrorResponse(content)
                    {
                        Message = content
                    };
            }
        }
    }
}
