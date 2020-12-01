using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    interface Enumerable<T>
    {
        Iterator<T> GetEnumerator();
    }
}
