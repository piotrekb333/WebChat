using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models.Entities
{
    public class Message
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("userNameSender")]
        public string UserNameSender { get; set; }
        [BsonElement("userNameReceiver")]
        public string UserNameReceiver { get; set; }
        [BsonElement("dateSent")]
        public DateTime DateSent { get; set; }
    }
}