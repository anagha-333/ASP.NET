using CarAuthentication.Commands;
using CarAuthentication.models;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace CarAuthentication.CommandsHandler
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly ILogger<UpdateCarCommandHandler> _logger;

        public UpdateCarCommandHandler(IOptions<MongoDBSettings> settings, ILogger<UpdateCarCommandHandler> logger)
        {
            _logger = logger;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var existing = await _cars.Find(c => c.Number == request.Number).FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                return new UpdateCarCommandResponse
                {
                    IsUpdated = false,
                    Message = "Car not found"
                };
            }

            var updated = request.UpdatedCar;
            updated.Id = existing.Id;
            updated.Number = existing.Number;
            updated.Image = existing.Image;
            updated.CreatedAt = existing.CreatedAt;
            updated.UpdatedAt = DateTime.UtcNow;

            await _cars.ReplaceOneAsync(c => c.Id == existing.Id, updated, cancellationToken: cancellationToken);

            return new UpdateCarCommandResponse
            {
                IsUpdated = true,
                Car = updated
            };
        }
    }
}
