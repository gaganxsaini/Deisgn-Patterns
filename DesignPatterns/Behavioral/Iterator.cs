using System;

namespace DesignPatterns.Behavioral.Iterator
{
    namespace GoFExample
    {
        public interface IIterator<T>
        {
            void First();
            void Next();
            T GetCurrent();
            bool HasNext();
        }

        public class ListIterator<T> : IIterator<T>
        {
            private readonly List<T> _list = null;
            int currentPos = 0;

            public ListIterator(List<T> l) => _list = l;
            
            public void First() => currentPos = 0;
            
            public T GetCurrent() => _list[currentPos];
            
            public bool HasNext() => currentPos < _list.Count;
            
            public void Next()
            {
                if (!HasNext())
                    throw new Exception("End of collection");
                currentPos++;
            }
        }

        public class List<T>
        {
            const int MAX = 20;
            readonly T[] items = new T[MAX];

            public int Count { get; private set; }

            public void Insert(T el)
            {
                if (Count < MAX)
                {
                    items[Count] = el;
                    Count++;
                }
                else
                    throw new InsufficientMemoryException("Array Full");
            }

            public T this[int index]
            {
                get 
                {
                    if (index < Count && index >= 0)
                        return items[index];
                    else
                        throw new IndexOutOfRangeException("Invalid Index");
                }
                set 
                {
                    if (index < Count && index >= 0)
                        items[index] = value;
                    else
                        throw new IndexOutOfRangeException("Invalid Index");
                }
            }

            public ListIterator<T> GetIterator() => new ListIterator<T>(this);
        }

        static class ExampleCode
        {
            public static void Run()
            {
                List<int> list = new List<int>();

                for (int i = 0; i < 20; i++)
                    list.Insert(i);

                for (var itr = list.GetIterator(); itr.HasNext(); itr.Next())
                    Console.WriteLine(itr.GetCurrent());
            }
        }
    }
}
