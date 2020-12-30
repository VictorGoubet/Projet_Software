using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public interface IObserver
    {
        void update(Square position);
        void update(bool state);
        void update(int dice1,int dice2);
        void update(int time);
    }
}
