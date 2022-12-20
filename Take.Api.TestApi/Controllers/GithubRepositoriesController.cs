using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Models;

namespace Take.Api.TestApi.Controllers
{
    /// <summary>
    /// This controller is responsible to get take blip repos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GithubRepositoriesController : ControllerBase
    {
        private readonly IGithubRepositoriesFacade _githubRepositoriesFacade;
        private readonly IOrganizationRepositoryLanguagesFacade _organizationRepositoryLanguagesFacade;
        private const string INVALID_RESPOSITORY_RANGE = "Please chose a value between 1 and 10";
        private const string LANGUAGE_NOT_FOUND = "Invalid language, to consult available Languages, check the available languages endpoint";

        /// <summary>
        /// contructor for this controller
        /// </summary>
        public GithubRepositoriesController(IGithubRepositoriesFacade githubRepositoriesFacade,
                                            IOrganizationRepositoryLanguagesFacade organizationRepositoryLanguagesFacade)
        {
            _githubRepositoriesFacade = githubRepositoriesFacade;
            _organizationRepositoryLanguagesFacade = organizationRepositoryLanguagesFacade;
        }

        /// <summary>
        /// Gets a repositories list of a selected organization by language
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortParameter"></param>
        /// <param name="quantity"></param>
        /// <param name="organization"></param>
        /// <remarks>
        /// Exemplo:
        ///
        ///     Get /api/GithubRepositories
        ///     {
        ///        "Language": Programming language,
        ///        "Sort-Direction-Type": "ASC = ascending, DESC = downward ",
        ///        "Sort-Parameter-Type": Select one of the available sorting parameters,
        ///        "Repositories-Quantity" : "Minimum 1, Maximum 10",
        ///        "Organization-Name" : "Github organization Name"
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns a list of repositories by language for the selected organization</returns>
        /// <response code="200">Returns a list of repositories by language for the selected organization</response>
        /// <response code="400">Returns a error message</response>
        /// <response code="401">User is not authorized to make a request without authentication</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationRepositoriesAsync([FromQuery(Name = Constants.LANGUAGE), Required] string language,
                                                                      [FromQuery(Name = Constants.SORT_DIRECTION_TYPE)] SortDirectionType sortDirection,
                                                                      [FromQuery(Name = Constants.SORT_PARAMETER_TYPE)] SortParameterType sortParameter,
                                                                      [FromQuery(Name = Constants.REPOSITORIES_QUANTITY), Required] int quantity,
                                                                      [FromQuery(Name = Constants.ORGANIZATION_NAME), Required] string organization)
        {
            if (quantity <= 0 || quantity > 10)
            {
                throw new BaseException(INVALID_RESPOSITORY_RANGE);
            }

            var availableOrgLanguages = await _organizationRepositoryLanguagesFacade.GetOrganizationLanguagesAsync(organization);
            if (!availableOrgLanguages.Contains(language.ToUpper()))
            {
                throw new BaseException(LANGUAGE_NOT_FOUND);
            }

            var repositories = await _githubRepositoriesFacade.GetOrganizationRepositoriesAsync(language, sortDirection, sortParameter, quantity, organization);

            return Ok(repositories);
        }

    }

}
