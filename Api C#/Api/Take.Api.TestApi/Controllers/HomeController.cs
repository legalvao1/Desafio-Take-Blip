using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Take.Api.TestApi.Facades.Interfaces;

namespace Take.Api.TestApi.Controllers
{
    /// <summary>
    /// This controller is responsible to get take blip repos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeFacade _homeFacade;

        /// <summary>
        /// contructor for this controller
        /// </summary>
        public HomeController(IHomeFacade homeFacade)
        {
            _homeFacade = homeFacade;
        }

        /// <summary>
        /// Gets a repositories list from Take Blip Github
        /// </summary>
        /// <returns>DueDateResponse object</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTakeBlipRepositoriesAsync()
        {
            var repositories = await _homeFacade.GetTakeBlipRepositoriesAsync();

            return Ok(repositories);
        }

    }

}
