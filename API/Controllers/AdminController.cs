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

        [HttpDelete("deleteBook/{id}")]
        public bool DeleteBook(int id)
        {
            return _bookService.DeleteBook(id);
        }

        [HttpPut("updateBook")]
        public UpdateBookModel UpdateBook(UpdateBookModel model)
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
    }
}
