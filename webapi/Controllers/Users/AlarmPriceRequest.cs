using System;

namespace webapi.Controllers.Users
{
    public class AlarmPriceRequest
    {
        public Guid ProductId { get; set; }
        public double PriceToMonitor { get; set; }
    }
}