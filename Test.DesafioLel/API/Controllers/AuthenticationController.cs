using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Services;
using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;
using Test.DesafioLeo.Filters;

namespace Test.DesafioLeo.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
        readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("validadeUser")]
        public async Task<IActionResult> ValidadeUser([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (!authenticationRequest.ValidateRolesPassword())
                return StatusCode(StatusCodes.Status422UnprocessableEntity, AuthenticationRequest.MessageInvalidPassword);

            var response = await _authenticationService.Authenticate(authenticationRequest.User, authenticationRequest.Password);

            if (response == null)
                return UnprocessableEntity(new { message = "Usuário e senha se encontram incorretos." });

            return Ok(response);

        }

        [Authorize]
        [HttpPost("create/password")]
        public async Task<IActionResult> CreatePassword([FromBody] AuthenticationRequest authenticationRequest)
        {
            var result = authenticationRequest.ValidateRolesPassword();

            if (!result)
                return UnprocessableEntity(new { message = AuthenticationRequest.MessageInvalidPassword });

            var resultCreate = await _authenticationService.CreatePassword(authenticationRequest.User, authenticationRequest.Password);
            if(!resultCreate)
                return UnprocessableEntity(new { message = "Falha ao criar senha do usuário" });

            return Ok(resultCreate);
        }

        [Authorize]
        [HttpGet("validate/password/{password}")]
        public IActionResult ValidadePassword([FromRoute] string password)
		{
            var result = AuthenticationRequest.ValidateRolesPassword(password);

            if(!result)
                return UnprocessableEntity(new { message = AuthenticationRequest.MessageInvalidPassword });

            return Ok(result);
        }
    }
}
