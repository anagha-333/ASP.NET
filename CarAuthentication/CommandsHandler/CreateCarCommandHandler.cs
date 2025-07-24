using CarAuthentication.Commands;
using CarAuthentication.models;
using CarAuthentication.Notifications;
using CarAuthentication.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuthentication.CommandsHandler
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateCarCommandHandler> _logger;

        public CreateCarCommandHandler(
            IOptions<MongoDBSettings> settings,
            IMediator mediator,
            ILogger<CreateCarCommandHandler> logger)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var car = new Car
            {
                Id = null, // MongoDB will assign the ObjectId automatically
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

                // Publish CarCreatedEvent
                await _mediator.Publish(new CarCreatedEvent
                {
                    CarId = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    Place = car.Place
                }, cancellationToken);

                _logger.LogInformation("Car created successfully: {Brand} {Model}", car.Brand, car.Model);

                return new CreateCarCommandResponse { Car = car };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a car");
                throw;
            }
        }
    }
}
