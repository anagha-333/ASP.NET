using Revv.Cars.Shared;
using MediatR;

namespace Revv.Cars.Shared.Commands
{
    public class CreateCarCommandRequest : IRequest<CreateCarCommandResponse>
    {
        public string Image { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;

        public int Year { get; set; }     
        public string Place { get; set; } = default!;
        public int Number { get; set; }    
        public string Date { get; set; } = default!;
    }
}
