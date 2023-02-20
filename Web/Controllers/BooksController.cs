using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly ApiClient _client;
		private static string SessionKey = "BooksToCheckoutIds";

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

		private static List<BookInCartModel> GetBooksIds(ISession session, string key)
		{
			var value = session.GetString(key);

			return value == null ? new List<BookInCartModel>() : JsonConvert.DeserializeObject<List<BookInCartModel>>(value);
		}

		[HttpPost]
        [Authorize]
		public async Task<bool> AddToCart(BookModel book)
		{
			var currentSession = HttpContext.Session;

			if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKey)))
			{
				List<BookInCartModel> existedBooksIds = GetBooksIds(currentSession, SessionKey);

				if (!existedBooksIds.Any(x => x.Id == book.Id))
				{
					existedBooksIds.Add(new BookInCartModel
					{
						Id = book.Id,
						Quantity = book.Quantity
					});
				}

				SetBooksIds(currentSession, SessionKey, existedBooksIds);
			}
			else
			{
				List<BookInCartModel> newBooksIds = new List<BookInCartModel>();

				newBooksIds.Add(new BookInCartModel
				{
					Id = book.Id,
					Quantity = book.Quantity
				});

				SetBooksIds(currentSession, SessionKey, newBooksIds);
			}

			return true;
		}

		private static void SetBooksIds(ISession session, string key, List<BookInCartModel> booksIds)
		{
			session.SetString(key, JsonConvert.SerializeObject(booksIds));
		}
	}
}
