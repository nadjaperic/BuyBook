using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class BooksController : Controller
	{

		[Authorize(Policy = "RequireAdmin")]
		public IActionResult Index()
		{
			List<BookModel> books = new List<BookModel>();

			return View(books);
		}
	}
}
