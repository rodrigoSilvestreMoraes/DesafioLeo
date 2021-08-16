using System;

namespace Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel
{
	public class AuthenticationModel
	{
		public string User { get; set; }
		public bool HasAuthenticaded { get; set; }
		public string Token { get; set; }
		public DateTime TokenWillExpire { get; set; }
	}
}
