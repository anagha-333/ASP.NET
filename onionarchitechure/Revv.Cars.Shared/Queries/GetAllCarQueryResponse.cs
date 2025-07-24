using Revv.Cars.Shared;

namespace Revv.Cars.Shared.Queries
{
    public class GetAllCarQueryResponse
    {
        public List<Car> cars { get; set; } = new();
    }
}
