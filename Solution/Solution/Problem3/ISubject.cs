using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void AddObserver(IObserver observer);
        void NotifyObserverMoving();
        void NotifyObserverState();
        void NotifyObserverDices(int dice1,int dice2);
        void NotifyObserverReplay(int time);

    }
}
