using Revv.Cars.Shared;
using MediatR;

namespace Revv.Cars.Shared.Commands
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public int Number { get; set; }
        public Car UpdatedCar { get; set; } = default!;
    }
}
