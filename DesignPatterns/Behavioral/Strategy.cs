using System;

namespace DesignPatterns.Behavioral.Strategy
{
    namespace DuckExample
    {
        public interface IFlyBehavior
        {
            void Fly();
        }

        public interface IQuackBehavior
        {
            void Quack();
        }

        public class FlyWithWings : IFlyBehavior
        {
            public void Fly() => Console.WriteLine("Flapping the wings");
        }

        public class NoFly : IFlyBehavior
        {
            public void Fly() { }
        }
        
        public class Quacker : IQuackBehavior
        {
            public void Quack() => Console.WriteLine("Quacking...");
        }

        public class Squeaker : IQuackBehavior
        {
            public void Quack() => Console.WriteLine("Squeking...");
        }

        public class Mute : IQuackBehavior
        {
            public void Quack() { }
        }

        public abstract class Duck
        {
            protected IFlyBehavior flyBehavior;
            protected IQuackBehavior quackBehavior;

            public void PerformQuack() => quackBehavior.Quack();
            public void PerformFly() => flyBehavior.Fly();
            public virtual void Display() => Console.WriteLine("I'm a Duck");
        }

        public class MallordDuck : Duck
        {
            public MallordDuck()
            {
                flyBehavior = new FlyWithWings();
                quackBehavior = new Quacker();
            }

            public override void Display() => Console.WriteLine("I'm a Mallord Duck");
        }

        public static class ExampleCode
        {
            public static void Run()
            {
                Duck d = new MallordDuck();
                d.Display();
                d.PerformFly();
                d.PerformQuack();
            }
        }
    }
}
