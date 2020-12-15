using System;
using Solution.Problem1;
using Solution.Problem2;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    class Program
    {

        public static void Problem1()
        {
            #region Creation of the queue
            Solution.Problem1.Queue<int> myqueue = new Solution.Problem1.Queue<int>();

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

            Solution.Problem1.Queue<int> myqueue2 = new Solution.Problem1.Queue<int>();
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


        public static void Problem2()
        {
            Dictionary<string, string> inputData = new Dictionary<string, string> { 
                ["firstDocument"] = "hello hello je suis pierrick et je suis la pour vous faire rigoler",
                ["secondDocument"] = "je suis encore là!"};

            IEnumerable<int> Reduce2(string key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new int[1] { sum };
            }

            IList<KeyValuePair<string, int>> Map2(string key, string value)
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                foreach (var word in value.Split(' '))
                {
                    result.Add(new KeyValuePair<string, int>(word, 1));
                }
                return result;
            }


            MapReduce2<string, string, string, int, int> master = new MapReduce2<string, string, string, int, int>(Map2, Reduce2);
            var result = master.Execute(inputData).ToDictionary(key => key.Key, v => v.Value);

            foreach(KeyValuePair<string, int> pair in result)
            {
                Console.WriteLine(pair.Key+": "+pair.Value);
            }







            KeyValuePair<string, int> Reduce(string key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new KeyValuePair<string, int>(key, sum);
            }

            IList<KeyValuePair<string, int>> Map(string key, string value)
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                foreach (var word in value.Split(' '))
                {
                    result.Add(new KeyValuePair<string, int>(word, 1));
                }
                return result;
            }

            MapReduce<string, string, string, int, string, int> mapreduce = new MapReduce<string, string, string, int, string, int>(Map, Reduce);
            IEnumerable<KeyValuePair<string, int>> res = mapreduce.get_result(inputData);

            foreach (KeyValuePair<string, int> pair in res)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }

        }






        static void Main(string[] args)
        {
            //Problem1();
            Problem2();
        }
    }
}
