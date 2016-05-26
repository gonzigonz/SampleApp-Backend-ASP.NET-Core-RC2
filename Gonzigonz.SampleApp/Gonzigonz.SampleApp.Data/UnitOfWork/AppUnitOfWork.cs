using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gonzigonz.SampleApp.Data.UnitOfWork
{
	public class AppUnitOfWork : IUnitOfWork
	{
		private AppDbContext _context;

		public AppUnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public async Task<int> SaveChangesAsync()
		{
			foreach (var entry in _context.ChangeTracker.Entries()
					   .Where(
					   e => e.Entity is EntityBase &&
					   (e.State == EntityState.Added) ||
					   (e.State == EntityState.Modified))
			)
			{
				var e = (EntityBase)entry.Entity;
				if (entry.State == EntityState.Added)
				{
					e.CreatedTimeUTC = DateTime.UtcNow;
				}
				e.ModifiedTimeUTC = DateTime.UtcNow;
			}

			return await _context.SaveChangesAsync();
		}
	}
}
