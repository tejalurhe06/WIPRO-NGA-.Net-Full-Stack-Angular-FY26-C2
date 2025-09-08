using System;

namespace Design_Patterns.ObserverPattern
{
    public class ObserverDemo
    {
        public static void Run()
        {
            Console.WriteLine("Observer Pattern Demo:");
            WeatherStation station = new WeatherStation();

            var display1 = new WeatherDisplay("Display 1");
            var display2 = new WeatherDisplay("Display 2");

            station.RegisterObserver(display1);
            station.RegisterObserver(display2);

            station.SetTemperature(28.5f);
            station.RemoveObserver(display1);
            station.SetTemperature(30f);

            Console.WriteLine();
        }
    }
}