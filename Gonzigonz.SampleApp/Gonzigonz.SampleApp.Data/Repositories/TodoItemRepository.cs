using Gonzigonz.SampleApp.Data.Context;
using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;

namespace Gonzigonz.SampleApp.Data.Repositories
{
	public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository
    {
		public TodoItemRepository(AppDbContext context) :
			base(context)
		{
		}
    }
}
