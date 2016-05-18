using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using System.Collections.Concurrent;

namespace Gonzigonz.SampleApp.Data.Repositories
{
	public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository
    {
		static ConcurrentDictionary<int, TodoItem> _todosData =
			  new ConcurrentDictionary<int, TodoItem>();

		public TodoItemRepository() :
			base(_todosData)
		{
			Create(new TodoItem { Name = "My first Item" });
		}
    }
}
