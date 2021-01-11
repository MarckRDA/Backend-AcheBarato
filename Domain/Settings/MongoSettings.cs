using Domain.Interfaces;

namespace Domain.Settings
{
    public class MongoSettings : IMongoSettings
    {
        public string Connection { get; set; } 
        public string DatabaseName { get; set; } 
        
    }

    
}