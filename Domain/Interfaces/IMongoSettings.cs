namespace Domain.Interfaces
{
    public interface IMongoSettings
    {
        string Connection { get; set; }
        string DatabaseName { get; set; }
    }
}