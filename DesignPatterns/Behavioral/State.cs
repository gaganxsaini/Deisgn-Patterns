using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.State
{
    namespace GumBallMachineExample
    {
        interface IGumBallMachineState
        {
            void InsertQuarter();
            void EjectQuarter();
            bool TurnCrank();
            void Dispense();
        }

        class NoQuarterState : IGumBallMachineState
        {
            readonly GumBallMachine machine;

            public NoQuarterState(GumBallMachine m) => machine = m;

            public void Dispense() => Console.WriteLine("Insert Quarters First.");

            public void EjectQuarter() => Console.WriteLine("No quarters to eject.");

            public void InsertQuarter()
            {
                Console.WriteLine("Quarter Inserted.");
                machine.SetState(machine.HasQuarterStateObj);
            }

            public bool TurnCrank()
            {
                Console.WriteLine("Insert Quarters First.");
                return false;
            }
        }

        class HasQuarterState : IGumBallMachineState
        {
            readonly GumBallMachine machine;
            public HasQuarterState(GumBallMachine m) => machine = m;

            public void Dispense() => Console.WriteLine("Turn the Crank First.");

            public void EjectQuarter()
            {
                Console.WriteLine("Quarter Ejected.");
                machine.SetState(machine.NoQuarterStateObj);
            }

            public void InsertQuarter() => Console.WriteLine("Already a Quarter.");
            public bool TurnCrank()
            {
                machine.SetState(machine.SoldStateObj);
                return true;
            }
        }

        class SoldState : IGumBallMachineState
        {
            readonly GumBallMachine machine;
            public SoldState(GumBallMachine m) => machine = m;

            public void Dispense()
            {
                machine.ReleaseBall();
                machine.SetState(machine.GumBallsCount > 0 ? machine.NoQuarterStateObj : machine.SoldOutStateObj);
            }

            public void EjectQuarter() => Console.WriteLine("No quarters to eject.");
            public void InsertQuarter() => Console.WriteLine("Please Wait.");
            public bool TurnCrank()
            {
                Console.WriteLine("Dispensing Already. Hold On.");
                return false;
            }
        }


        class SoldOutState : IGumBallMachineState
        {
            readonly GumBallMachine machine;

            public SoldOutState(GumBallMachine m) => machine = m;

            public void Dispense() => Console.WriteLine("Nothing to Dispense.");
            public void EjectQuarter() => Console.WriteLine("No quarters to eject.");
            public void InsertQuarter() => Console.WriteLine("No Gumballs.");
            public bool TurnCrank()
            {
                Console.WriteLine("No Gumballs.");
                return false;
            }
        }

        class GumBallMachine
        {
            public int GumBallsCount { get; private set; }
            IGumBallMachineState state;
            public void SetState(IGumBallMachineState newState) => state = newState;
            public void Refill(int n)
            {
                GumBallsCount = n;
                state = NoQuarterStateObj;
            }

            public IGumBallMachineState NoQuarterStateObj { get; }
            public IGumBallMachineState HasQuarterStateObj { get; }
            public IGumBallMachineState SoldStateObj { get; }
            public IGumBallMachineState SoldOutStateObj { get; }

            public GumBallMachine()
            {
                NoQuarterStateObj = new NoQuarterState(this);
                HasQuarterStateObj = new HasQuarterState(this);
                SoldStateObj = new SoldState(this);
                SoldOutStateObj = new SoldOutState(this);
                state = SoldOutStateObj;
            }

            public void ReleaseBall()
            {
                Console.WriteLine("GumBall Coming....");

                if (GumBallsCount != 0)
                    GumBallsCount--;
            }

            public void InsertQuarter() => state.InsertQuarter();
            public void EjectQuarter() => state.EjectQuarter();
            public void TurnCrank()
            {
                if(state.TurnCrank())
                    state.Dispense();
            }
        }

        public static class ExmapleCode
        {
            public static void Run()
            {
                GumBallMachine machine = new GumBallMachine();
                machine.Refill(5);
                machine.InsertQuarter();
                machine.TurnCrank();
                machine.EjectQuarter();
                machine.TurnCrank();

                for (int i = 0; i < 5; i++)
                {
                    machine.InsertQuarter();
                    machine.TurnCrank();
                }
            }
        }
    }
}
