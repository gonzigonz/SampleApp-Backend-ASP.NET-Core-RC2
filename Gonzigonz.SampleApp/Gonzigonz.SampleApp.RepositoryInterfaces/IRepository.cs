using Gonzigonz.SampleApp.Domain;
using System.Collections.Generic;

namespace Gonzigonz.SampleApp.RepositoryInterfaces
{
	public interface IRepository<TEntity> where TEntity : EntityBase
    {
		TEntity Create(TEntity entity);
		ICollection<TEntity> ReadAll();
		TEntity ReadById(int Id);
		TEntity Update(TEntity entityToUpdate);
		void Delete(int id);
		void Delete(TEntity entityToDelete);
	}
}
