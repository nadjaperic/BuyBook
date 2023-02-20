using Application.Models;

namespace Web.Models
{
    public class AdminPanelModel
    {
        public List<BookModel> Books { get; set; }
        public List<AuthorModel> AllAuthors { get; set; }
        public List<CategoryModel> AllCategories { get; set; }
        public List<PublisherModel> AllPublishers { get; set; }
    }
}
