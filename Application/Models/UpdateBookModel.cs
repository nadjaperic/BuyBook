using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UpdateBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string BookUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<int> AuthorsIds { get; set; }
        public List<int> PublishersIds { get; set; }
        public List<int> CategoriesIds { get; set; }
    }
}
