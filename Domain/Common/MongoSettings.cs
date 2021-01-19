namespace Domain.Common
{
    public class MongoSettings : IMongoSettings
    {
        public string Connection { get; set; } 
        public string DatabaseName { get; set; } 
        
    }

    
}