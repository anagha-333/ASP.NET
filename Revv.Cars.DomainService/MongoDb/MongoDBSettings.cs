namespace Revv.Cars.DomainService.MongoDb
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CarCollectionName { get; set; }
    }
}
