using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBuyBookDbContext _dbContext;

        public BookService(IBuyBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookModel AddBook(BookModel book)
        {
            if (book is null) return null;

            var authors = book.AuthorsIds != null ? _dbContext.Authors.Where(x => book.AuthorsIds.Contains(x.Id)).ToList() : Enumerable.Empty<Author>();
            var publishers = book.PublishersIds != null ? _dbContext.Publishers.Where(x => book.PublishersIds.Contains(x.Id)).ToList() : Enumerable.Empty<Publisher>();
            var categories = book.CategoriesIds != null ? _dbContext.Categories.Where(x => book.CategoriesIds.Contains(x.Id)).ToList() : Enumerable.Empty<Category>();

            var bookToAdd = new Book
            {
                Authors = authors.ToList(),
                Publishers = publishers.ToList(),
                Categories = categories.ToList(),
                Description = book.Description,
                Price = book.Price,
                IsFeatured = book.IsFeatured,
                Title = book.Title,
                BookUrl = book.BookUrl,
                ShortDescription = book.ShortDescription
            };

            _dbContext.Books.Add(bookToAdd);
            _dbContext.SaveChanges();

            book.Id = bookToAdd.Id;

            return book;
        }

        public bool DeleteBook(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

            if (book is null) return false;

            _dbContext.Books.Remove(book);
            return _dbContext.SaveChanges() == 1;
        }

        public List<BookModel> GetAllBooks()
        {
            var books = _dbContext.Books.Include(x => x.Publishers)
                                        .Include(x => x.Categories)
                                        .Include(x => x.Authors)
                                        .ToList();

            var booksToReturn = books.Select(x =>
                new BookModel
                {
                    Id = x.Id,
                    BookUrl = x.BookUrl,
                    Price = x.Price,
                    Description = x.Description,
                    IsFeatured = x.IsFeatured,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                    Authors = x.Authors.Select(a => new AuthorModel
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList(),
                    Publishers = x.Publishers.Select(p =>
                    new PublisherModel
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList(),
                    Categories = x.Categories.Select(c =>
                    new CategoryModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()

                }).ToList();

            return booksToReturn;
        }

        public BookModel GetBookById(int id)
        {
            var book = _dbContext.Books.Include(x => x.Publishers)
                                       .Include(x => x.Categories)
                                       .Include(x => x.Authors)
                                       .FirstOrDefault(x => x.Id == id);

            if (book is null) return null;

            var bookToReturn = new BookModel
            {
                Id = book.Id,
                BookUrl = book.BookUrl,
                Price = book.Price,
                Description = book.Description,
                IsFeatured = book.IsFeatured,
                ShortDescription = book.ShortDescription,
                Title = book.Title,
                Authors = book.Authors.Select(a => new AuthorModel
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList(),
                Publishers = book.Publishers.Select(p =>
                new PublisherModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),
                Categories = book.Categories.Select(c =>
                new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()

            };

            return bookToReturn;
        }

        public UpdateBookModel UpdateBook(UpdateBookModel model)
        {
            if (model == null) return null;

            var book = _dbContext.Books.FirstOrDefault(x => x.Id == model.Id);

            if (book == null) return null;

            book.ShortDescription = model.ShortDescription;
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.BookUrl = model.BookUrl;
            book.IsFeatured = model.IsFeatured;

            var authors = model.AuthorsIds != null ? _dbContext.Authors.Where(x => model.AuthorsIds.Contains(x.Id)).ToList() : Enumerable.Empty<Author>();
            var publishers = model.PublishersIds != null ? _dbContext.Publishers.Where(x => model.PublishersIds.Contains(x.Id)).ToList() : Enumerable.Empty<Publisher>();
            var categories = model.CategoriesIds != null ? _dbContext.Categories.Where(x => model.CategoriesIds.Contains(x.Id)).ToList() : Enumerable.Empty<Category>();

            book.Authors = authors.ToList();
            book.Publishers = publishers.ToList();
            book.Categories = categories.ToList();

            return _dbContext.SaveChanges() == 1 ? model : null;
        }
    }
}