using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Take.Api.TestApi.Models;
using Take.Api.TestApi.Models.UI;
using Take.Api.TestApi.Services;
using Take.Api.TestApi.Services.Repository;

namespace Take.Api.TestApi.Controllers
{
    /// <summary>
    /// This Controller Auhtenticate a User Generating a JWT to make the requests
    /// </summary>
    [Route("api/Authentication/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApiSettings _settings;

        /// <summary>
        /// This Controller Auhtenticate a User Generating a JWT to make the requests
        /// </summary>
        public LoginController(ApiSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Authenticate and get a jwt token to the user make requests
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromHeader(Name = Constants.USER_NAME), Required] string username,
                                                                   [FromHeader(Name = Constants.USER_PASSWORD), Required] string password)
         {
            var user = UserRepository.Get(username, password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user, _settings);

            return Ok(new { token = $"Bearer {token}" });
         }
    }
}
