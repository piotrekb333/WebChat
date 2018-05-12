using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Domain
{
    public class MongoContext
    {
        public IMongoDatabase GetDatabase()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            return client.GetDatabase("Chat");
        }
    }
}