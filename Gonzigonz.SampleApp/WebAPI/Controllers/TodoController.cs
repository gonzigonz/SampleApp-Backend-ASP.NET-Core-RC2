using Gonzigonz.SampleApp.Domain;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
		public async Task<JsonResult> Get()
		{
			return Json(await _todoRepo
				.ReadAll()
				.ToListAsync());
		}

		[HttpGet("{id:int:min(1)}", Name = "GetTodo")]
		public async Task<JsonResult> Get(int id)
		{
			var itemFromDatabase = await _todoRepo
				.Read(i => i.Id == id)
				.FirstOrDefaultAsync();

			if (itemFromDatabase == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}

			return Json(itemFromDatabase);
		}

		[HttpPost("")]
		public async Task<JsonResult> Post([FromBody] TodoItem newTodoItem)
		{
			//TODO: Validate model coming in better than this!
			if (newTodoItem != null)
			{
				_todoRepo.Create(newTodoItem);
				var changes = await _unitOfWork.SaveChangesAsync();

				Response.StatusCode = (int)HttpStatusCode.Created;
				Response.Headers["Location"] = $"/api/Todo/{newTodoItem.Id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
				return Json("Item saved successfully");
			}
			else
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("No item was passed to create");
			}
			
		}

		[HttpPut("{id:int:min(1)}")]
		public async Task<JsonResult> Put(int id, [FromBody] TodoItem updatedTodoItem)
		{
			if (id != updatedTodoItem.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("The item id does not match the id supplied in the url");
			}

			//TODO: Vaidate model coming in
			if (updatedTodoItem !=null)
			{
				var itemToUpdate = await _todoRepo
				.Read(i => i.Id == id)
				.FirstOrDefaultAsync();

				if (itemToUpdate != null)
				{
					itemToUpdate.Name = updatedTodoItem.Name;
					itemToUpdate.Details = updatedTodoItem.Details;
					itemToUpdate.IsComplete = updatedTodoItem.IsComplete;

					_todoRepo.Update(itemToUpdate);
					await _unitOfWork.SaveChangesAsync();

					Response.Headers["Location"] = $"/api/Todo/{id}"; //TODO: Gonz do this properly... all api items should provide valid urls.
					return Json("Item updated successfully");
				}
				else
				{
					Response.StatusCode = (int)HttpStatusCode.NotFound;
					return Json($"No item was found with an id of {id}");
				}
			}
			else
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("No item was passed to update");
			}

		}

		[HttpDelete("{id:int:min(1)}")]
		public async Task<JsonResult> Delete(int id)
		{
			var itemToDelete = await _todoRepo
				.Read(i => i.Id == id)
				.FirstOrDefaultAsync();

			if (itemToDelete == null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json($"No item was found with an id of {id}");
			}
			else
			{
				_todoRepo.Delete(itemToDelete);
				await _unitOfWork.SaveChangesAsync();
			}

			return Json($"The item with the id of {id} was deleted");
		}
	}
}
