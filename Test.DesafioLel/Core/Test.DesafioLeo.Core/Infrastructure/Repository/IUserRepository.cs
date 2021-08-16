using System.Threading.Tasks;

namespace Test.DesafioLeo.Core.Infrastructure.Repository
{
	public interface IUserRepository
	{
		Task<bool> ValidadeCredencials(string user, string password);
		Task<bool> CreatePassword(string user, string password);
	}
}
