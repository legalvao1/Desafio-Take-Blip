using Newtonsoft.Json;

namespace Take.Api.TestApi.Facades.Interfaces
{
    public class GithubResponse
    {

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("created_at")]
        public string RepositoreCreatedAt { get; set; }
    }
}
