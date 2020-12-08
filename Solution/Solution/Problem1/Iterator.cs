using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    public interface Iterator<T>
    {
        Node<T> Current { get; set; }
        bool MoveNext();
        void Reset();
    }
}
