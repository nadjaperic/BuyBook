namespace Application.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
		public string BookUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<int> AuthorsIds { get; set; }
        public List<int> PublishersIds { get; set; }
        public List<int> CategoriesIds { get; set; }
        public List<AuthorModel> Authors { get; set; }
        public List<PublisherModel> Publishers { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}