using Application.Models;

namespace Web.Models
{
    public class HomeModel
    {
        public List<BookModel> FeaturedBooks { get; set; }
        public List<BookModel> LastBooks { get; set; }
    }
}