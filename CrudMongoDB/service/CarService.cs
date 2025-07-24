using CrudMongoDB.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudMongoDB.service
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _cars;

        public CarService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<List<Car>> GetAsync() =>
            await _cars.Find(c => true).ToListAsync();

        public async Task<Car> GetAsync(string id) =>
            await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Car car) =>
            await _cars.InsertOneAsync(car);

        public async Task UpdateAsync(string id, Car carIn) =>
            await _cars.ReplaceOneAsync(c => c.Id == id, carIn);

        public async Task DeleteAsync(string id) =>
            await _cars.DeleteOneAsync(c => c.Id == id);
    }
}
