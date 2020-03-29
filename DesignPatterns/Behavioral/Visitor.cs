using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Visitor
{
    namespace GoFExample
    {
        interface IEquipment
        {
            string Name { get; }
            double Price { get; }
            double DiscountedPrice { get; }
            void Accept(IEquipmentVisitor visitor);
        }

        class FloppyDisk : IEquipment
        {
            public string Name { get; private set; }
            public double Price { get; private set; }
            public double DiscountedPrice { get; private set; }
            public FloppyDisk(string name, double price, double dp) => (Name, Price, DiscountedPrice) = (name, price, dp);
            public void Accept(IEquipmentVisitor visitor) => visitor.VisitFloppyDisk(this);
        }

        class Chassis : IEquipment
        {
            List<IEquipment> parts = new List<IEquipment>();
            public string Name { get; private set; }
            public double Price { get; private set; }
            public double DiscountedPrice { get; private set; }
            public Chassis(string name, double price, double dp) => (Name, Price, DiscountedPrice) = (name, price, dp);
            public void AddItem(IEquipment item) => parts.Add(item);

            public void Accept(IEquipmentVisitor visitor)
            {
                foreach (var item in parts)
                    item.Accept(visitor);

                visitor.VisitChasis(this);
            }
        }

        interface IEquipmentVisitor
        {
            void VisitFloppyDisk(FloppyDisk d);
            void VisitChasis(Chassis c);
        }

        class PricingVisitor : IEquipmentVisitor
        {
            public double result = 0;
            public void VisitChasis(Chassis c) => result += c.Price;
            public void VisitFloppyDisk(FloppyDisk d) => result += d.Price;
        }

        public static class ExampleCode
        {
            public static void Run()
            {
                Chassis c = new Chassis("Main Chasis", 10, 10);
                c.AddItem(new FloppyDisk("Disk 1", 50, 20));
                c.AddItem(new FloppyDisk("Disk 2", 30, 10));
                c.AddItem(new FloppyDisk("Disk 3", 60, 30));

                PricingVisitor visitor = new PricingVisitor();
                c.Accept(visitor);

                Console.WriteLine($"Total Price : {visitor.result}");
            }
        }

    }
}
