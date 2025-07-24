using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Revv.Cars.Domain;
using Revv.Cars.Domain.models;
using Revv.Cars.DomainService.MongoDb;
using Revv.Cars.Shared.Commands;

namespace Revv.Cars.DomainService.CommandsHandler
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(IOptions<MongoDBSettings> settings, IMapper mapper)
        {
           
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
            _mapper = mapper;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var sharedCar = _mapper.Map<Revv.Cars.Shared.Car>(request);
                sharedCar.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                sharedCar.Image = request.Image; // Assuming `Image` is a string filename
                sharedCar.CreatedAt = DateTime.UtcNow;
                sharedCar.UpdatedAt = DateTime.UtcNow;

                var domainCar = _mapper.Map<Car>(sharedCar);
                await _cars.InsertOneAsync(domainCar, cancellationToken);

                var response = _mapper.Map<CreateCarCommandResponse>(domainCar);
                return response;
            }
            catch (Exception ex)
            {
                // Consider logging the error here
                throw;
            }
        }
    }
}
