using MediatR;

namespace Revv.Cars.Shared.Commands
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
