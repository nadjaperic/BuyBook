using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Currency
{
    public class CurrencyService
    {
        private readonly HttpClient _client;

        public CurrencyService(HttpClient client)
        {
            _client = client;

            _client.BaseAddress = new Uri("https://api.freecurrencyapi.com/v1/latest?apikey=5nKz7ohauRvwDB7IHrVKDfwHK1lxXOBul4aw4xS5");
            _client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public ExchangeRateData ListAllCurrencies()
        {
            string path = _client.BaseAddress.ToString();

            HttpResponseMessage response = _client.GetAsync(path).Result;

            ExchangeRateData exchangeRate = new ExchangeRateData();

            if (response.IsSuccessStatusCode)
            {
                exchangeRate = response.Content.ReadFromJsonAsync<ExchangeRateData>().Result;
            }

            return exchangeRate;
        }
    }

    public class ExchangeRateData
    {
        public Dictionary<string, double> Data { get; set; }
    }
}
