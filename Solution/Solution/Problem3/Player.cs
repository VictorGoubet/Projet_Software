using System;
using System.Collections.Generic;
namespace Solution.Problem3
{
    public class Player : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        public string Name { get; }
        private Square currentPosition; 
        private bool inPrison = false;
        public Player NextPlayer { get; set; }
        private int timeInPrison;
        private Caretaker caretaker;

        #region get methods
        public Square CurrentPosition
        {
            get { return currentPosition; }
        }

        public int TimeInPrison
        {
            get { return timeInPrison; }
        }

        public Caretaker Caretaker
        {
            get { return caretaker; }
        }

        public bool InPrison
        {
            get { return inPrison; }
        }



        #endregion

        #region ISubject methods
        public void RegisterObserver(IObserver observer)
        {
            Console.WriteLine("Observer Added!");
            observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void AddObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObserverMoving()
        {
            foreach (IObserver observer in observers)
            {
                observer.update(currentPosition);
            }
        }
        public void NotifyObserverReplay(int time)
        {
            foreach (IObserver observer in observers)
            {
                observer.update(time);
            }
        }
        public void NotifyObserverState()
        {
            foreach (IObserver observer in observers)
            {
                observer.update(inPrison);
            }
        }

        public void NotifyObserverDices(int dice1,int dice2)
        {
            foreach (IObserver observer in observers)
            {
                observer.update(dice1,dice2);
            }
        }

        #endregion

        #region Constructor
        public Player(string name, Square currentPosition)
        {
            this.Name = name;
            this.currentPosition = currentPosition;
            this.timeInPrison = 0;
            this.caretaker = new Caretaker();
        }
        #endregion

        #region Methods for a real game simulation
        public void Play()
        {
            int count = 0;
            bool dual = false;
            if(this.timeInPrison == 2) // If he stays more than 2 turns he will be able to move next round
            {
               
                ChangeState();
            }
            do
            {
                dual = this.RollTheDices();
                if (dual)
                {
                    count++;
                    NotifyObserverReplay(count);
                }
            }
            while (count < 3 && dual);

            if(count == 3)
            {
             
                ChangeState();
                this.GoToJail();
            }
            
        }

        public bool RollTheDices()
            {
                bool res = false;

                Random r = new Random();
                int dice1 = r.Next(1, 7);   
                int dice2 = r.Next(1, 7);
                NotifyObserverDices(dice1, dice2);

                if (this.inPrison)
                {
                    if (dice1 == dice2)
                    {
                        ChangeState();
                        this.Move(dice1 + dice2);
                    }
                    else
                    {
                        this.timeInPrison++;
                    }
                }
                else
                {
                    this.Move(dice1 + dice2);
                    if(this.currentPosition.value == 30)
                    {
                        GoToJail();
                        ChangeState();

                    }
                    else
                    {
                        if (dice1 == dice2)
                            res = true;
                    }
                    
                
                }
            
                return res;
            }

        #endregion

        #region Method for test simulation (we decide the dices)

        public void Play(int dice1,int dice2)
        {
            int count = 0;
            bool dual = false;
            if (this.timeInPrison == 2)
            {
            
                ChangeState();
            }
            do
            {
                dual = this.RollTheDices(dice1,dice2);
                if (dual)
                {
                    count++;
                    NotifyObserverReplay(count);
                }
            }
            while (count < 3 && dual);

            if (count == 3)
            {
                
                ChangeState();
                this.GoToJail();
            }

        }
        

        public bool RollTheDices(int dice1,int dice2)
        {
            bool res = false;

            NotifyObserverDices(dice1, dice2);

            if (this.inPrison)
            {
                //Console.WriteLine("Time in prison : " + this.timeInPrison);
                if (dice1 == dice2)
                {
                    ChangeState();
                    this.Move(dice1 + dice2);
                }
                else
                {
                    this.timeInPrison++;
                }
            }
            else
            {
                this.Move(dice1 + dice2);
                if (this.currentPosition.value == 30)
                {
                    GoToJail();
                    ChangeState();

                }
                else
                {
                    if (dice1 == dice2)
                        res = true;
                }


            }

            return res;
        }

        #endregion

        #region Other Methods (Move/ChangeState/GoToJail/GoToCase)
        public void Move(int number)
        {

            for (int i=0; i < number; i++)
            {
                currentPosition = currentPosition.next;
            }
            NotifyObserverMoving();

        }

        public void ChangeState()
        {
            inPrison = !inPrison;
            this.timeInPrison = 0;
            NotifyObserverState();
        }

        public void GoToJail()
        {
            GoToCase(10);
        }

        public void GoToCase(int boxvalue)
        {
            while (currentPosition.value != boxvalue)
            {
                currentPosition = currentPosition.next;
            }
        }

        #endregion

        #region Overriding usual methods
        public override bool Equals(object value)
        {
            Player p2 = value as Player;

            return !Object.ReferenceEquals(null, p2)
                && String.Equals(this.GetHashCode(),p2.GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public static bool operator ==(Player p1, Player p2)
        {
            if (Object.ReferenceEquals(p1, p2))
            {
                return true;
            }

            // Ensure that p1 isn't null
            if (Object.ReferenceEquals(null, p1))
            {
                return false;
            }

            return (p1.Equals(p2));
        }

        public static bool operator !=(Player p1, Player p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            if (this.inPrison)
            {
                return Name + " is in prison on case " + currentPosition.ToString();
            }
            return Name + " is on case " + currentPosition.ToString();
        }
        #endregion
    }
}
