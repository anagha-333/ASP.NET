using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbConnection.model;

namespace MongoDbConnection.service
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _students = database.GetCollection<Student>("students");
        }

        public List<Student> Get() => _students.Find(student => true).ToList();

        public Student Get(string id) =>
            _students.Find(student => student.Id == id).FirstOrDefault();

        public Student Create(Student student)
        {
            // Sanitize ID (in case something like "1" is passed)
            if (!string.IsNullOrEmpty(student.Id) && !ObjectId.TryParse(student.Id, out _))
            {
                student.Id = null;
            }

            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student studentIn) =>
            _students.ReplaceOne(student => student.Id == id, studentIn);

        public void Remove(string id) =>
            _students.DeleteOne(student => student.Id == id);
    }
}
