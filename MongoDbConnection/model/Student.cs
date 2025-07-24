using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbConnection.model
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        public int Age { get; set; }

        public Student() { }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Student(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
