using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
	[AllowAnonymous]
	public class AdminController : Controller
	{
        private readonly ApiClient _client;

        public AdminController(ApiClient client)
        {
            _client = client;
        }

        public IActionResult Index()
		{
			AdminPanelModel model = new AdminPanelModel();

			model.Books = _client.GetAllBooks().Result;

			model.AllAuthors= _client.GetAllAuthors().Result;
			model.AllPublishers = _client.GetAllPublishers().Result;
			model.AllCategories = _client.GetAllCategories().Result;

			return View(model);
		}

		public CategoryModel CreateCategory(CategoryModel model)
		{
			return _client.CreateCategory(model).Result;
		}
        public PublisherModel AddPublisher(PublisherModel model)
        {
            return _client.AddPublisher(model).Result;
        }

        public AuthorModel AddAuthor(AuthorModel model)
        {
            return _client.AddAuthor(model).Result;
        }

        public BookModel AddBook(BookModel model)
        {
            return _client.AddBook(model).Result;
        }

        public bool DeleteCategory(int id)
        {
            return _client.DeleteCategory(id).Result;
        }

        public bool DeletePublisher(int id)
        {
            return _client.DeletePublisher(id).Result;
        }

        public bool DeleteAuthor(int id)
        {
            return _client.DeleteAuthor(id).Result;
        }
    }
}
