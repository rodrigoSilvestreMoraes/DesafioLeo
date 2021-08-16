using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Config;
using Test.DesafioLeo.Core.Domain.Context.Authentication.Services;
using Test.DesafioLeo.Core.Infrastructure.Repository;
using Test.DesafioLeo.Middleware;

namespace Test.DesafioLeo
{
	[ExcludeFromCodeCoverage]
	public class Startup
	{

		static AppSettings _appSettings;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();
			services.AddControllers().AddNewtonsoftJson();

			// configure strongly typed settings object
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

			_appSettings = new AppSettings();
			Configuration.Bind("AppSettings", _appSettings);

			// configure DI for application services
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton(_appSettings);

			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.DesafioLeo", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.DesafioLeo v1"));
			}

			app.UseRouting();

			// global cors policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			// custom jwt auth middleware
			app.UseMiddleware<JwtMiddleware>();

			app.UseEndpoints(x => x.MapControllers());
		}
	}
}
