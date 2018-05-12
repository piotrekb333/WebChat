using ChatApp.Domain;
using ChatApp.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ChatApp.Repositories
{
    public class UserRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<User> usersCollection;
        public UserRepository()
        {
            MongoContext contex = new MongoContext();
            database = contex.GetDatabase();
            usersCollection = database.GetCollection<User>("users");
        }

        public bool Insert(User model)
        {
            var md5 = new MD5CryptoServiceProvider();            
            usersCollection.InsertOne(model);
            return true;
        }

        public bool Delete(ObjectId id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var result = usersCollection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }

        public bool UpdateUser(ObjectId id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var update = Builders<User>.Update.Set(udateFieldName, updateFieldValue);

            var result = usersCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public  List<User> GetUsersByField(string fieldName, string fieldValue)
        {
            var filter = Builders<User>.Filter.Eq(fieldName, fieldValue);
            var result = usersCollection.Find(filter).ToList();

            return result;
        }
    }
}