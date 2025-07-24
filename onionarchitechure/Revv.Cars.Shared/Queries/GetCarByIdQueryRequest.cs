using MediatR;

namespace Revv.Cars.Shared.Queries
{
    public class GetCarByIdQueryRequest : IRequest<GetCarByIdQueryResponse>
    {
        public string Id { get; }

        public GetCarByIdQueryRequest(string id)
        {
            Id = id;
        }
    }
}
