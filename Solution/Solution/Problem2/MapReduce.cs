using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Solution.Problem2
{
    public class MapReduce<K1, V1, K2, V2, K3, V3>
    {
        // Define map and reduce delegate
        public delegate IEnumerable<KeyValuePair<K2, V2>> map_function(K1 key, V1 val);
        public delegate KeyValuePair<K3, V3> reduce_function(K2 key, IEnumerable<V2> val);

        private map_function map;
        private reduce_function reduce;

        public MapReduce(map_function map, reduce_function reduce)
        {
            this.map = map;
            this.reduce = reduce;
        }

        // Define the Shuffling function
        private Dictionary<K2, List<V2>> shuffling(IEnumerable<IEnumerable<KeyValuePair<K2, V2>>> res_map)
        {
            // Make flatten our array of result which are also arrays 
            // Ex: [[("j",1),("k",1)], [(k, 1), ("l",2)]] to [("j",1),("k",1),(k, 1), ("l",2)]
            var concatenate_res_map = res_map.SelectMany(i => i).ToArray();

            // Group all values by their key 
            // Ex: {j=(1), k=(1, 1), l=(1)}
            var grouped_res_map = from pair in concatenate_res_map
                                  group pair.Value by pair.Key into g
                                  select g;
            return grouped_res_map.ToDictionary(key => key.Key, v => v.ToList());
        }

        // Apply map function on each subset throw a independant worker 
        private IEnumerable<IEnumerable<KeyValuePair<K2, V2>>> apply_all_map(IEnumerable<KeyValuePair<K1, V1>> input)
        {
            // for each pair, create a worker and give it the map function with the input
            // Then, the worker execute the function on the input and save the result
            foreach(KeyValuePair<K1, V1> pair in input)
            {
                Worker<K1, V1, IEnumerable<KeyValuePair<K2, V2>>> my_worker = new Worker<K1, V1, IEnumerable<KeyValuePair<K2, V2>>>((a, b) => map(a, b), pair.Key, pair.Value);
                my_worker.execute();
            }

            // Wait all worker have finished
            Worker<K1, V1, IEnumerable<KeyValuePair<K2, V2>>>.join_workers();
            // Get the result of all workers
            IList<IEnumerable<KeyValuePair<K2, V2>>> res_map = Worker<K1, V1, IEnumerable<KeyValuePair<K2, V2>>>.get_result();
            return res_map;
        }

        // Apply reduce function on each subset throw a independant worker 
        private IEnumerable<KeyValuePair<K3, V3>> apply_all_reduce(Dictionary<K2, List<V2>> res_shuffling)
        {
            // for each pair, create a worker and give it the reduce function with the result of shuffling
            // Then, the worker execute the function on the shuffling result and save the result
            foreach (KeyValuePair<K2, List<V2>> pair in res_shuffling)
            {
                Worker<K2, List<V2>, KeyValuePair<K3, V3>> my_worker = new Worker<K2, List<V2>, KeyValuePair<K3, V3>>((a, b) => reduce(a, b), pair.Key, pair.Value);
                my_worker.execute();
            }

            // Wait all worker have finished
            Worker<K2, List<V2>, KeyValuePair<K3, V3>>.join_workers();
            // Get the final result of all workers
            IList<KeyValuePair<K3, V3>> res_reduce = Worker<K2, List<V2>, KeyValuePair<K3, V3>>.get_result();
            return res_reduce;
        }

        // Deal with the deferents steps of MapReduce
        public IEnumerable<KeyValuePair<K3, V3>> get_result(IEnumerable<KeyValuePair<K1, V1>> input)
        {
            // First, apply map function independantly
            IEnumerable<IEnumerable<KeyValuePair<K2, V2>>> res_map = this.apply_all_map(input);
            // Then, apply shuffling
            Dictionary<K2, List<V2>> res_shuffling = this.shuffling(res_map);
            // And finally, reduce to the solution
            IEnumerable<KeyValuePair<K3, V3>> res_reduce = this.apply_all_reduce(res_shuffling);
            return res_reduce;

        }
    }
}
