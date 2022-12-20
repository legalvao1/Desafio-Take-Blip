using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Take.Api.TestApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortDirectionType
    {
        ASC,
        DESC
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortParameterType
    {
        RepositoreCreatedAt,
        Description,
        FullName,
        Language
    }
}
