using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public interface Aggregate
    {
        Iterator createIterator();
    }
}
