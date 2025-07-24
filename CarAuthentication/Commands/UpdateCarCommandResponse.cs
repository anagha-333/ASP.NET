using CarAuthentication.models;

namespace CarAuthentication.Commands
{
    public class UpdateCarCommandResponse
    {
        public Car? Car { get; set; }
        public bool IsUpdated { get; set; }
        public string? Message { get; set; }
    }
}
