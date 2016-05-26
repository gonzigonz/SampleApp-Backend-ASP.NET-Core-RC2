using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Gonzigonz.SampleApp.Data
{
	public static class AppDatabase
	{
		public static async void InitializeDatabase(IServiceProvider serviceProvider, bool isProduction)
		{

			// Create App database tables
			using (var context = serviceProvider.GetRequiredService<AppDbContext>())
			{
				if (isProduction)
				{
					// FOR PROD - Use Migrations
					await context.Database.MigrateAsync();
				}
				else
				{
					// FOR NON PROD - Use Auto Database Generation without the need for Migrations
					await context.Database.EnsureCreatedAsync();

					// Seed Development Data
					var todoRepo = serviceProvider.GetRequiredService<ITodoItemRepository>();
					var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

					if (await todoRepo.ReadAll().AnyAsync())
					{
						return; // Database has already been seeded
					};

					todoRepo.CreateBulk(SeedTodoItemsData());
					await unitOfWork.SaveChangesAsync();
				}
			}

		}

		public static async void EnsureIdentityDatabaseExists(IServiceProvider serviceProvider, bool isProduction)
		{
			using (var identityContext = new AppIdentityDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<AppIdentityDbContext>>()))
			{
				// Ensure the identity tables are created and up-to-date
				if (isProduction)
				{
					await identityContext.Database.MigrateAsync();
				}
				else
				{
					await identityContext.Database.EnsureCreatedAsync();
				}

				// Ensure the admin user exists
				var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
				var adminUser = new IdentityUser { UserName = "/" };

				if (await userManager.FindByNameAsync("Admin") == null)
				{
					var result = await userManager.CreateAsync(adminUser, "admin");
					//if (!result.Succeeded)
					//{
					//	// TODO: Gonz implement correct logging!
					//	Console.WriteLine("Could not create default 'Admin' user");
					//	foreach (var error in result.Errors)
					//	{
					//		Console.WriteLine($"{error.Code}: {error.Description}");
					//	}
					//}
				};
			}
		}

		static IList<TodoItem> SeedTodoItemsData()
		{
			return new List<TodoItem> {
				new TodoItem
				{
					Name = "This is my first item on the list",
					IsComplete = false
				},

				new TodoItem
				{
					Name = "And here is the second one",
					IsComplete = false
				},

				new TodoItem
				{
					Name = "Oh don't forget the third one",
					IsComplete = false
				},

				new TodoItem
				{
					Name = "And this is the very last one",
					IsComplete = false
				}
			};
		}
	}
}