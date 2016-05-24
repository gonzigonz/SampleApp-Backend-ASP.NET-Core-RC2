using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	public class TodoController : Controller
	{
		private ITodoItemRepository _todoRepo;
		private IUnitOfWork _unitOfWork;

		public TodoController(ITodoItemRepository todoItemRepository, IUnitOfWork unitOfWork)
		{
			_todoRepo = todoItemRepository;
			_unitOfWork = unitOfWork;
		}

		[HttpGet("")]
		public JsonResult Get()
		{
			return Json(_todoRepo.ReadAll());
		}

		[HttpGet("{id:int:min(1)}", Name = "GetTodo")]
		public JsonResult Get(int id)
		{
			var itemFromDatabase = _todoRepo.Read(i => i.Id == id).FirstOrDefault();
			if (itemFromDatabase.Id != id)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}

			return Json(itemFromDatabase);
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
				_todoRepo.Create(newTodoItem);
				_unitOfWork.SaveChangesAsync();
			}

			Response.StatusCode = (int)HttpStatusCode.Created;
			Response.Headers["Location"] = $"/api/Todo/{newTodoItem.Id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
			return Json("Item saved successfully");
		}

		[HttpPut("{id:int:min(1)}")]
		public JsonResult Put(int id, [FromBody] TodoItem updatedTodoItem)
		{
			if (updatedTodoItem.Id != id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("The item id does not match the id supplied in the url");
			}

			var itemFromDatabaseId = _todoRepo
				.Read(i => i.Id == id)
				.Select(i => i.Id)
				.FirstOrDefault();
			if (itemFromDatabaseId != id)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}
			else
			{
				_todoRepo.Update(updatedTodoItem);
				_unitOfWork.SaveChangesAsync();
			}

			Response.Headers["Location"] = $"/api/Todo/{id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
			return Json("Item updated successfully");
		}

		[HttpDelete("{id:int:min(1)}")]
		public JsonResult Delete(int id)
		{
			var itemToDelete = _todoRepo.Read(i => i.Id == id).FirstOrDefault();
			if (itemToDelete == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}
			else
			{
				_todoRepo.Delete(id);
			}

			return Json($"The item with the id of {id} was deleted");
		}	
	}
}
