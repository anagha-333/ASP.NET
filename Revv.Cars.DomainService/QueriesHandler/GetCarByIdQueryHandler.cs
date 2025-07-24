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
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQueryRequest, GetCarByIdQueryResponse>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCarByIdQueryHandler> _logger;

        public GetCarByIdQueryHandler(IOptions<MongoDBSettings> settings, IMapper mapper, ILogger<GetCarByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;

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

                if (car == null)
                {
                    return new GetCarByIdQueryResponse
                    {
                        Car = null
                         
                    };
                }

                var sharedCar = _mapper.Map<Revv.Cars.Shared.Car>(car);

                return new GetCarByIdQueryResponse
                {
                    Car = sharedCar

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving car with ID: {request.Id}");
                throw;
            }
        }
    }
}
