using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    public class Node<T>
    {

        public T data { get; set; }
        public Node<T> next { get; set; }

        #region Constructors

		//Constructor with intial value
        public Node(T val)
		{
			data = val;
			next = null;
		}

		//Empty constructor
		public Node()
        {
			data = default(T);
			next = null;
        }
        #endregion

        #region Other functions

        // Define how to compare a node to other object
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Node<T> node2 = (Node<T>)obj;

                return node2.data.Equals(this.data);
            }
        }

        //Override GetHasCode for the Equals function
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        // Define the way a node is display
        public override string ToString()
		{
			return this.data.ToString();
		}
        #endregion

    }
}
