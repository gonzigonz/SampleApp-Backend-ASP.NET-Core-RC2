using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	public class TodoController : Controller
	{
		private ITodoItemRepository _todoRepo;

		public TodoController(ITodoItemRepository todoItemRepository)
		{
			_todoRepo = todoItemRepository;
		}

		[HttpGet("")]
		public JsonResult Get()
		{
			return Json(_todoRepo.ReadAll());
		}

		[HttpGet("{id}", Name = "GetTodo")]
		public JsonResult Get(int id)
		{
			var item = _todoRepo.ReadById(id);
			if (item == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}
			return Json(item);
		}

		[HttpPost("")]
		public JsonResult Post([FromBody] TodoItem newTodoItem)
		{
			if (newTodoItem == null)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("No item was passed to create");
			}
			else
			{
				Response.StatusCode = (int)HttpStatusCode.Created;
				Response.Headers["Location"] = $"/api/Todo/{newTodoItem.Id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
				return Json(_todoRepo.Create(newTodoItem));
			}
		}

		[HttpPut("{id}")]
		public JsonResult Put(int id, [FromBody] TodoItem updatedTodoItem)
		{
			if (updatedTodoItem == null || updatedTodoItem.Id != id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("Invalid item was passed or the item id does not match the id supplied in the url");
			}

			var todo = _todoRepo.ReadById(id);
			if (todo == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}

			Response.Headers["Location"] = $"/api/Todo/{updatedTodoItem.Id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
			return Json(_todoRepo.Update(updatedTodoItem));
		}

		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			var itemToDelete = _todoRepo.ReadById(id);
			if (itemToDelete == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}
			_todoRepo.Delete(itemToDelete);
			return Json($"The item with the id of {itemToDelete.Id} was deleted");
		}	
	}
}
