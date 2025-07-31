using CarAuthentication.models;
using CarAuthentication.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAuthentication.QueriesHandler
{


    public class GetCarByNumberQueryHandler : IRequestHandler<GetCarByNumberQueryRequest, Car?>
    {
        private readonly IMongoCollection<Car> _cars;

        public GetCarByNumberQueryHandler(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<Car?> Handle(GetCarByNumberQueryRequest request, CancellationToken cancellationToken)
        {
            return await _cars.Find(c => c.Number == request.Number).FirstOrDefaultAsync(cancellationToken);
        }
    }


}
