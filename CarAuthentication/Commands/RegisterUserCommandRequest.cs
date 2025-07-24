using MediatR;

namespace CarAuthentication.Commands
{
    public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}
