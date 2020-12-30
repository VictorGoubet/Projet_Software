using System;
using Solution.Problem1;
using Solution.Problem2;
using System.Collections.Generic;


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
            #region Word counting Example
            Console.WriteLine("\n----------------  Word Counting  ----------------");
            Dictionary<string, string> input_Text = new Dictionary<string, string> { 
                ["firstDocument"] = "hello hello je suis pierrick et je suis là pour vous faire rigoler",
                ["secondDocument"] = "je suis encore là !"};

            KeyValuePair<string, int> Reduce_Text(string key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new KeyValuePair<string, int>(key, sum);
            }

            IList<KeyValuePair<string, int>> Map_Text(string key, string value)
            {
                List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
                foreach (var word in value.Split(' '))
                {
                    result.Add(new KeyValuePair<string, int>(word, 1));
                }
                return result;
            }



            MapReduce<string, string, string, int, string, int> mapreduce_text = new MapReduce<string, string, string, int, string, int>(Map_Text, Reduce_Text);
            IEnumerable<KeyValuePair<string, int>> res_text = mapreduce_text.get_result(input_Text);

            foreach (KeyValuePair<string, int> pair in res_text)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }

            #endregion

            #region Columns sum example
            Console.WriteLine("\n----------------  Columns sum  ----------------");
            Dictionary<string, int[,]> input_mat = new Dictionary<string, int[,]>
            {
                ["firstmat"] = new int[,] { { 1, 2, 3 }, { 5, 9, 8 } },
                ["secondMat"] = new int[,] { { 8, 6, 3 }, { 10, 5, 3 } }

            };
            
            IList<KeyValuePair<int, int>> Map_mat(string key, int[,] value)
            {

                IList<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();
                for(int j=0; j<value.GetLength(1); j++)
                {
                    int sum = 0;
                    for(int i=0; i<value.GetLength(0); i++)
                    {
                        sum += value[i, j];
                    }
                    res.Add(new KeyValuePair<int, int>(j, sum));
                }
                return res;
            }

            KeyValuePair<int, int> Reduce_mat(int key, IEnumerable<int> values)
            {
                int sum = 0;
                foreach (int value in values)
                {
                    sum += value;
                }
                return new KeyValuePair<int, int>(key, sum);
            }

            MapReduce<string, int[,], int, int, int, int> mapreduce_mat = new MapReduce<string, int[,], int, int, int, int>(Map_mat, Reduce_mat);
            IEnumerable<KeyValuePair<int, int>> res_mat = mapreduce_mat.get_result(input_mat);

            foreach (KeyValuePair<int, int> pair in res_mat)
            {
                Console.WriteLine("Column n°"+(pair.Key+1) + ": " + pair.Value);
            }

            #endregion

        }

        
        public static void Problem3()
        {

        }

        static void Main(string[] args)
        {
            //Problem1();
            Problem2();
        }
    }
}
