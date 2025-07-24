using MediatR;

namespace CarAuthentication.Commands
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
