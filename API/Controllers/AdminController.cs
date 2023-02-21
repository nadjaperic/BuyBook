using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;

        public AdminController(IBookService bookService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _publisherService = publisherService;
        }

        [HttpPost("addBook")]
        public BookModel AddBook(BookModel model)
        {
            return _bookService.AddBook(model);
        }


        [HttpPost("addAuthor")]
        public AuthorModel AddAuthor(AuthorModel model)
        {
            return _bookService.AddAuthor(model);
        }

        [HttpDelete("deleteAuthor")]
        public bool DeleteAuthor(int id)
        {
            return _bookService.DeleteAuthor(id);
        }

        [HttpDelete("deleteBook")]
        public bool DeleteBook(int id)
        {
            return _bookService.DeleteBook(id);
        }

        [HttpPut("updateBook")]
        public bool UpdateBook(UpdateBookModel model)
        {
            return _bookService.UpdateBook(model);
        }

        [HttpPost("addPublisher")]
        public PublisherModel AddPublisher(PublisherModel model)
        {
            return _publisherService.AddPublisher(model);
        }

        [HttpDelete("deletePublisher")]
        public bool DeletePublisher(int id)
        {
            return _publisherService.DeletePublisher(id);
        }

        [HttpPost("createCategory")]
        public CategoryModel CreateCategory(CategoryModel model)
        {
            return _bookService.CreateCategory(model);
        }

        [HttpDelete("deleteCategory")]
        public bool DeleteCategory(int id)
        {
            return _bookService.DeleteCategory(id);
        }

        [HttpGet("getAllAuthors")]
        public List<AuthorModel> GetAllAuthors()
        {
            return _bookService.GetAllAuthors();
        }

        [HttpGet("getAllPublishers")]
        public List<PublisherModel> GetAllPublishers()
        {
            return _publisherService.GetAllPublishers();
        }

        [HttpGet("getAllCategories")]
        public List<CategoryModel> GetAllCategories()
        {
            return _bookService.GetAllCategories();
        }

    }
}
