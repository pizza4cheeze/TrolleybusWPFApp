using System;
using System.Threading.Tasks;

namespace TrolleybusWPFApp.Models
{
    public class Trolleybus
    {
        private static readonly Random Random = new Random();

        public event Action<Trolleybus> BrokenDown;
        public event Action<Trolleybus> PolesOff;

        public Driver Driver { get; set; }
        public string TrolleybusNumber { get; set; }
        public bool IsRunning { get; private set; }

        public Trolleybus(string trolleybusNumber, Driver driver)
        {
            TrolleybusNumber = trolleybusNumber;
            Driver = driver;
        }

        public void Start()
        {
            IsRunning = true;
            Task.Run(() => Run());
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private async Task Run()
        {
            while (IsRunning)
            {
                await Task.Delay(1000);

                if (Random.NextDouble() < 0.1)
                {
                    BrokenDown?.Invoke(this);
                }

                if (Random.NextDouble() < 0.2)
                {
                    PolesOff?.Invoke(this);
                }
            }
        }
    }
}
