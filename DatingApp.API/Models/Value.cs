using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatingApp.API.Models
{
    public class Value
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public object Body { get; internal set; }
        public object UpdatedOn { get; internal set; }

        public object CreatedOn { get; internal set;}
    }
}