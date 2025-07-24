namespace CarAuthentication.Commands
{
    public class LoginUserCommandResponse
    {
        public string Token { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
