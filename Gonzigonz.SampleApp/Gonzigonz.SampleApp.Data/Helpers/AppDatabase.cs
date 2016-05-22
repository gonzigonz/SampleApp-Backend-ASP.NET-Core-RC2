using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.Domain;
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
			using (var context = new AppDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
			{
				await context.Database.MigrateAsync();
			}
		}
		public static async void InitializeForDevelopment(IServiceProvider serviceProvider)
		{
			using (var context = new AppDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
			{

				await context.Database.EnsureCreatedAsync();

				if (await context.TodoItems.AnyAsync())
				{
					return;   // DB has already been seeded
				}

				context.TodoItems.AddRange(SeedTodoItemsData());

				await context.SaveChangesAsync();
			}
		}

		static IList<TodoItem> SeedTodoItemsData()
		{
			return new List<TodoItem> {
				new TodoItem
				{
					Name = "This is my first item on the list",
					IsComplete = false,
					CreatedTime = DateTime.UtcNow,
					ModifiedTime = DateTime.UtcNow
				},

				new TodoItem
				{
					Name = "And here is the second one",
					IsComplete = false,
					CreatedTime = DateTime.UtcNow,
					ModifiedTime = DateTime.UtcNow
				},

				new TodoItem
				{
					Name = "Oh don't forget the third one",
					IsComplete = false,
					CreatedTime = DateTime.UtcNow,
					ModifiedTime = DateTime.UtcNow
				},

				new TodoItem
				{
					Name = "And this is the very last one",
					IsComplete = false,
					CreatedTime = DateTime.UtcNow,
					ModifiedTime = DateTime.UtcNow
				}
			};
		}
	}
}
