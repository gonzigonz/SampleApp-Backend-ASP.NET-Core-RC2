using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gonzigonz.SampleApp.Data.Context
{
	public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
	{

		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.HasDefaultSchema("identity");
			base.OnModelCreating(builder);
		}

	}
}
