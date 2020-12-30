using System;
using System.Collections;
using System.Text;

namespace Solution.Problem3
{
    public class Caretaker
    {
        public ArrayList mementos { get; set; } = new ArrayList();

        public void addMemento(Memento m)
        {
            mementos.Add(m);
        }

        public Memento getlastMemento()
        {
            if(mementos.Count > 2)
            {
                Memento last = (Memento)mementos[mementos.Count - 2];
                return last;
            }
            return null;


            
        }
    }
}
