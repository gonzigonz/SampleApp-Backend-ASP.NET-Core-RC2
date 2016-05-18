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
				return Json(null);
			}
			return Json(item);
		}
	}
}
