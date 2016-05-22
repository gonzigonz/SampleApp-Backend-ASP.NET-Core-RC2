using Gonzigonz.SampleApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gonzigonz.SampleApp.Data.Context
{
	public class AppDbContext : DbContext
    {

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<TodoItem> TodoItems { get; set; }

	}
}
