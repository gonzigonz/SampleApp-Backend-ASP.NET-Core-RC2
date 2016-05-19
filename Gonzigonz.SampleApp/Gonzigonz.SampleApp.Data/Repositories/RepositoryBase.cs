using System;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Gonzigonz.SampleApp.Domain;

namespace Gonzigonz.SampleApp.Data.Repositories
{
	public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{
		static int _nextIndex = 1;
		private ConcurrentDictionary<int, TEntity> _data;

		public RepositoryBase(ConcurrentDictionary<int, TEntity> data)
		{
			_data = data;
		}

		public TEntity Create(TEntity entity)
		{
			entity.Id = _nextIndex;
			entity.CreatedTime = DateTime.UtcNow;
			entity.ModifiedTime = DateTime.UtcNow;
			_data[entity.Id] = entity;
			_nextIndex++;
			return _data[entity.Id];
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

		public TEntity Update(TEntity entityToUpdate)
		{
			entityToUpdate.ModifiedTime = DateTime.UtcNow;
			_data[entityToUpdate.Id] = entityToUpdate;
			return _data[entityToUpdate.Id];
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
