using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public class Memento
    {
        public int state { get; }
        public Memento(int state)
        {
            this.state = state;
        }

        public override string ToString()
        {
            return state.ToString();
        }


    }
}
