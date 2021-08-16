using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Test.DesafioLeo.Core.Infrastructure.Repository
{
	[ExcludeFromCodeCoverage]
	public class UserRepository : IUserRepository
	{
		public async Task<bool> CreatePassword(string user, string password) => await Task.FromResult(true);
		public async Task<bool> ValidadeCredencials(string user, string password) => await Task.FromResult(true);
	}
}
