using GraphLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.ContainerClasses
{
    public class OrdinaryList<T>
    {
        private class Node
        {
            public Node Next { get; set; }
            public T Data { get; set; }
        }

        int ElementsCount
        {
            get => elementsCount;
            set
            {
                if (0 == elementsCount && value < 0)
                {
                    return;
                }
                elementsCount = value;
            }
        }
        int elementsCount = 0;
        Node head = null;
        Node tail = null;
        public bool IsEmpty()
        {
            return 0 == ElementsCount;
        }

        public T Peek()
        {
            if (null != head)
            {
                return head.Data;
            }
            return default(T);
        }

        public T Pop()
        {
            T top = Peek();

            ElementsCount--;

            if (null != head)
            {
                head = head.Next;
            }

            if (null == head)
            {
                tail = null;
            }
            return top;
        }

        public void Push(T e)
        {
            Node node = new Node()
            {
                Data = e
            };
            ElementsCount++;

            if (null == tail)
            {
                head = node;

            }
            else
            {
                tail.Next = node;
            }

            tail = node;
        }
    }
}
