namespace Application.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public List<BookModel> Books { get; set; }
    }
}