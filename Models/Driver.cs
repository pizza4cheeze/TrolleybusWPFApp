using System;

namespace TrolleybusWPFApp.Models
{
    public class Driver
    {
        public string Name { get; set; }

        public Driver(string name)
        {
            Name = name;
        }

        public void FixPoles(Trolleybus trolleybus)
        {
            Console.WriteLine($"{Name} is fixing the poles on trolleybus {trolleybus.TrolleybusNumber}.");
        }
    }
}

