using CarAuthentication.Commands;
using CarAuthentication.models;
using CarAuthentication.Services;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAuthentication.CommandsHandler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IMongoCollection<User> _users;
        private readonly JwtTokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(
            IOptions<MongoDBSettings> settings,
            JwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>("users");
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _users.Find(u => u.Username == request.Username).FirstOrDefaultAsync(cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _tokenGenerator.GenerateToken(user.Username, user.Role);

            return new LoginUserCommandResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
