using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class Light
    {
        public bool IsOn { get; private set; } = false;
        public void Toggle()
        {
            IsOn = !IsOn;
            Console.WriteLine($"Light is on? : {IsOn}");
        }
    }

    public class LightToggleCommand : ICommand
    {
        private Light light = null;
        public LightToggleCommand(Light l) => light = l;
        public void Execute() => light.Toggle();
        public void Undo() => light.Toggle();
    }

    public enum CeilingFanSpeed { Off, Low, Medium, High }

    public class CeilingFan
    {
        public CeilingFanSpeed CurrentSpeed { get; private set; } = CeilingFanSpeed.Off;

        public void SpeedUp()
        {
            if (CurrentSpeed != CeilingFanSpeed.High)
                CurrentSpeed++;

            Console.WriteLine($"Fan is running on {CurrentSpeed} speed.");
        }

        public void SpeedDown()
        {
            if (CurrentSpeed != CeilingFanSpeed.Off)
                CurrentSpeed--;

            Console.WriteLine($"Fan is running on {CurrentSpeed} speed.");
        }

        public void ChangeSpeed(CeilingFanSpeed newSpeed) => CurrentSpeed = newSpeed;
        public void TurnOff() => CurrentSpeed = CeilingFanSpeed.Off;
    }

    public class CeilingFanSpeedUpCommand : ICommand
    {
        CeilingFanSpeed prevSpeed;
        CeilingFan fan;

        public CeilingFanSpeedUpCommand(CeilingFan f) => fan = f;

        public void Execute()
        {
            prevSpeed = fan.CurrentSpeed;
            fan.SpeedUp();
        }

        public void Undo() => fan.ChangeSpeed(prevSpeed);
    }

    public class CeilingFanSpeedDownCommand : ICommand
    {
        CeilingFanSpeed prevSpeed;
        CeilingFan fan;

        public CeilingFanSpeedDownCommand(CeilingFan f) => fan = f;

        public void Execute()
        {
            prevSpeed = fan.CurrentSpeed;
            fan.SpeedDown();
        }

        public void Undo()
        {
            fan.ChangeSpeed(prevSpeed);
        }

    }

    public class MasterCommand : ICommand //Macro Command
    {
        List<ICommand> commands = new List<ICommand>();

        public void AddCommand(ICommand cmd) => commands.Add(cmd);

        public void Execute()
        {
            foreach (var cmd in commands)
                cmd.Execute();
        }

        public void Undo()
        {
            foreach (var cmd in commands)
                cmd.Undo();
        }
    }

    public class NoOpCommand : ICommand
    {
        public void Execute() {}
        public void Undo() {}
    }

    public class RemoteContorl
    {
        ICommand[] buttons = new ICommand[5];
        public void AddCommand(int btnNum, ICommand cmd) => buttons[btnNum] = cmd;
        public void OnButtonPress(int btnNum) => buttons[btnNum].Execute();
    }

    static class ExampleCode
    {
        public static void Run()
        {
            RemoteContorl remote = new RemoteContorl();
            Light light = new Light();
            var lightToggleCommand = new LightToggleCommand(light);

            CeilingFan fan = new CeilingFan();
            var ceilingFanSpeedUpCommand = new CeilingFanSpeedUpCommand(fan);
            var ceilingFanSpeedDownCommand = new CeilingFanSpeedDownCommand(fan);

            remote.AddCommand(0, lightToggleCommand);
            remote.AddCommand(1, ceilingFanSpeedUpCommand);
            remote.AddCommand(2, ceilingFanSpeedDownCommand);

            MasterCommand master = new MasterCommand();
            master.AddCommand(lightToggleCommand);
            master.AddCommand(ceilingFanSpeedUpCommand);
            master.AddCommand(ceilingFanSpeedDownCommand);

            remote.AddCommand(3, master);
            remote.AddCommand(4, new NoOpCommand());


            remote.OnButtonPress(1);
            remote.OnButtonPress(1);
            remote.OnButtonPress(1);
            remote.OnButtonPress(1);

            remote.OnButtonPress(0);
            remote.OnButtonPress(0);

            remote.OnButtonPress(3);

        }

    }







}
