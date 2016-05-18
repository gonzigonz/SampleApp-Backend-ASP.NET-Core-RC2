using System;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Gonzigonz.SampleApp.Domain;

namespace Gonzigonz.SampleApp.Data.Repositories
{
	public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{
		private ConcurrentDictionary<int, TEntity> _data;

		public RepositoryBase(ConcurrentDictionary<int, TEntity> data)
		{
			_data = data;
		}

		public void Create(TEntity entity)
		{
			entity.Id = _data.Count;
			entity.CreatedTime = DateTime.UtcNow;
			entity.ModifiedTime = DateTime.UtcNow;
			_data[entity.Id] = entity;
		}

		public ICollection<TEntity> ReadAll()
		{
			return _data.Values;
		}

		public TEntity ReadById(int Id)
		{
			TEntity entityToReturn;
			_data.TryGetValue(Id, out entityToReturn);
			return entityToReturn;
		}

		public void Update(TEntity entityToUpdate)
		{
			entityToUpdate.ModifiedTime = DateTime.UtcNow;
			_data[entityToUpdate.Id] = entityToUpdate;
		}

		public void Delete(int id)
		{
			var entityToDelete = ReadById(id);
			Delete(entityToDelete);
		}

		public void Delete(TEntity entityToDelete)
		{
			_data.TryRemove(entityToDelete.Id, out entityToDelete);
		}
	}
}
