using Gonzigonz.SampleApp.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApp.Controllers.Web
{
	public class AppController : Controller
    {
		private AppDbContext _context;

		public AppController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var todoItems = await _context.TodoItems.ToListAsync();
			return View(todoItems);
		}
	}
}
