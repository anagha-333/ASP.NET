using CarAuthentication.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAuthentication.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _cars;

        private readonly ILogger<CarService> _logger;



        public CarService(IOptions<MongoDBSettings> settings, ILogger<CarService> logger)
        {

            _logger = logger;

            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        // Get all cars
        //public async Task<List<Car>> GetAsync() =>
        //    await _cars.Find(c => true).ToListAsync();

        public async Task<List<Car>> GetAsync()
        {
            _logger.LogInformation("Fetching all cars");
            return await _cars.Find(c => true).ToListAsync();
        }



        // Get car by ID
        public async Task<Car> GetAsync(string id) =>
            await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();

        // ✅ Updated to use int instead of string
        public async Task<Car> GetByNumberAsync(int number) =>
            await _cars.Find(c => c.Number == number).FirstOrDefaultAsync();

        // Create new car
        public async Task<Car> CreateAsync(Car car)
        {
            try
            {
                car.CreatedAt = DateTime.UtcNow;
                car.UpdatedAt = DateTime.UtcNow;
                await _cars.InsertOneAsync(car);
                return car;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ InsertOneAsync failed: " + ex.Message);
                throw; // rethrow to hit controller catch
            }
        }

        // ✅ Updated to use int and updatedCar
        public async Task UpdateByNumberAsync(int number, Car updatedCar)
        {
            var existing = await GetByNumberAsync(number);
            if (existing == null) return;

            updatedCar.Id = existing.Id; // retain MongoDB ID
            await _cars.ReplaceOneAsync(c => c.Id == existing.Id, updatedCar);
        }

        // Delete car by ID
        public async Task DeleteAsync(string id) =>
            await _cars.DeleteOneAsync(c => c.Id == id);
    }
}
