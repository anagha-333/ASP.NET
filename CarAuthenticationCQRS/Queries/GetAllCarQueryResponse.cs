using CarAuthentication.models;

namespace CarAuthentication.Queries
{
    public class GetAllCarQueryResponse
    {
        public List<Car> cars { get; set; } = new();
    }
}
