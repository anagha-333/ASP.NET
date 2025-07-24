using CarAuthentication.models;
using MediatR;

namespace CarAuthentication.Commands
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public int Number { get; set; }
        public Car UpdatedCar { get; set; } = default!;
    }
}
