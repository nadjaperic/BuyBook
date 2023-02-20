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

        public AuthorModel AddAuthor(AuthorModel model)
        {
            if (model is null) return null;

            var newAuthor = new Author
            {
                Name = model.Name,
                Biography = model.Biography,
                BirthDate = model.BirthDate
            };

            _dbContext.Authors.Add(newAuthor);
            _dbContext.SaveChanges();

            model.Id = newAuthor.Id;

            return model;
        }

        public BookModel AddBook(BookModel book)
        {
            if (book is null) return null;

            var authors = book.AuthorsIds != null ? _dbContext.Authors.Where(x => book.AuthorsIds.Contains(x.Id)).ToList() : new List<Author>();
            var publishers = book.PublishersIds != null ? _dbContext.Publishers.Where(x => book.PublishersIds.Contains(x.Id)).ToList() : new List<Publisher>();
            var categories = book.CategoriesIds != null ? _dbContext.Categories.Where(x => book.CategoriesIds.Contains(x.Id)).ToList() : new List<Category>();

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

        public CategoryModel CreateCategory(CategoryModel category)
        {
            if (category is null) return null;

            var newCategory = new Category
            {
                Name = category.Name
            };

            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            category.Id = newCategory.Id;

            return category;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(x => x.Id == id);

            if (author is null) return false;

            _dbContext.Authors.Remove(author);
            return _dbContext.SaveChanges() == 1;
        }

        public bool DeleteBook(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

            if (book is null) return false;

            _dbContext.Books.Remove(book);
            return _dbContext.SaveChanges() == 1;
        }

        public bool DeleteCategory(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null) return false;

            _dbContext.Categories.Remove(category);
            return _dbContext.SaveChanges() == 1;
        }

        public List<AuthorModel> GetAllAuthors()
        {
            var allAuthors = _dbContext.Authors.ToList();

            return allAuthors.Select(x => new AuthorModel
            {
                Id = x.Id,
                Name = x.Name,
                Biography = x.Biography,
                BirthDate = x.BirthDate
            }).ToList();
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

        public List<CategoryModel> GetAllCategories()
        {
            var allCategories = _dbContext.Categories.ToList();

            return allCategories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
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

        public List<BookModel> GetFeaturedBooks()
        {
            var books = _dbContext.Books.Include(x => x.Publishers)
                                         .Include(x => x.Categories)
                                         .Include(x => x.Authors)
                                         .Where(x => x.IsFeatured)
                                         .Take(10)
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

        public List<BookModel> GetLastTenBooks()
        {
            var books = _dbContext.Books.Include(x => x.Publishers)
                                         .Include(x => x.Categories)
                                         .Include(x => x.Authors)
                                         .Take(10)
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

            var authors = model.AuthorsIds != null ? _dbContext.Authors.Where(x => model.AuthorsIds.Contains(x.Id)).ToList() : new List<Author>();
            var publishers = model.PublishersIds != null ? _dbContext.Publishers.Where(x => model.PublishersIds.Contains(x.Id)).ToList() : new List<Publisher>();
            var categories = model.CategoriesIds != null ? _dbContext.Categories.Where(x => model.CategoriesIds.Contains(x.Id)).ToList() : new List<Category>();

            book.Authors = authors.ToList();
            book.Publishers = publishers.ToList();
            book.Categories = categories.ToList();

            return _dbContext.SaveChanges() == 1 ? model : null;
        }
    }
}