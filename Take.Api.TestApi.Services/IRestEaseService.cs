using System;
using System.Threading.Tasks;

using RestEase;

namespace Take.Api.TestApi.Services
{
    public interface IRestEaseService : IDisposable
    {
        /// <summary>
        /// Get all the repositories for one organization on Github
        /// </summary>
        [Get("https://api.github.com/orgs/{organization}/repos")]
        [Header("User-Agent", "take")]
        Task<string> GetOrganizationRepositoriesAsync([Path] string organization);

        /// <summary>
        /// Get all the organizations listed on Github
        /// </summary>
        [Get("https://api.github.com/orgs/{organization}")]
        [Header("User-Agent", "take")]
        //[Header("Accept", "application/vnd.github.v3+json")]
        Task<string> GetOrganizationAsync([Path] string organization);
    }
}
