using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem3
{
    public class MonopolyPlayerIterator : Iterator
    {
        public MonopolyPlayerIterator(Monopoly game)
        {
            this.game = game;
        }

        public bool hasNext()
        {
            if (current == null)
                return (game.First != null);
            else
                return (current.NextPlayer != null && current.NextPlayer !=game.First );
        }

        public object next()

        {
            if (current == null)
                current = game.First;
            else if (hasNext())
                current = current.NextPlayer;

            return current;
        }

        private Monopoly game;
        private Player current;
    }
}
