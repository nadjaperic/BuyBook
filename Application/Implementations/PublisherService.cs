using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IBuyBookDbContext _dbContext;

        public PublisherService(IBuyBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PublisherModel AddPublisher(PublisherModel model)
        {
            if (model == null) return null;

            var publisherToAdd = new Publisher
            {
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                DateEstablished = model.DateEstablished,
                Name = model.Name
            };

            _dbContext.Publishers.Add(publisherToAdd);
            _dbContext.SaveChanges();

            model.Id = publisherToAdd.Id;

            return model;
        }

        public bool DeletePublisher(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(x => x.Id == id);

            if (publisher is null) return false;

            _dbContext.Publishers.Remove(publisher);
            return _dbContext.SaveChanges() == 1;
        }

        public List<PublisherModel> GetAllPublishers()
        {
            var allPublishers = _dbContext.Publishers.ToList();

            return allPublishers.Select(x => new PublisherModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Country = x.Country,
                DateEstablished = x.DateEstablished
            }).ToList();
        }
    }
}
