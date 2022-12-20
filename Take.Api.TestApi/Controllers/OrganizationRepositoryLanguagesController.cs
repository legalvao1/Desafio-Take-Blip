using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public class OrganizationRepositoryLanguagesController : ControllerBase
    {
        private readonly IOrganizationRepositoryLanguagesFacade _organizationRepositoryLanguagesFacade;

        /// <summary>
        /// contructor for this controller
        /// </summary>
        public OrganizationRepositoryLanguagesController(IOrganizationRepositoryLanguagesFacade organizationRepositoryLanguagesFacade)
        {
            _organizationRepositoryLanguagesFacade = organizationRepositoryLanguagesFacade;
        }

        /// <summary>
        /// Gets a list of the available languages for this organization repository
        /// </summary>
        /// <param name="organization"></param>
        /// <returns>Returns a list of repositories by language for the selected organization</returns>
        /// <response code="200">Returns a list of repositories by language for the selected organization</response>
        /// <response code="400">Returns a error message</response>
        /// <response code="401">User is not authorized to make a request without authentication</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationLanguagesAsync([FromQuery(Name = Constants.ORGANIZATION_NAME), Required] string organization)
        {

            var languages = await _organizationRepositoryLanguagesFacade.GetOrganizationLanguagesAsync(organization);

            return Ok(languages);
        }
    }
}
