using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using RestEase;

namespace Take.Api.TestApi.Services
{
    public interface IHomeService
    {
        /// <summary>
        /// Checks if api from Banco Pan have status sucess
        /// </summary>
        [Get("https://api.github.com/orgs/takenet/repos")]
        [Header("User-Agent", "take")]
        Task<string> GetTakeBlipRepositoriesAsync();
    }
}
