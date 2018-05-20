using ChatApp.Domain;
using ChatApp.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Repositories
{
    public class MessageRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<Message> messagesCollection;
        public MessageRepository()
        {
            MongoContext contex = new MongoContext();
            database = contex.GetDatabase();
            messagesCollection = database.GetCollection<Message>("messages");
        }

        public bool Insert(Message model)
        {
            messagesCollection.InsertOne(model);
            return true;
        }

        public bool Delete(ObjectId id)
        {
            var filter = Builders<Message>.Filter.Eq("_id", id);
            var result = messagesCollection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }

        public bool Update(ObjectId id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<Message>.Filter.Eq("_id", id);
            var update = Builders<Message>.Update.Set(udateFieldName, updateFieldValue);

            var result = messagesCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public List<Message> GetMessagesByField(string fieldName, string fieldValue)
        {
            var filter = Builders<Message>.Filter.Eq(fieldName, fieldValue);
            var result = messagesCollection.Find(filter).ToList();

            return result;
        }
    }
}