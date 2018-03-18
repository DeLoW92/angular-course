using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatingApp.API.Models
{
    public class User
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}