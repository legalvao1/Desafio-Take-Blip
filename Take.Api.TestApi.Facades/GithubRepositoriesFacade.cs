using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Serilog;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Models;
using Take.Api.TestApi.Services;

namespace Take.Api.TestApi.Facades
{
    public class GithubRepositoriesFacade : IGithubRepositoriesFacade
    {
        private readonly ILogger _logger;
        private readonly IRestEaseService _restEaseService;
        private readonly IOrganizationRepositoryLanguagesFacade _organizationRepositoryLanguagesFacade;
        private const string INVALID_RESPOSITORY_RANGE = "Please chose a value between 1 and 10";

        public GithubRepositoriesFacade(ILogger logger,
                                        IRestEaseService restEaseService,
                                        IOrganizationRepositoryLanguagesFacade organizationRepositoryLanguagesFacade)
        {
            _restEaseService = restEaseService;
            _organizationRepositoryLanguagesFacade = organizationRepositoryLanguagesFacade;
            _logger = logger;
        }

        public async Task<object> GetOrganizationRepositoriesAsync(string language,
                                                               SortDirectionType sortDirection,
                                                               SortParameterType sortParameter,
                                                               int quantity,
                                                               string organization)
        {
            if (quantity <= 0 || quantity > 10)
            {
                throw new BaseException(INVALID_RESPOSITORY_RANGE);
            }

            _logger.Information("[API] Organization name | {@organization}", organization);

            var response = await _restEaseService.GetOrganizationRepositoriesAsync(organization);

            _logger.Information("[API] Response for the selected organization  | {@response}", response);

            var responseToJson = JsonConvert.DeserializeObject<List<GithubResponse>>(response);

            var repositoriesByLanguage = GetRepositoriesByLanguage(responseToJson, language);

            var orderedRepositoriesList = OrderedRepositories(repositoriesByLanguage, sortDirection.ToString(), sortParameter.ToString());

            var repositoriesRange = orderedRepositoriesList.Count > quantity ? quantity : orderedRepositoriesList.Count;
            var selectedRespositoriesQuantity = orderedRepositoriesList.GetRange(0, repositoriesRange);

            return selectedRespositoriesQuantity;
        }

        private List<GithubResponse> OrderedRepositories(IEnumerable<GithubResponse> repos, string sortDirection, string sortParameter)
        {
            if (SortDirectionType.DESC.ToString().Equals(sortDirection))
            {
                return repos.OrderByDescending(x => x.GetType().GetProperty(sortParameter).GetValue(x)).ToList();
            }
            else
            {
                return repos.OrderBy(x => x.GetType().GetProperty(sortParameter).GetValue(x)).ToList();
            }
        }

        private IEnumerable<GithubResponse> GetRepositoriesByLanguage(IEnumerable<GithubResponse> repos, string language)
        {
            var repositoriesWithLanguageNotNull = repos.Where(repo => repo.Language != null);
            var repositoriesByLanguage = repositoriesWithLanguageNotNull.Where(repo => repo.Language.ToUpper().Equals(language.ToUpper()));

            return repositoriesByLanguage;
        }
    }
}
