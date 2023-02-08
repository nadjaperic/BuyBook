using Domain.Common;

namespace Domain.Entities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string BookUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
