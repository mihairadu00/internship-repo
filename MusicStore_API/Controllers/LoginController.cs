using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore_API.Middleware.Auth;
using MusicStore_API.Models;
using MusicStore_Common.Entities;
using MusicStore_Interfaces;
using System.Threading.Tasks;

namespace MusicStore_API.Controllers
{

    /// <summary>
    /// Authentication for users
    /// </summary>
    [Route("login1")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest login)
        {

            IActionResult response = Unauthorized();
            var token = await _userService.Authenticate(login.Username, login.Password);
            if (token != null)
                response = Ok(token);

            return response;
       
        }

        /// <summary>
        /// Refresh the access token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        [Authorize(Policy = Policies.All)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RefreshAccessToken()
        {

            IActionResult response = Unauthorized();
            var user = (User)HttpContext.Items["User"];
            var token = await _userService.RenewAccessToken(user);
            if (token != null)
                response = Ok(token);

            return response;

        }

        /// <summary>
        /// Register a new user in app
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registration)
        {

            IActionResult response = BadRequest();
            var addedUser = await _userService.Register(registration.FirstName, registration.LastName, registration.Username, registration.Password);

            if (addedUser != null)
                response = Ok(addedUser);

            return response;

        }

    }

}