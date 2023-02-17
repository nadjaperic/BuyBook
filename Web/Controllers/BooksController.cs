using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class BooksController : Controller
	{
		public IActionResult Index()
		{
			List<BookModel> books = new List<BookModel>();

			return View(books);
		}
	}
}
