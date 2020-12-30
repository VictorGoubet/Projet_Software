using System;
namespace Solution.Problem3
{
    public class Board
    {
        public Square head { get; set; }
        public Square last { get; set; }

        public Board(int board_size)
        {
            for (int i = 0; i < board_size; i++)
            {
                this.addSquare(i);
            }
        }

        public void addSquare(int value)
        {
            Square s = new Square(value);
            if (head == null)
            {
                head = s;
                last = head;
                head.next = last;
                last.next = head;
            }
            else
            {
                last.next = s;
                last = s;
                last.next = head;
                
            }
            
        }

        public override string ToString()
        {
            string res = "";
            Square actual = head;
            res += actual.ToString();
            while (actual.next != head)
            {
               
                actual = actual.next;
                res += ","+actual.ToString();
            }
            return res;

        }

    }
}
