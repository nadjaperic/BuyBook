using Application.Models;
using Infrastructure.Currency;

namespace Web.Models
{
    public class BooksPageModel
    {
        public BookModel Book { get; set; }
        public ExchangeRateData ExchangeRateData { get; set; }
    }
}
