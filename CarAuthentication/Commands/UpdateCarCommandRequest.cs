using CarAuthentication.models;
using MediatR;

namespace CarAuthentication.Commands
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public string Id { get; set; } = default!;
        public Car UpdatedCar { get; set; } = default!;
    }
}
