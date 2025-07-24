using Revv.Cars.Shared;

namespace Revv.Cars.Shared.Commands
{
    public class UpdateCarCommandResponse
    {
        public Car? Car { get; set; }
        public bool IsUpdated { get; set; }
        public string? Message { get; set; }
    }
}
