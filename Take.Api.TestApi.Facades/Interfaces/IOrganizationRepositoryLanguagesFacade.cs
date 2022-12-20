using System.Collections.Generic;
using System.Threading.Tasks;

namespace Take.Api.TestApi.Facades.Interfaces
{
    public interface IOrganizationRepositoryLanguagesFacade
    {
        /// <summary>
        /// Gets a list of available langues for this organization repository
        /// </summary>
        /// <param name="organization"></param>
        /// <returns>A list of objects </returns>
        Task<HashSet<string>> GetOrganizationLanguagesAsync(string organization);
    }
}
