using Gonzigonz.SampleApp.Domain;
using System.Collections.Generic;

namespace Gonzigonz.SampleApp.RepositoryInterfaces
{
	public interface IRepository<TEntity> where TEntity : EntityBase
    {
		void Create(TEntity entity);
		ICollection<TEntity> ReadAll();
		TEntity ReadById(int Id);
		void Update(TEntity entityToUpdate);
		void Delete(int id);
		void Delete(TEntity entityToDelete);
	}
}
