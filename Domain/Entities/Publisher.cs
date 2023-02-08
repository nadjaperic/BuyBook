using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Publisher : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateEstablished { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Book> Books { get; set; }
    }
}