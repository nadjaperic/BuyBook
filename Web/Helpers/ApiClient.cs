using System.Net.Http.Headers;
using Application.Models;

namespace Web.Helpers
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(HttpClient client)
        {
            _client = client;

            _client.BaseAddress = new Uri("https://localhost:7041/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public async Task<List<BookModel>> GetLastTenBooks()
        {
            string path = _client.BaseAddress + "Book/getLastTenBooks";
            HttpResponseMessage response = await _client.GetAsync(path);

            List<BookModel> books = new List<BookModel>();

            if (response.IsSuccessStatusCode)
            {
                books = await response.Content.ReadFromJsonAsync<List<BookModel>>();
            }

            return books;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            string path = _client.BaseAddress + "Book/getAllBooks";
            HttpResponseMessage response = await _client.GetAsync(path);

            List<BookModel> books = new List<BookModel>();

            if (response.IsSuccessStatusCode)
            {
                books = await response.Content.ReadFromJsonAsync<List<BookModel>>();
            }

            return books;
        }

        public async Task<List<AuthorModel>> GetAllAuthors()
        {
            string path = _client.BaseAddress + "Admin/getAllAuthors";

            HttpResponseMessage response = await _client.GetAsync(path);

            List<AuthorModel> authors = new List<AuthorModel>();

            if (response.IsSuccessStatusCode)
            {
                authors = await response.Content.ReadFromJsonAsync<List<AuthorModel>>();
            }

            return authors;
        }

        public async Task<List<PublisherModel>> GetAllPublishers()
        {
            string path = _client.BaseAddress + "Admin/getAllPublishers";
            HttpResponseMessage response = await _client.GetAsync(path);

            List<PublisherModel> publishers = new List<PublisherModel>();

            if (response.IsSuccessStatusCode)
            {
                publishers = await response.Content.ReadFromJsonAsync<List<PublisherModel>>();
            }

            return publishers;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            string path = _client.BaseAddress + "Admin/getAllCategories";
            HttpResponseMessage response = await _client.GetAsync(path);

            List<CategoryModel> categories = new List<CategoryModel>();

            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadFromJsonAsync<List<CategoryModel>>();
            }

            return categories;
        }

        public async Task<List<BookModel>> GetFeaturedBooks()
        {
            string path = _client.BaseAddress + "Book/getLastTenBooks";
            HttpResponseMessage response = await _client.GetAsync(path);

            List<BookModel> books = new List<BookModel>();

            if (response.IsSuccessStatusCode)
            {
                books = await response.Content.ReadFromJsonAsync<List<BookModel>>();
            }

            return books;
        }

        public async Task<BookModel> GetBook(string id)
        {
            string path = _client.BaseAddress + $"Book/getBook/{id}";

            HttpResponseMessage response = await _client.GetAsync(path);

            BookModel book = new BookModel();

            if (response.IsSuccessStatusCode)
            {
                book = await response.Content.ReadFromJsonAsync<BookModel>();
            }

            return book;
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            string path = _client.BaseAddress + $"Admin/createCategory";

            HttpResponseMessage response = await _client.PostAsJsonAsync(path, category);

            CategoryModel newCategory = new CategoryModel();

            if (response.IsSuccessStatusCode)
            {
                newCategory = await response.Content.ReadFromJsonAsync<CategoryModel>();
            }

            return newCategory;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            string path = _client.BaseAddress + $"Admin/deleteCategory?id={id}";

            HttpResponseMessage response = await _client.DeleteAsync(path);

            bool successfully = false;

            if (response.IsSuccessStatusCode)
            {
                successfully = await response.Content.ReadFromJsonAsync<bool>();
            }

            return successfully;
        }

        public async Task<PublisherModel> AddPublisher(PublisherModel model)
        {
            string path = _client.BaseAddress + $"Admin/addPublisher";

            HttpResponseMessage response = await _client.PostAsJsonAsync(path, model);

            PublisherModel newPublisher = new PublisherModel();

            if (response.IsSuccessStatusCode)
            {
                newPublisher = await response.Content.ReadFromJsonAsync<PublisherModel>();
            }

            return newPublisher;
        }

        public async Task<bool> DeletePublisher(int id)
        {
            string path = _client.BaseAddress + $"Admin/deletePublisher?id=" + id;

            HttpResponseMessage response = await _client.DeleteAsync(path);

            bool successfully = false;

            if (response.IsSuccessStatusCode)
            {
                successfully = await response.Content.ReadFromJsonAsync<bool>();
            }

            return successfully;
        }

        public async Task<AuthorModel> AddAuthor(AuthorModel model)
        {
            string path = _client.BaseAddress + $"Admin/addAuthor";

            HttpResponseMessage response = await _client.PostAsJsonAsync(path, model);

            AuthorModel authorModel = new AuthorModel();

            if (response.IsSuccessStatusCode)
            {
                authorModel = await response.Content.ReadFromJsonAsync<AuthorModel>();
            }

            return authorModel;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            string path = _client.BaseAddress + $"Admin/deleteAuthor?id=" + id;

            HttpResponseMessage response = await _client.DeleteAsync(path);

            bool successfully = false;

            if (response.IsSuccessStatusCode)
            {
                successfully = await response.Content.ReadFromJsonAsync<bool>();
            }

            return successfully;
        }

        public async Task<BookModel> AddBook(BookModel model)
        {
            string path = _client.BaseAddress + $"Admin/addBook";

            HttpResponseMessage response = await _client.PostAsJsonAsync(path, model);

            BookModel bookModel = new BookModel();

            if (response.IsSuccessStatusCode)
            {
                bookModel = await response.Content.ReadFromJsonAsync<BookModel>();
            }

            return bookModel;
        }
    }
}
