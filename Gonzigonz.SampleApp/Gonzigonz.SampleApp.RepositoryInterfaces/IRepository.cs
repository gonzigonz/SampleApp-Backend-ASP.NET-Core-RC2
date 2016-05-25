using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gonzigonz.SampleApp.RepositoryInterfaces
{
	public interface IRepository<TEntity, TId> 
		where TEntity : class
		where TId : struct
    {
		// CREATE
		void Create(TEntity entity);
		void CreateBulk(IEnumerable<TEntity> entityCollection);

		// READ
		IQueryable<TEntity> ReadAll();
		IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> filter);

		// UPDATE
		void Update(TEntity entityToUpdate);

		// DELETE
		void Delete(TId id);
		void Delete(TEntity entityToDelete);
	}
}