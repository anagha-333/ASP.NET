using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Revv.Cars.Shared.Queries;
using Revv.Cars.Domain.models;
using Revv.Cars.DomainService.MongoDb;

namespace Revv.Cars.DomainService.QueriesHandler
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQueryRequest, GetAllCarQueryResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCarQueryHandler> _logger;

        public GetAllCarQueryHandler(IOptions<MongoDBSettings> settings, IMapper mapper, ILogger<GetAllCarQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;

            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _cars = database.GetCollection<Car>(settings.Value.CarCollectionName);
        }

        public async Task<GetAllCarQueryResponse> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var domainCars = await _cars.Find(_ => true).ToListAsync(cancellationToken);
                var sharedCars = _mapper.Map<List<Revv.Cars.Shared.Car>>(domainCars);

                return new GetAllCarQueryResponse
                {
                    cars = sharedCars
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all cars");
                throw;
            }
        }
    }
}
