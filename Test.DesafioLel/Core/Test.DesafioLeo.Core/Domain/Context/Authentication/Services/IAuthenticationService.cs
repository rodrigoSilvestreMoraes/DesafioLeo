using System.Threading.Tasks;
using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;

namespace Test.DesafioLeo.Core.Domain.Context.Authentication.Services
{
	public interface IAuthenticationService
	{
		Task<AuthenticationModel> Authenticate(string user, string password);
		Task<bool> CreatePassword(string user, string password);
		AuthenticationModel GetUser(string user);
	}
}
