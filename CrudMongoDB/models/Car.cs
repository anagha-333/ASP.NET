using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudMongoDB.models
{
    [BsonIgnoreExtraElements] 
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("year")]
        public int Year { get; set; } 

        [BsonElement("place")]
        public string Place { get; set; }

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }
    }
}
