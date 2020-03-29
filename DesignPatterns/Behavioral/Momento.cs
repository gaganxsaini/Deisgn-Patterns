using System;

namespace DesignPatterns.Behavioral.Momento
{
    namespace SimpleExample
    {
        interface IMomentoNarrowInterface
        {
            public string TimeStamp { get; set; }
        }

        interface IMomnetoWideInterface
        {
            public ObjectState State { get; set; }
        }

        class ObjectState
        {
            public int Id { get; set; }
            public string Color { get; set; }
            public int BorderThickness { get; set; }
        }

        class Memento : IMomentoNarrowInterface, IMomnetoWideInterface
        {
            public string TimeStamp { get; set; }
            public ObjectState State { get; set; }
        }

        class Originator
        {
            private ObjectState State { get; set; }
            public IMomentoNarrowInterface GetMemento() => new Memento() { State = State, TimeStamp = "29/03/2020 1:11 PM"};
            public Originator() => State = new ObjectState() { Id = 1, Color = "Black", BorderThickness = 1 };
            public void StateChangingOperation() => State = new ObjectState() { Id = 5, Color = "Red", BorderThickness = 2 };
            public override string ToString() => $"Object {State.Id} of {State.Color} color with {State.BorderThickness} thickness.";

            public void RevertToState(IMomentoNarrowInterface prevMemento)
            {
                var m = (IMomnetoWideInterface)prevMemento;
                State = m.State;
                Console.WriteLine("Previous State Restored.");
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                Originator org = new Originator();
                Console.WriteLine(org);
                var m = org.GetMemento();
                Console.WriteLine($"Momento Captured at {m.TimeStamp}");
                org.StateChangingOperation();
                Console.WriteLine(org);
                org.RevertToState(m);
                Console.WriteLine(org);
                Console.ReadKey();
            }
        }
    }


}
