using System.Threading.Tasks;
using System.Collections.Generic;
using Take.Api.TestApi.Models;

namespace Take.Api.TestApi.Facades.Interfaces
{
    public interface IGithubRepositoriesFacade
    {
        /// <summary>
        /// Gets a list of a selected organization repositories by language
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sortDirection"></param>
        /// <param name="quantity"></param>
        /// <param name="organization"></param>
        /// <returns>A list of objects </returns>
        Task<object> GetOrganizationRepositoriesAsync(string language, 
                                                  SortDirectionType sortDirection,
                                                  SortParameterType sortParameter,
                                                  int quantity,
                                                  string organization);
    }
}
