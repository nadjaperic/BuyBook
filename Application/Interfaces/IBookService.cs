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
        UpdateBookModel UpdateBook(UpdateBookModel model);
        CategoryModel CreateCategory(CategoryModel category);
        bool DeleteCategory(int id);
        AuthorModel AddAuthor(AuthorModel model);
        bool DeleteAuthor(int id);
    }
}