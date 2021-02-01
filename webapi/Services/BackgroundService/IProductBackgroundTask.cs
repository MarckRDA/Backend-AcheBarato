using System.Threading.Tasks;

namespace webapi.Services.BackgroundService
{
    public interface IProductBackgroundTask
    {
        void PushProductsInDB();
        void PushTrendProductsInDB();
        void CleanTrendsProducts();
        Task MonitorPriceProducts();
        void NotifyUserAboutAlarmPrice();
        
    }
}