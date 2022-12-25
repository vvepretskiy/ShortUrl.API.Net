using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortUrl.Interfaces.Repositories;
using ShortUrl.Interfaces.Services;
using ShortUrl.Services;
using ShortUrl.Repositories;

namespace ShortUrl.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddCors();
			services.AddScoped<IUrlRepository, UrlRepository>();
			services.AddScoped<IAnalyticRepository, AnalyticRepository>();
			services.AddScoped<IUrlService, UrlService>();
			services.AddScoped<IAnalyticService, AnalyticService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(x => x
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
													//.WithOrigins("https://http://localhost:3009/")); // Allow only this origin can also have multiple origins separated with comma
				.AllowCredentials()); // allow credentials

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
