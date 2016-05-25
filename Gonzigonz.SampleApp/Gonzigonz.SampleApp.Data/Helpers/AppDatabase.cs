using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ASP.NetCore.Empty.Data
{
	public static class AppDatabase
	{
		public static async void Initialize(IServiceProvider serviceProvider)
		{
			// Use Migrations
			using (var context = new AppDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
			{
				await context.Database.MigrateAsync();
			}
		}
		public static async void InitializeForDevelopment(IServiceProvider serviceProvider)
		{
			// Use Auto Database Generation without the need for Migrations
			using (var context = new AppDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
			{
				await context.Database.EnsureCreatedAsync();
			}

			// Seed Dev Data
			var todoRepo = serviceProvider.GetRequiredService<ITodoItemRepository>();
			var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

			if (await todoRepo.ReadAll().AnyAsync())
			{
				return; // Database has already been seeded
			};

			todoRepo.CreateBulk(SeedTodoItemsData());
			unitOfWork.SaveChangesAsync();

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