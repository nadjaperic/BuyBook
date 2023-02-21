using Application.Models;
using Infrastructure.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
	[AllowAnonymous]
	public class BooksController : Controller
	{
		private readonly ApiClient _client;
		private static string SessionKey = "BooksToCheckoutIds";
		private readonly CurrencyService _currencyService;

		public BooksController(ApiClient client, CurrencyService currencyService)
        {
            _client = client;
			_currencyService = currencyService;
        }

		public IActionResult Index()
		{
			List<BookModel> books = _client.GetAllBooks().Result;

			return View(books);
		}

        public IActionResult SearchResult(string searchTerm)
        {
            List<BookModel> books = _client.SearchBooks(searchTerm).Result;

            return View(books);
        }

        public async Task<IActionResult> One(int id)
        {
			BooksPageModel booksPageModel = new BooksPageModel();

            booksPageModel.Book = _client.GetBook(id).Result;
			booksPageModel.ExchangeRateData = _currencyService.ListAllCurrencies();

            return View(booksPageModel);
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
