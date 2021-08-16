using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using Test.DesafioLeo.Core.Domain.Context.Authentication.SimpleModel;

namespace Test.DesafioLeo.Filters
{
    /// <summary>
    /// Classe que serve de filter, deve ser testado em um conjunto de testes de componente ou integrado.
    /// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [ExcludeFromCodeCoverage]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (AuthenticationModel)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
