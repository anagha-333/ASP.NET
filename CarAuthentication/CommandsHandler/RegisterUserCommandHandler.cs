using CarAuthentication.Commands;
using CarAuthentication.models;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAuthentication.CommandsHandler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IMongoCollection<User> _users;

        public RegisterUserCommandHandler(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>("users");
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existing = await _users.Find(u => u.Username == request.Username)
                                       .FirstOrDefaultAsync(cancellationToken);

            if (existing != null)
            {
                return new RegisterUserCommandResponse
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            var user = new User
            {
                Id = null,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password), // ✅ hash here
                Role = request.Role
            };

            await _users.InsertOneAsync(user, cancellationToken: cancellationToken);

            return new RegisterUserCommandResponse
            {
                Success = true,
                Message = "User registered"
            };
        }
    }
}
