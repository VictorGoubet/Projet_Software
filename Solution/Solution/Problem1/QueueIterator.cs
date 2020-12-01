using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Problem1
{
    class QueueIterator<T> : Iterator<T>
    {
        private Queue<T> queue;
        public Node<T> Current { get; set; }

        public QueueIterator(Queue<T> queue)
        {
            this.queue = queue;
            this.Current = new Node<T>();
            this.Current.next = this.queue.root;
        }

        
        // Move to the next element of the queue and check if we are out of it
        public bool MoveNext()
        {
            this.Current = this.Current.next;

            if (this.Current == null)
            {
                return false;
            }
            return true;
        }

        // Reset the current element to the head
        public void Reset()
        {
            this.Current = new Node<T>();
            this.Current.next = this.queue.root;

        }
    }
}
