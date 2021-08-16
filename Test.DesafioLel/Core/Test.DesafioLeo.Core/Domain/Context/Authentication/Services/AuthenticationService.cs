using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Config;
using System.Security.Claims;
using Test.DesafioLeo.Core.Infrastructure.Repository;

namespace Test.DesafioLeo.Core.Domain.Context.Authentication.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		readonly AppSettings _appSettings;
		readonly IUserRepository _userRepository;
		public AuthenticationService(IUserRepository userRepository, AppSettings appSettings)
		{
			_userRepository = userRepository;
			_appSettings = appSettings;
		}

		public async Task<AuthenticationModel> Authenticate(string user, string password)
		{
			var modelResult = new AuthenticationModel()
			{
				User = user,
				HasAuthenticaded = false
			};
			if (await _userRepository.ValidadeCredencials(user, password))
				GenerateToken(modelResult);

			return modelResult;
		}

		public async Task<bool> CreatePassword(string user, string password) => await _userRepository.CreatePassword(user, password);
		public AuthenticationModel GetUser(string user) => new AuthenticationModel { User = "teste", HasAuthenticaded = true };
		
		private AuthenticationModel GenerateToken(AuthenticationModel authenticationModel)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			DateTime? tokenDateExpired = DateTime.UtcNow.AddMinutes(5);
			authenticationModel.TokenWillExpire = tokenDateExpired.Value;
			authenticationModel.HasAuthenticaded = true;

			var listClaims = new List<Claim>();
			listClaims.Add(new Claim("user", authenticationModel.User.ToString()));
			listClaims.Add(new Claim("hasAuthenticaded", authenticationModel.HasAuthenticaded.ToString()));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(listClaims),
				Expires = tokenDateExpired.Value,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			authenticationModel.Token = tokenHandler.WriteToken(token);
			return authenticationModel;
		}
	}
}
