using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly ApiClient _client;

        public BooksController(ApiClient client)
        {
            _client = client;
        }

        [Authorize(Policy = "RequireAdmin")]
		public IActionResult Index()
		{
			List<BookModel> books = new List<BookModel>();

			return View(books);
		}

        public async Task<IActionResult> One(string id)
        {
            BookModel book = _client.GetBook(id).Result;

            return View(book);
        }
    }
}
