using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models.Entities
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("registerDate")]
        public DateTime RegisterDate { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}