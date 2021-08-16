using System.Linq;
using System.Text.RegularExpressions;

namespace Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel
{
	public class AuthenticationRequest
	{
		public const string MessageInvalidPassword = "A senha se encontra fora das politícas de segurança: A senha precisa conter." +
					"Conter no mínimo 15 caracteres. No mínimo uma letra maiúscula. No mínimo uma letra minúscula., No mínimo um dos seguintes caracteres especiais: (@,#,_,- e !)." +
					"Não poder ter caracteres repetidos em sequência, por exemplo: 1111, aaaa, bbbb, @@@@, BBBB.";

		public string User { get; set; }
		public string Password { get; set; }

		public bool ValidateRolesPassword() => RolesPasswordProcess(this.Password);
		public static bool ValidateRolesPassword(string password) => RolesPasswordProcess(password);

		private static bool RolesPasswordProcess(string password)
		{
			if (password.Length < 15)
				return false;

			var matches = Regex.Matches(password, @"(.)\1+");
			if (matches.Any(x => x.Length >= 4))
				return false;

			var regexValidate = new Regex(@"^(?=(.*\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");
			return regexValidate.IsMatch(password);
		}
	}
}
