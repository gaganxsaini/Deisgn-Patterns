using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Bridge
{
    namespace RemoteControlExample
    {
        interface ITelevision
        {
            void Off();
            void On();
            void TuneChannel(int c);
        }

        public class SonyTv : ITelevision
        {
            public void Off() => Console.WriteLine("Turning OFF Sony TV");
            public void On() => Console.WriteLine("Turning ON Sony TV");
            public void TuneChannel(int c) => Console.WriteLine($"Tuning Channel to {c} on Sony TV");
        }

        public class SamsungTv : ITelevision
        {
            public void Off() => Console.WriteLine("Turning OFF Samsung TV");
            public void On() => Console.WriteLine("Turning ON Samsung TV");
            public void TuneChannel(int c) => Console.WriteLine($"Tuning Channel to {c}on Samsung TV");
        }

        class RemoteControl
        {
            private ITelevision implementor;

            public int CurrentChannel { get; set; }

            public RemoteControl(string tvType)
            {
                if (tvType == "SONY")
                    implementor = new SonyTv();
                else
                    implementor = new SamsungTv();
            }

            public void TurnOn() => implementor?.On();
            public void TurnOff() => implementor?.Off();
            public void SetChannel(int c)
            {
                if (c < 0)
                    c = 0;
                implementor?.TuneChannel(c);
                CurrentChannel = c;
            }
        }

        class AdvancedRemoteControl : RemoteControl
        {
            public AdvancedRemoteControl(string tvType) : base(tvType) { }
            public void NextChannel() => SetChannel(CurrentChannel + 1);
            public void PrevChannel() => SetChannel(CurrentChannel - 1);
        }

        static class ExampleCode
        {
            public static void Run()
            {
                AdvancedRemoteControl rc = new AdvancedRemoteControl("SONY");
                rc.TurnOn();
                rc.SetChannel(50);
                rc.NextChannel();
                rc.TurnOff();
            }
        }

    }
}
