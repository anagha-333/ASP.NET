using CarAuthentication.models;
using MediatR;
using MongoDB.Driver;

namespace CarAuthentication.Queries
{
    public class GetAllCarQueryRequest: IRequest<GetAllCarQueryResponse>
    {
        
    }
}
