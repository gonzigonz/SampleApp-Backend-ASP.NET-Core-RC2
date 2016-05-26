using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Gonzigonz.SampleApp.Data.Repositories;
using Newtonsoft.Json.Serialization;
using Gonzigonz.SampleApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using ASP.NetCore.Empty.Data;
using Gonzigonz.SampleApp.Data.UnitOfWork;

namespace WebAPI
{
	public class Startup
    {
		private IHostingEnvironment _hostingEnvironment;

		public Startup(IHostingEnvironment hostingEnvironment)
        {
			_hostingEnvironment = hostingEnvironment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_hostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// Add Cors service.
			services.AddCors(options => options
				.AddPolicy("AllowAll", p => p
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
				)
			);

			// Add framework services
			services.AddDbContext<AppDbContext>(options =>
				options.UseInMemoryDatabase()
			);
			services.AddMvc()
				.AddJsonOptions(options => {
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});

			// All other services.
			services.AddScoped<IUnitOfWork, AppUnitOfWork>();
			services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			// Before we setup the pipeline, get the database up
			AppDatabase.InitializeDatabase(app.ApplicationServices, 
				isProduction: false);
			app.UseRuntimeInfoPage();
			app.UseDeveloperExceptionPage();
			app.UseBrowserLink();

			// Allow any client from any origin to use our API
			app.UseCors("AllowAll");

			app.UseStatusCodePages();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseMvc();
        }
    }
}
