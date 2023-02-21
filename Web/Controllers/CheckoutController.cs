using Application.Interfaces;
using Application.Models;
using Infrastructure.Email;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApiClient _client;
        private readonly EmailService _emaiLService;

        public CheckoutController(ApiClient client, EmailService service)
        {
            _client = client;
            _emaiLService = service;
        }

        public IActionResult Index()
        {
            var booksIds = GetBooksIds(HttpContext.Session, SessionKey);

            List<BookModel> books = _client.GetBooksByIds(booksIds.Select(x => x.Id).ToList()).Result;

            return View(books);
        }

        public bool FinishCheckout()
        {
            var currentSession = HttpContext.Session;

            currentSession.Remove(SessionKey);

            _emaiLService.SendShopingConfirmationMail(User.Identity.GetUserName());
            return true;
        }

        private static string SessionKey = "BooksToCheckoutIds";

        private static List<BookInCartModel> GetBooksIds(ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? new List<BookInCartModel>() : JsonConvert.DeserializeObject<List<BookInCartModel>>(value);
        }
    }
}
