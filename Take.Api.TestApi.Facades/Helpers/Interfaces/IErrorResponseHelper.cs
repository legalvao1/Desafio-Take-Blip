using System.Net;

using Take.Api.BancoPanCartoes.Models.Exceptions;

namespace Take.Api.BancoPanCartoes.Facades.Helpers.Interfaces
{
    public interface IErrorResponseHelper
    {
        /// <summary>
        /// Based on api's return, check if STATUS code are BadRequest
        /// If so, returns Error Response class
        /// </summary>
        /// <param name="content"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ErrorResponse GetErrorResponse(string content, HttpStatusCode status);
    }
}
