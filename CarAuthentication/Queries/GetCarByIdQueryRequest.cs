using MediatR;

namespace CarAuthentication.Queries
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
