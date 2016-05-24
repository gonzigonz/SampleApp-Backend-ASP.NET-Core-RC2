using System;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Gonzigonz.SampleApp.Domain;

namespace Gonzigonz.SampleApp.Data.Repositories
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity, int> 
		where TEntity : EntityBase
	{
		private DbContext _context;
		private DbSet<TEntity> _dbSet;
		private object _tEntity;

		public RepositoryBase(DbContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		protected DbContext Context
		{
			get { return _context; }
		}

		// CREATE
		public void Create(TEntity entityToAdd)
		{
			_dbSet.Add(entityToAdd);
		}

		// READ
		public IQueryable<TEntity> ReadAll()
		{
			return _dbSet;
		}
		public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> filter)
		{
			return _dbSet.Where(filter);
		}

		// UPDATE
		public void Update(TEntity entityToUpdate)
		{
			_dbSet.Attach(entityToUpdate);
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		// DETETE
		public virtual async void Delete(int id)
		{
			var entityToDelete = await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
			Delete(entityToDelete);
		}
		public void Delete(TEntity entityToDelete)
		{
			if (_context.Entry(entityToDelete).State == EntityState.Deleted)
			{
				_dbSet.Attach(entityToDelete);
			}
			_dbSet.Remove(entityToDelete);
		}
	}
}
