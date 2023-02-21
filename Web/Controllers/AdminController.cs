using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;
using Web.Service;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    public class AdminController : Controller
	{
        private readonly ApiClient _client;
        private readonly UserManagementService _userService;

        public AdminController(ApiClient client, UserManagementService userService)
        {
            _client = client;
            _userService = userService;
        }

        public IActionResult Index()
		{
			AdminPanelModel model = new AdminPanelModel();

			model.Books = _client.GetAllBooks().Result;

			model.AllAuthors= _client.GetAllAuthors().Result;
			model.AllPublishers = _client.GetAllPublishers().Result;
			model.AllCategories = _client.GetAllCategories().Result;
            model.AllAdmins = _userService.GetAllAdmins();

			return View(model);
		}

        public bool AddAdmin(UserModel admin)
        {
            return _userService.AddNewAdmin(admin);
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

        public BookModel GetBook(int id)
        {
            return _client.GetBook(id).Result;
        }

        public bool UpdateBook(BookModel model)
        {
            return _client.UpdateBook(model).Result;
        }

        public bool DeleteBook(int id)
        {
            return _client.DeleteBook(id).Result;
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

        public bool DeleteAdmin(string id)
        {
            return _userService.DeleteAdmin(id);
        }
    }
}
