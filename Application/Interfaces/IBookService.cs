using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IBookService
    {
        BookModel AddBook(BookModel book);
        bool DeleteBook(int id);
        List<BookModel> GetAllBooks();
        BookModel GetBookById(int id);
        bool UpdateBook(UpdateBookModel model);
        CategoryModel CreateCategory(CategoryModel category);
        bool DeleteCategory(int id);
        AuthorModel AddAuthor(AuthorModel model);
        bool DeleteAuthor(int id);
        List<BookModel> GetLastTenBooks();
        List<BookModel> GetFeaturedBooks();
        List<AuthorModel> GetAllAuthors();
        List<CategoryModel> GetAllCategories();
        List<BookModel> GetBooksByIds(List<int> ids);
        List<BookModel> SearchBooks(string searchTerm);
    }
}