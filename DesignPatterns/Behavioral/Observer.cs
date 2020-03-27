using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Observer
{
    namespace WeatherStationExample
    {
        interface ISubject
        {
            void Attach(IObserver ob);
            void Detach(IObserver ob);
            void NotifyAll();
        }

        interface IObserver
        {
            //Push Model
            void Update(int temperature, int humidity);
        }

        class WeatherData : ISubject
        {
            private int temp = 0, humidity = 0;
            private List<IObserver> _observers = new List<IObserver>();
            public void Attach(IObserver ob) => _observers.Add(ob);
            public void Detach(IObserver ob) => _observers.Remove(ob);
            public void NotifyAll()
            {
                foreach (var ob in _observers)
                    ob.Update(temp, humidity);
            }

            public void setState(int t, int h)
            {
                (temp, humidity) = (t, h);
                NotifyAll();
            }
        }

        class TVDisplay : IObserver
        {
            private ISubject subject = null;
            public TVDisplay(ISubject s)
            {
                subject = s;
                s.Attach(this);
            }

            public void Update(int temperature, int humidity)
            {
                Console.WriteLine($"Weather Updated : {temperature}, {humidity}");
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                WeatherData sub = new WeatherData();
                TVDisplay display = new TVDisplay(sub);

                sub.setState(10, 20);
                sub.setState(20, 30);
            }
        }
    }
}
