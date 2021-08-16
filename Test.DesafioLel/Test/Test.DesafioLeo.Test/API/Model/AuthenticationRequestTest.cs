using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;
using Xunit;

namespace Test.DesafioLeo.Test.API.Model
{
	public class AuthenticationRequestTest
	{
		[Fact]
		public void Should_PasswordPassed()
		{
			var request = new AuthenticationRequest { User = "teste", Password = "@CodigoLimpo2020" };
			Assert.True(request.ValidateRolesPassword());
		}

		[Fact]
		public void Should_PasswordPassed_SendPassword() => Assert.True(AuthenticationRequest.ValidateRolesPassword("@CodigoLimpo2020"));

		[Fact]
		public void Cannot_PassedPasswordLess15() => Assert.False(AuthenticationRequest.ValidateRolesPassword("@Codigo20"));

		[Fact]
		public void Cannot_PassedRepearSequenceChar() => Assert.False(AuthenticationRequest.ValidateRolesPassword("@Codigo@@@@2020"));

	}
}
