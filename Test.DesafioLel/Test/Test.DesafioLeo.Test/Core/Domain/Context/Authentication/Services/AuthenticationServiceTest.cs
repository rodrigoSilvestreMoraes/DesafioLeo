using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Config;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Services;
using Test.DesafioLeo.Core.Infrastructure.Repository;
using Xunit;

namespace Test.DesafioLeo.Test.Core.Domain.Context.Authentication.Services
{
	public class AuthenticationServiceTest
	{
		AppSettings _appSettings;
		Mock<IUserRepository> _userRepository;

		public AuthenticationServiceTest() 
		{
			_userRepository = new Mock<IUserRepository>();
			_appSettings = new AppSettings { Secret = "teste@teste@teste" };
		}

		[Fact]
		public async void Should_Authenticate_Passed()
		{
			_userRepository.Setup(x => x.ValidadeCredencials(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
			var service = GetService();
			var result = await service.Authenticate("teste", "@CodigoLimpo2020");
			Assert.True(result.HasAuthenticaded);
		}

		[Fact]
		public async void Cannot_Authenticate_Passed()
		{
			_userRepository.Setup(x => x.ValidadeCredencials(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));
			var service = GetService();
			var result = await service.Authenticate("teste", "@CodigoLimpo2020");
			Assert.False(result.HasAuthenticaded);
		}

		[Fact]
		public async void Should_CreatePassword()
		{
			_userRepository.Setup(x => x.CreatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
			Assert.True(await GetService().CreatePassword("teste", "teste"));
		}

		[Fact]
		public void Should_GetUser()
		{
			Assert.NotNull(GetService().GetUser("teste"));
		}

		private IAuthenticationService GetService() => new AuthenticationService(_userRepository.Object, _appSettings);
	}
}
