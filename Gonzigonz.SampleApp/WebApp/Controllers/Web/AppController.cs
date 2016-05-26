using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gonzigonz.SampleApp.RepositoryInterfaces;
using Gonzigonz.SampleApp.Domain;

namespace WebApp.Controllers
{
	public class AppController : Controller
    {
		private readonly ITodoItemRepository _todoItemRepo;
		private readonly IUnitOfWork _unitOfWork;

		public AppController(ITodoItemRepository todoItemRepository, IUnitOfWork unitOfWork)
		{
			_todoItemRepo = todoItemRepository;
			_unitOfWork = unitOfWork;
		}

		// GET: App
		public async Task<IActionResult> Index()
        {
            return View(await _todoItemRepo
				.ReadAll()
				.ToListAsync());
        }

        // GET: App/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var todoItem = await _todoItemRepo
				.Read(item => item.Id == id)
				.SingleOrDefaultAsync();

			if (todoItem == null)
			{
				return NotFound();
			}

			return View(todoItem);
		}

        // GET: App/Create
        public IActionResult Create()
        {
            return View();
        }

		// POST: App/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Details,IsComplete,Name")] TodoItem todoItem)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_todoItemRepo.Create(todoItem);
					var changes = await _unitOfWork.SaveChangesAsync();
					return RedirectToAction("Index");
				}
				else
				{
					throw new Exception("ModelState Invalid");
				}
			}
			catch
			{
				return View(todoItem);
			}
		}

		// GET: App/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var todoItem = await _todoItemRepo
				.Read(item => item.Id == id)
				.SingleOrDefaultAsync();

			if (todoItem == null)
			{
				return NotFound();
			}

			return View(todoItem);
        }

		// POST: App/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Details,IsComplete,Name")] TodoItem updatedTodoItem)
		{
			if (id != updatedTodoItem.Id)
			{
				return NotFound();
			}

			try
			{
				if (ModelState.IsValid)
				{
					var itemToUpdate = await _todoItemRepo
						.Read(item => item.Id == id)
						.SingleOrDefaultAsync();

					if (itemToUpdate != null)
					{
						itemToUpdate.Name = updatedTodoItem.Name;
						itemToUpdate.Details = updatedTodoItem.Details;
						itemToUpdate.IsComplete = updatedTodoItem.IsComplete;

						_todoItemRepo.Update(itemToUpdate);
						await _unitOfWork.SaveChangesAsync();
						return RedirectToAction("Index");
					}
					else
					{
						return NotFound();
					}
				}
				else
				{
					throw new Exception("ModelState Invalid");
				}
				
			}
			catch
			{
				return View(updatedTodoItem);
			}

		}

		// GET: App/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var todoItem = await _todoItemRepo
				.Read(item => item.Id == id)
				.SingleOrDefaultAsync();

			if (todoItem == null)
			{
				return NotFound();
			}

			return View(todoItem);
		}

		// POST: App/DeleteConfirmed/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var todoItem = await _todoItemRepo
				.Read(item => item.Id == id)
				.SingleOrDefaultAsync();

			if (todoItem == null)
			{
				return NotFound();
			}
			else
			{
				_todoItemRepo.Delete(todoItem);
				await _unitOfWork.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}
	}
}