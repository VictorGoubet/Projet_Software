using System;
namespace Solution.Problem3
{
    public class Square
    {

        public int value { get; set; }
        public Square next { get; set; }

        public Square(int value)
        {
            this.value = value;
            this.next = null;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }


    }
}
