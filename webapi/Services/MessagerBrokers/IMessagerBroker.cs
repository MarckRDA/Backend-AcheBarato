using Domain.Models.SenderEntities;

namespace webapi.Services.MessagerBrokers
{
    public interface IMessagerBroker
    {
        void SendEntityToNotify(SenderEntity sendObject);
    }
}