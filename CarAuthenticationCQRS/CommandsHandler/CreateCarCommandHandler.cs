using CarAuthentication.Commands;
using CarAuthentication.models;
using CarAuthentication.Services;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace CarAuthentication.CommandsHandler
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IMongoCollection<Car> _cars;
       

        public CreateCarCommandHandler(IOptions<MongoDBSettings> settings )
        {
           
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                Id = null,
                Image = request.Image,
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                Place = request.Place,
                Number = request.Number,
                Date = request.Date,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            try
            {
                await _cars.InsertOneAsync(car, cancellationToken: cancellationToken);
                return new CreateCarCommandResponse { Car = car };
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
    }
}
