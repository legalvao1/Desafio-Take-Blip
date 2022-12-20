using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Serilog;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Services;

namespace Take.Api.TestApi.Facades
{
    public class OrganizationRepositoryLanguagesFacade : IOrganizationRepositoryLanguagesFacade
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRestEaseService _restEaseService;
        private const string ORGANIZATION_NOT_FOUND = "Invalid organization, please verify the correct name of the organization on Github";

        public OrganizationRepositoryLanguagesFacade(IRestEaseService restEaseService,
                                                     ILogger logger)
        {
            _restEaseService = restEaseService;
            _logger = logger;
        }

        public async Task<HashSet<string>> GetOrganizationLanguagesAsync(string organization)
        {
            try
            {
                _logger.Information("[API] Organization name | {@organization}", organization);

                var response = await _restEaseService.GetOrganizationRepositoriesAsync(organization);

                _logger.Information("[API] Response for the selected organization  | {@response}", response);

                var responseToJson = JsonConvert.DeserializeObject<List<GithubResponse>>(response);

                var uniqueLanguages = new HashSet<string>();

                foreach (var i in responseToJson)
                {
                    if (i.Language != null)
                        uniqueLanguages.Add(i.Language.ToUpper());
                }

                return uniqueLanguages;

            }
            catch (Exception ex)
            {
                _logger.Error("[API] Request Failed | {@ex}", ex);
                throw new BaseException(ORGANIZATION_NOT_FOUND);
            }
        }
    }
}
