using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces
{
    public interface IPublisherService
    {
        PublisherModel AddPublisher(PublisherModel model);
        bool DeletePublisher(int id);
        List<PublisherModel> GetAllPublishers();
    }
}
