using CarAuthentication.Commands;
using CarAuthentication.models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAuthentication.CommandsHandler
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommandRequest, Unit>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly ILogger<DeleteCarCommandHandler> _logger;

        public DeleteCarCommandHandler(IOptions<MongoDBSettings> settings, ILogger<DeleteCarCommandHandler> logger)
        {
            _logger = logger;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<Unit> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            var existing = await _cars.Find(c => c.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException("Car not found");

            await _cars.DeleteOneAsync(c => c.Id == request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
