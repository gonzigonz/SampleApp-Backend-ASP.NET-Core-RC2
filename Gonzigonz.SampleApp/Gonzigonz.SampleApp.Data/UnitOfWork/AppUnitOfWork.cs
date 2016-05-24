using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.RepositoryInterfaces;

namespace Gonzigonz.SampleApp.Data.UnitOfWork
{
	public class AppUnitOfWork : IUnitOfWork
    {
		private AppDbContext _context;

		public AppUnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public void SaveChangesAsync()
		{
			_context.SaveChangesAsync();
		}
	}
}
