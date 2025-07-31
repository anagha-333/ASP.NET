using CarAuthentication.models;
using MediatR;

namespace CarAuthentication.Queries
{
    public class GetCarByNumberQueryRequest : IRequest<Car?>
    {
        public int Number { get; set; }
    }
}
