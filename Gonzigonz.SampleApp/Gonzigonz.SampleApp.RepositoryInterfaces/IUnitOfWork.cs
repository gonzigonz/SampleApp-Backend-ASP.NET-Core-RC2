using System.Threading.Tasks;

namespace Gonzigonz.SampleApp.RepositoryInterfaces
{
	public interface IUnitOfWork
    {
		Task<int> SaveChangesAsync();
    }
}
