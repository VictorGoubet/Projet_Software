using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public interface Iterator
    {
        bool hasNext();
        object next();
    }
}
