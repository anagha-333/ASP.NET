using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv.Cars.Domain.models;
using Revv.Cars.DomainService.MongoDb;
using Revv.Cars.Shared.Commands;

namespace Revv.Cars.DomainService.CommandsHandler
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly ILogger<UpdateCarCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler(IOptions<MongoDBSettings> settings, ILogger<UpdateCarCommandHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

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

            // Map updated shared car to domain car
            var updatedCar = _mapper.Map<Car>(request.UpdatedCar);

            // Preserve non-updated fields from the existing document
            updatedCar.Id = existing.Id;
            updatedCar.Number = existing.Number;
            updatedCar.Image = existing.Image;
            updatedCar.CreatedAt = existing.CreatedAt;
            updatedCar.UpdatedAt = DateTime.UtcNow;

            await _cars.ReplaceOneAsync(c => c.Id == existing.Id, updatedCar, cancellationToken: cancellationToken);

            return new UpdateCarCommandResponse
            {
                IsUpdated = true,
                Car = _mapper.Map<Revv.Cars.Shared.Car>(updatedCar)
            };
        }
    }
}
