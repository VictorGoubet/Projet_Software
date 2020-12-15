using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Solution.Problem2
{
    public class MapReduce2<K1,V1, K2,V2,V3>
    {

        public delegate IEnumerable<KeyValuePair<K2, V2>> MapFunction(K1 key, V1 value);
        public delegate IEnumerable<V3> ReduceFunction(K2 key, IEnumerable<V2> values);


        private MapFunction _map;
        private ReduceFunction _reduce;


        public MapReduce2(MapFunction mapFunction, ReduceFunction reduceFunction)
        {
            _map = mapFunction;
            _reduce = reduceFunction;
        }

        private IEnumerable<KeyValuePair<K2, V2>> Map(IEnumerable<KeyValuePair<K1, V1>> input)
        {
            var q = from pair in input
                    from mapped in _map(pair.Key, pair.Value)
                    select mapped;

            return q;
        }

        private IEnumerable<KeyValuePair<K2, V3>> Reduce(IEnumerable<KeyValuePair<K2, V2>> intermediateValues)
        {
            // First, group intermediate values by key 
            var groups = from pair in intermediateValues
                         group pair.Value by pair.Key into g
                         select g;
            // Reduce on each group 
            var reduced = from g in groups
                          let k2 = g.Key
                          from reducedValue in _reduce(k2, g)
                          select new KeyValuePair<K2, V3>(k2, reducedValue);

            return reduced;
        }

        public IEnumerable<KeyValuePair<K2, V3>> Execute(IEnumerable<KeyValuePair<K1, V1>> input)
        {
            return Reduce(Map(input));
        }
    }
}
