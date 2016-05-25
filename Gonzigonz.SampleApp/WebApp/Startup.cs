using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Gonzigonz.SampleApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ASP.NetCore.Empty.Data;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Gonzigonz.SampleApp.Data.UnitOfWork;
using Gonzigonz.SampleApp.Data.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
				services.AddDbContext<AppIdentityDbContext>(options =>
					options.UseInMemoryDatabase());
			}
			else
			{
				services.AddDbContext<AppDbContext>(options =>
					options.UseSqlServer(
						"Server=localhost;Database=GonzigonzSampleApp;Trusted_Connection=true;MultipleActiveResultSets=true;",
						b => b.MigrationsAssembly("WebApp"))
				);
				services.AddDbContext<AppIdentityDbContext>(options =>
					options.UseSqlServer(
						"Server=localhost;Database=GonzigonzSampleApp;Trusted_Connection=true;MultipleActiveResultSets=true;",
						b => b.MigrationsAssembly("WebApp"))
				);
			}

			services.AddIdentity<IdentityUser, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = false;
				config.Password.RequiredLength = 5;
				config.Password.RequireDigit = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
			})
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddMvc();

			// All other services.
			services.AddScoped<IUnitOfWork, AppUnitOfWork>();
			services.AddScoped<ITodoItemRepository, TodoItemRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			// Before we setup the pipeline, get the database up
			if (_hostingEnvironment.IsDevelopment())
			{
				AppDatabase.InitializeForDevelopment(app.ApplicationServices);
				app.UseRuntimeInfoPage();
				app.UseBrowserLink();
			}
			else
			{
				AppDatabase.Initialize(app.ApplicationServices);
			}

			// Error Handling and Diagnostics
			app.UseDeveloperExceptionPage();

			// Shows database operation failures. Good incase you've missed a migration.
			app.UseDatabaseErrorPage();

			// Use Asp.Net Identity
			app.UseIdentity();

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
