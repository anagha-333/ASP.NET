using CarAuthentication.models;
using CarAuthentication.Queries;
using CarAuthentication.Services;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace CarAuthentication.QueriesHandler
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQueryRequest, GetAllCarQueryResponse>
    {
        private readonly IMongoCollection<Car> _cars;
       

        public GetAllCarQueryHandler(IOptions<MongoDBSettings> settings)
        {
            

            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<GetAllCarQueryResponse> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var carsList = await _cars.Find(_ => true).ToListAsync(cancellationToken);

                return new GetAllCarQueryResponse
                {
                    cars = carsList
                };
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
