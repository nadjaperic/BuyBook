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
    }
}
