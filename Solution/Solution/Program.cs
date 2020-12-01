using System;
using Solution.Problem1;

namespace Solution
{
    class Program
    {

        public static void Problem1()
        {
            #region Creation of the queue
            Queue<int> myqueue = new Queue<int>();

            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);
            #endregion

            #region First print
            Console.Write("My queue : ");
            Console.WriteLine(myqueue);
            Console.Write("Length : ");
            Console.WriteLine(myqueue.Count());
            #endregion

            #region Dequeue
            Console.WriteLine("----- DEQUEUE -----");
            
            Console.Write("Dequeued value : ");
            Console.WriteLine(myqueue.dequeue());
            Console.Write("My queue : ");
            Console.WriteLine(myqueue);
            Console.Write("Length : ");
            Console.WriteLine(myqueue.Count());
            #endregion

            #region Contains
            Console.WriteLine("----- CONTAINS -----");

            Console.Write("Is my queue contains 5 ? : ");
            Console.WriteLine(myqueue.contains(5));
            Console.Write("Is my queue contains 147 ? : ");
            Console.WriteLine(myqueue.contains(147));
            #endregion

            #region Equals
            Console.WriteLine("----- EQUALS -----");

            Queue<int> myqueue2 = new Queue<int>();
            myqueue2.enqueue(5);
            myqueue2.enqueue(10);

            Console.Write("My queue : ");
            Console.WriteLine(myqueue);
            Console.Write("My second queue : ");
            Console.WriteLine(myqueue2);
            Console.Write("Are my queue equals to second queue ? : ");
            Console.WriteLine(myqueue.Equals(myqueue2));
            Console.Write("Are my queue equals to 7 ? : ");
            Console.WriteLine(myqueue.Equals(7));
            Console.Write("Are my queue equals to 'hello' ? : ");
            Console.WriteLine(myqueue.Equals("hello"));


            #endregion

            #region Clear
            Console.WriteLine("----- CLEAR -----");

            myqueue.clear();
            Console.Write("My queue : ");
            Console.WriteLine(myqueue);
            #endregion

            
        }







        static void Main(string[] args)
        {
            Problem1();
        }
    }
}
