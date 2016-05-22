using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Gonzigonz.SampleApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ASP.NetCore.Empty.Data;

namespace WebApp
{
	public class Startup
	{
		private IHostingEnvironment _hostingEnvironment;

		public Startup(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services
			if (_hostingEnvironment.IsDevelopment())
			{
				services.AddDbContext<AppDbContext>(options =>
				options.UseInMemoryDatabase());
			}
			else
			{
				services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer("Server=localhost;Database=GonzigonzSampleApp;Trusted_Connection=true;MultipleActiveResultSets=true;"));

			}
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			// Before we setup the pipeline, get the database up
			if (_hostingEnvironment.IsDevelopment())
			{
				AppDatabase.InitializeForDevelopment(app.ApplicationServices);
				app.UseDeveloperExceptionPage();
			}
			else
			{
				AppDatabase.Initialize(app.ApplicationServices);
			}
			// Error Handling and Diagnostics
			app.UseRuntimeInfoPage();
			app.UseDeveloperExceptionPage();
			app.UseDatabaseErrorPage(); // Shows database operation failures. Good incase you've missed a migration.
			app.UseBrowserLink();

			// Allows any static content in wwwroot to be servered
			app.UseStaticFiles();

			// Config MVC6
			app.UseMvc(config =>
			{
				config.MapRoute(
					name: "default",
					template: "{controller=App}/{action=Index}/{id?}");
			});
		}
	}
}
