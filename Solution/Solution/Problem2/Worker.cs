using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Solution.Problem2
{
    public class Worker<T1, T2, T3>
    {   
        // Stock workers to get all results next
        static public List<Worker<T1, T2, T3>> list_worker = new List<Worker<T1, T2, T3>>();

        // Define really generic delegate 
        public delegate T3 generic_function(T1 key, T2 val);

        private generic_function function;
        public Thread my_thread;
        private T1 key;
        private T2 val;

        T3 res { get; set; }

        public Worker(generic_function function, T1 key, T2 val)
        {
            this.key = key;
            this.val = val;
            this.function = function;

            list_worker.Add(this);
            // Initialize our thread
            my_thread = new Thread(new ThreadStart(this.thread));
        }

        // Define the function of our thread
        private void thread()
        {
            this.res = this.function(key, val);
        }

        // Launch the thread
        public void execute()
        {
            my_thread.Start();
        }

        // Get the result of all workers
        public static List<T3> get_result()
        {
            // Iterate throw all workers and stock the result
            List<T3> final_res = new List<T3>();
            foreach (Worker<T1, T2, T3> w in list_worker)
            {
                final_res.Add(w.res);
            }
            return final_res;
        }

        // Join all workers
        public static void join_workers()
        {
            foreach(Worker<T1, T2, T3> w in list_worker)
            {
                w.my_thread.Join();
            }
        }

    }
}
