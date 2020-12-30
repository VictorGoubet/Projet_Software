using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    public class Queue<T>: Enumerable<T> where T : struct
    {

        public Node<T> root { get; set; }

        #region Constructors

        //Constructor with root
        public Queue(Node<T> root)
        {
            this.root = root;
        }
        
        //Empty constructor
        public Queue()
        {
            this.root = null;
        }
        #endregion

        #region enqueue / dequeue

        // Add a value at the end of the queue
        public void enqueue(T val)
        {
            Node<T> node = new Node<T>(val);
            node.next = this.root;
            this.root = node;

        }

        // Dequeue the last value added
        public T? dequeue()
        {
            T? val = null;
            if(this.root != null)
            {
                val = this.root.data;
                this.root = this.root.next;
            }
            
            return val;
        }

        #endregion

        #region Utility functions

        // Clear all values of the queue
        public void clear()
        {
            this.root = null;
        }

        //Check if a value is in the current queue
        public bool contains(T val)
        {
            foreach(Node<T> node in this)
            {
                if (node.data.Equals(val))
                {
                    return true;
                }
            }
            return false;

        }

        // Return the lenght of the current queue
        public int Count()
        {
            int count = 0;
            foreach(Node<T> node in this)
            {
                count++;
            }
            return count;

        }

        #endregion

        #region Other functions

        // Define the way a queue is displayed
        public override string ToString()
        {
            string res = "";

            // Add the content of each node in a string 
            foreach(Node<T> node in this)
            {
                res += node.ToString() + ",";
            }

            // Reverse the string inorder to have a logical visualization 
            if (res != "")
            {
                res = res.Substring(0, res.Length - 1);
                string[] t = res.Split(',');
                Array.Reverse(t);
                res = String.Join(", ", t);
            }
            
            return "[" + res + "]";
        }

        // Return true if the current queue is equal to the other queue
        public override bool Equals(object obj)
        {

            //Check for null, different type and different length
            if ((obj == null) || !this.GetType().Equals(obj.GetType()) || (obj as Queue<T>).Count() != this.Count())
            {
                return false;
            }
            else
            {
                Queue<T> queue2 = (Queue<T>)obj;

                // A queue is equal to antoher if each of his nodes is equals to the other's nodes
                Node<T> node1 = this.root;
                Node<T> node2 = queue2.root;

                while(node1 != null && node2 != null)
                {
                    if (!node1.Equals(node2))
                    {
                        return false;
                    }
                    node1 = node1.next;
                    node2 = node2.next;
                }
                return true;
            }
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        // Allows to iterate throw this queue
        public Iterator<T> GetEnumerator()
        {
            return new QueueIterator<T>(this);
        }

        #endregion
    }
}

