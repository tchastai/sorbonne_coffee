﻿using System.Collections.Generic;

using MongoDB.Driver;

using SorbonneCoffee.Models;

namespace SorbonneCoffee.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;
        public OrderService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrderCollectionName);
        }

        public List<Order> Get() => _orders.Find(order => true).ToList();

        public Order Get(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order orderIn)
        {
            _orders.ReplaceOne(order => order.Id == id, orderIn);
        }

        public void Remove(Order orderIn)
        {
            _orders.DeleteOne(order => order.Id == orderIn.Id);
        }

        public void Remove(string id)
        {
            _orders.DeleteOne(order => order.Id == id);
        }
    }
}
