using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Config;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Services;

namespace Test.DesafioLeo.Middleware
{
    /// <summary>
    /// Usei o exemplo do site : https://jasonwatmore.com/post/2021/04/30/net-5-jwt-authentication-tutorial-with-example-api
    /// Classe que serve de middleware, deve ser testado em um conjunto de testes de componente ou integrado.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class JwtMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, authenticationService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthenticationService authenticationService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "user").Value;

                // attach user to context on successful jwt validation
                context.Items["User"] = authenticationService.GetUser(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
