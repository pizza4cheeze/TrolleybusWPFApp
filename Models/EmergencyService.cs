using System;

namespace TrolleybusWPFApp.Models
{
    public class EmergencyService : IEmergencyService
    {
        public void HandleBreakdown(Trolleybus trolleybus)
        {
            Console.WriteLine($"Emergency service is handling the breakdown of trolleybus {trolleybus.TrolleybusNumber}.");
        }
    }
}

