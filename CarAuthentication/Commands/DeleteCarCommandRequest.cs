using MediatR;

namespace CarAuthentication.Commands
{
    public class DeleteCarCommandRequest : IRequest <Unit> 
    {
        public string Id { get; }

        public DeleteCarCommandRequest(string id)
        {
            Id = id;
        }
    }
}
