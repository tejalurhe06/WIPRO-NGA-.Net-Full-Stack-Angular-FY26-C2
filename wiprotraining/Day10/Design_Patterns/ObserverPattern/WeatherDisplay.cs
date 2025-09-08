using System;

namespace Design_Patterns.ObserverPattern
{
    public class WeatherDisplay : IObserver
    {
        private string name;

        public WeatherDisplay(string name)
        {
            this.name = name;
        }

        public void Update(float temperature)
        {
            Console.WriteLine($"{name} received temperature update: {temperature}°C");
        }
    }
}