using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Problem2
{
    class MapReduce<K1, V1, K2, V2, K3, V3>
    {

        public delegate IEnumerable<KeyValuePair<K2, V2>> map_function(K1 key, V1 val);
        public delegate KeyValuePair<K3, V3> reduce_function(K2 key, IEnumerable<V2> val);



        private map_function map;
        private reduce_function reduce;

        public MapReduce(map_function map, reduce_function reduce)
        {
            this.map = map;
            this.reduce = reduce;
        }

        private Dictionary<K2, List<V2>> Shuffling(List<IEnumerable<KeyValuePair<K2, V2>>> res_map)
        {
            Dictionary<K2, List<V2>> res_shuffling = new Dictionary<K2, List<V2>>();
            foreach(IEnumerable<KeyValuePair<K2, V2>> x in res_map)
            {
                foreach(KeyValuePair<K2, V2> y in x)
                {
                    if (res_shuffling.ContainsKey(y.Key))
                    {
                        res_shuffling[y.Key].Add(y.Value);
                    }
                    else
                    {
                        List<V2> temp = new List<V2>();
                        temp.Add(y.Value);
                        res_shuffling.Add(y.Key, temp);
                    }  
                }
            }
            return res_shuffling;
        }

        private List<IEnumerable<KeyValuePair<K2, V2>>> apply_all_map(IEnumerable<KeyValuePair<K1, V1>> input)
        {
            List<IEnumerable<KeyValuePair<K2, V2>>> res_map = new List<IEnumerable<KeyValuePair<K2, V2>>>();
            foreach (KeyValuePair<K1, V1> res_splitting in input)
            {
                res_map.Add(this.map(res_splitting.Key, res_splitting.Value));
            }
            return res_map;
        }

        private List<KeyValuePair<K3, V3>> apply_all_reduce(Dictionary<K2, List<V2>> res_shuffling)
        {
            List<KeyValuePair<K3, V3>> res = new List<KeyValuePair<K3, V3>>();
            foreach(KeyValuePair<K2, List<V2>> x in res_shuffling)
            {
                res.Add(this.reduce(x.Key, x.Value));
            }
            return res;
        }
        

        public IEnumerable<KeyValuePair<K3, V3>> get_result(IEnumerable<KeyValuePair<K1, V1>> input)
        {

            List<IEnumerable<KeyValuePair<K2, V2>>> res_map = this.apply_all_map(input);
            Dictionary<K2, List<V2>> res_shuffling = this.Shuffling(res_map);
            List<KeyValuePair<K3, V3>> res = this.apply_all_reduce(res_shuffling);
            return res;

        }




    }
}
