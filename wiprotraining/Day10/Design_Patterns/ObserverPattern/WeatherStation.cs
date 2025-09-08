using System.Collections.Generic;

namespace Design_Patterns.ObserverPattern
{
    public class WeatherStation : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private float temperature;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(temperature);
            }
        }

        public void SetTemperature(float temp)
        {
            temperature = temp;
            NotifyObservers();
        }
    }
}