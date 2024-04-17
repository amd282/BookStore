using BookStore.Data; // Assuming BookStoreContext is in this namespace
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Linq;

namespace BookStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Books");

            var bookCollection = database.GetCollection<Book>("Books");

            
        }
    }
}
