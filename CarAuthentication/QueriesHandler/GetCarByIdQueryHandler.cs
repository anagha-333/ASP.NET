using CarAuthentication.models;
using CarAuthentication.Queries;
using CarAuthentication.Services;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace CarAuthentication.QueriesHandler
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQueryRequest, GetCarByIdQueryResponse>
    {
        private readonly IMongoCollection<Car> _cars;
       

        public GetCarByIdQueryHandler(IOptions<MongoDBSettings> settings)
        {
            

            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<GetCarByIdQueryResponse> Handle(GetCarByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var car = await _cars.Find(Builders<Car>.Filter.Eq(c => c.Id, request.Id))
                                     .FirstOrDefaultAsync(cancellationToken);

                return new GetCarByIdQueryResponse { Car = car };
            }
            catch (Exception ex)
            {
              
                throw;
            }
        }
    }
}
