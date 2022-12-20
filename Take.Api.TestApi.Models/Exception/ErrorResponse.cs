using Newtonsoft.Json;

namespace Take.Api.BancoPanCartoes.Models.Exceptions
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message text
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }

    }
}
