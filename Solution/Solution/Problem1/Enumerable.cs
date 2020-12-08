using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    public interface Enumerable<T>
    {
        Iterator<T> GetEnumerator();
    }
}
