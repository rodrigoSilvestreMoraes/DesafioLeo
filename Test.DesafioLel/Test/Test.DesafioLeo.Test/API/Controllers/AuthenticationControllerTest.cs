using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.DesafioLeo.Controllers;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Services;
using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;
using Xunit;

namespace Test.DesafioLeo.Test.API.Controllers
{
	public class AuthenticationControllerTest
	{
		Mock<IAuthenticationService> _authenticationService;

		public AuthenticationControllerTest()
		{
			_authenticationService = new Mock<IAuthenticationService>();
		}

		[Fact]
		public async void Should_ValidadeUser_Passed()
		{
			var authenticationRequest = new AuthenticationRequest { User = "teste", Password = "@CodigoLimpo3030" };
			var mock = new AuthenticationModel { HasAuthenticaded = true, Token = "dsfdsdfdfdfd", TokenWillExpire = DateTime.UtcNow.AddMinutes(5), User = "teste" };
			_authenticationService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(mock));

			var result = await GetController().ValidadeUser(authenticationRequest);
			var objectResult = result as ObjectResult;

			Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode.Value);
		}

		private AuthenticationController GetController() => new AuthenticationController(_authenticationService.Object);
	}
}
