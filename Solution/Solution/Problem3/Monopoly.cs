using System;
namespace Solution.Problem3
{
    public class Monopoly : IObserver, Aggregate
    {
        public Player First { get; set; }
        private Player last;
        private Player actual;
        private readonly Board board = new Board(40);
        private bool started;
        private static Monopoly _instance;

        #region constructor
        private Monopoly()
        {
            this.First = null;
            this.last = null;
            this.actual = null;
            this.started = false;
        }
        #endregion

        #region Special get/set
        public Player GetPlayer(string pname)
        {
            Player pointer = First;
            while(pointer.NextPlayer!=First && !pointer.Name.Equals(pname))
            {
                pointer = pointer.NextPlayer;
            }
            if (pointer.Name.Equals(pname))
            {
                return pointer;
            }
            return null;

        }

        public Player GetPlayer(int index)
        {
            Player pointer = First;
            int idx = 0;
            while (pointer.NextPlayer != First && idx!=index)
            {
                pointer = pointer.NextPlayer;
                idx++;
            }
            if (idx == index)
            {
                return pointer;
            }
            else
            {
                throw new IndexOutOfRangeException();
               
            }
            
        }

        // Override operator [] 
        public Player this[int index]
        {
            get
            {
                Console.WriteLine("Im here");
                return GetPlayer(index);
            }

       
        }
        #endregion

        #region Aggregate methods
        public Iterator createIterator()
        {
            return new MonopolyPlayerIterator(this);
        }
        #endregion

        #region Singleton method
        public static Monopoly GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Monopoly();
            }
            return _instance;
        }
        #endregion

        #region IObserver methods
        public void update(Square position)
        {
            if (position.value == 30)
            {
                Console.WriteLine(actual.Name + " moved to case " + position.value + ", the go to prison case.\n");
            }
            else
                Console.WriteLine(actual.Name + " moved to case " + position.value+"\n");
        }
        public void update(bool state)
        {
            if (state)
            {
                Console.WriteLine(actual.Name + " is now in prison.");
            }
            else
            {
                Console.WriteLine(actual.Name + " is now out of prison.");
            }

        }
        public void update(int time)
        {
            Console.WriteLine("Replaying... (" + time + " time)");
        }
        public void update(int dice1, int dice2)
        {
            if (dice1== dice2)
            {
                Console.WriteLine(actual.Name + " got a double " + dice1 + ".");
            }
            else
            {
                Console.WriteLine(actual.Name + " got a " + dice1 + " and a "+dice2+".");
            }
        }

        #endregion

        #region Adding/Deleting Players

        public void AddPlayer(string name)
        {
            if (started == false)
            {


                Player p = new Player(name, board.head);
                p.RegisterObserver(this);
                if (First == null)
                {
                    First = p;
                    last = First;
                    First.NextPlayer = last;
                    last.NextPlayer = First;
                    actual = First;
                }
                else
                {
                    last.NextPlayer = p;
                    last = p;
                    last.NextPlayer = First;

                }
            }
            else
            {
                Console.WriteLine("\nSorry "+name+" the game has already started, you can't join in..\n");
            }
        }

        public void DeletePlayer(string pname)
        {
            if (this.First != null)
            {


                Player pointer = First;
                while (pointer.NextPlayer != First && !pointer.NextPlayer.Name.Equals(pname))
                {
                    pointer = pointer.NextPlayer;
                }
                if (pointer.NextPlayer.Name.Equals(pname))
                {
                    if (last == First)
                        this.Restart();
                    //If we delete the first or last node we have to redefine those
                    if (pointer.NextPlayer == this.First)
                        this.First = pointer.NextPlayer.NextPlayer;

                    if (pointer.NextPlayer == this.last)
                    {
                        this.last = pointer;
                    }

                    pointer.NextPlayer = pointer.NextPlayer.NextPlayer;

                }
            }
            
        }
        #endregion

        #region Simulating the game

        public void PlayTheTurn()
        {
            started = true;
            actual.Play();
            actual.Caretaker.addMemento(Save());
            actual = actual.NextPlayer;
        }

        public void PlayRound()
        {
            this.actual = this.First;
            do
            {
                PlayTheTurn();
            }
            while (actual != this.First);
        }

        public void Restart() //Reset the game (delete all players)
        {
            this.First = null;
            this.last = null;
            this.actual = null;
            this.started = false;
        }
        #endregion

        #region Memento functions Save/Restore/ShowHistoric
        public Memento Save()
        {
            //To save all the historic of the position player
            Console.WriteLine("Originator: "+actual.Name+"'s move Saved to Memento.\n");
            return new Memento(actual.CurrentPosition.value);
        }
        public void ShowHistoric(string pname)
        {
            Player p = GetPlayer(pname);
            if (p != null) {

                for (int i = 0; i < p.Caretaker.mementos.Count; i++)
                    Console.Write(p.Caretaker.mementos[i] + ", ");

            }
            else
            {
                Console.WriteLine(pname + " doesn't play in this game :(, he should watch tv because this game goes on for hours...");
            }
         


        }
        public void Restore(string pname)
        {
            //Restore ought to restore the last position of the player
            //!CAREFULL we chose that when restoring an ulterior position it is imposible to to the position we suppress
            Player p = GetPlayer(pname);
            if (p != null)
            {
                Memento m = p.Caretaker.getlastMemento();
                if (m != null)
                {
                    p.Caretaker.mementos.RemoveAt(p.Caretaker.mementos.Count - 1);
                    p.GoToCase(m.state);
                    Console.WriteLine("\nOriginator: State after restoring from Memento: " + p);
                }
                else
                {
                    Console.WriteLine("\nThe player has only played once.\n");
                }
                
            }
            else
            {
                Console.WriteLine(pname + " doesn't play in this game :(, he should watch tv because this game goes on for hours...");

            }
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            Iterator it = this.createIterator();
            string ans="";
            while (it.hasNext())
            {
                Player p = (Player)it.next();
                ans += p.ToString()+"\n";
            }
            return ans;
        }
        #endregion
    }
}
