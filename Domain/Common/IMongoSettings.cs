namespace Domain.Common
{
    public interface IMongoSettings
    {
        string Connection { get; set; }
        string DatabaseName { get; set; }
    }
}