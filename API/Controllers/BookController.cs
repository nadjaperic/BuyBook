using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("getAllBooks")]
        public List<BookModel> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }


        [HttpGet("getLastTenBooks")]
        public List<BookModel> GetLastTenBooks()
        {
            return _bookService.GetLastTenBooks();
        }

        [HttpGet("searchBooks")]
        public List<BookModel> SearchBooks(string searchTerm)
        {
            return _bookService.SearchBooks(searchTerm);
        }

        [HttpPost("getBooksByIds")]
        public List<BookModel> GetBooksByIds(List<int> ids)
        {
            return _bookService.GetBooksByIds(ids);
        }


        [HttpGet("getFeaturedBooks")]
        public List<BookModel> GetFeaturedBooks()
        {
            return _bookService.GetFeaturedBooks();
        }

        [HttpGet("getBook/{id}")]
        public BookModel GetBook(int id)
        {
            return _bookService.GetBookById(id);
        }
    }
}