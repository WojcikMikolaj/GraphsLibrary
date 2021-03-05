using GraphLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace GraphLibrary.ContainerClasses
{
    public class Node<T>
    {
        public T Value { get; internal set; }
        public Node<T> child { get; internal set; }
        public bool pointsToParent { get; internal set; }
        public Node<T> right { get; internal set; }
        internal Node(T val)
        {
            Value = val;
            child = right = null;
            pointsToParent = false;
        }
    }

    public class PairingHeap<T> : IPriorityQueue<T>
            where T : new()
    {


        Node<T> head;
        /// <summary>
        /// default: first value greater is than the second one
        /// </summary>
        Func<T, T, bool> compareFunctor;

        public PairingHeap(Func<T, T, bool> compareFunctor)
        {
            this.compareFunctor = compareFunctor;
        }

        private PairingHeap(Node<T> node, Func<T, T, bool> compareFunctor)
        {
            head = node;
            this.compareFunctor = compareFunctor;
        }

        public bool isEmpty()
        {
            return head == null;
        }

        public bool Concatenate(IPriorityQueue<T> priorityQueue)
        {
            if (priorityQueue is PairingHeap<T> pq)
            {
                Concatenate(pq);
                return true;
            }
            return false;
        }

        private void Concatenate(PairingHeap<T> priorityQueue)
        {
            if (null == head)
            {
                head = priorityQueue.head;
                return;
            }
            if (null == priorityQueue || null == priorityQueue.head)
            {
                return;
            }
            if (!compareFunctor(head.Value, priorityQueue.head.Value))
            {
                if (null == head.child)
                {
                    head.child = priorityQueue.head;
                    priorityQueue.head.right = head;
                    priorityQueue.head.pointsToParent = true;
                }
                else
                {
                    priorityQueue.head.right = head.child;
                    head.child = priorityQueue.head;
                }
                return;
            }
            if (null == priorityQueue.head.child)
            {
                priorityQueue.head.child = head;
                head.right = priorityQueue.head.child;
                head.pointsToParent = true;
                head = priorityQueue.head;
                return;
            }
            head.right = priorityQueue.head.child;
            priorityQueue.head.child = head;
            head = priorityQueue.head;
        }

        public void Insert(T elem)
        {
            Node<T> newNode = new Node<T>(elem);
            Concatenate(new PairingHeap<T>(newNode, compareFunctor));
        }

        public (bool HasValue, T value) GetMinimum()
        {
            return (head != null ? true : false, head != null ? head.Value : new T());
        }

        public (bool HasValue, T value) ExtractMinimum()
        {
            if (head == null)
                return (false, new T());
            List<PairingHeap<T>> list = new List<PairingHeap<T>>();
            Node<T> prevHead = head;
            Node<T> pom = head.child;
            while (pom != head)
            {
                PairingHeap<T> first = new PairingHeap<T>(pom, compareFunctor);

                if (pom.pointsToParent)
                {
                    list.Add(first);
                    break;
                }

                Node<T> pom2 = pom;
                pom = pom.right;
                pom2.right = null;

                PairingHeap<T> second = new PairingHeap<T>(pom, compareFunctor);
                first.Concatenate(second);
                list.Add(first);
                pom2 = pom;
                pom = pom.right;
                pom2.right = null;
            }
            while (list.Count > 1)
            {
                PairingHeap<T> first = list[0];
                list.RemoveAt(0);
                PairingHeap<T> second = list[0];
                list.RemoveAt(0);
                first.Concatenate(second);
                list.Add(first);
            }
            prevHead.child = null;
            prevHead.right = null;
            if (list.Count == 0)
            {
                head = null;
                return (true, prevHead.Value);
            }
            head = list[0].head;
            return (true, prevHead.Value);
        }

        public void DecreaseKey(Node<T> node, T newValue)
        {
            if (node == null)
                throw new ArgumentException("node is null");
            if (compareFunctor(newValue, node.Value))
                throw new ArgumentException("newValue must be lesser than node.Value");
            Node<T> pom = node;
            if (node == head)
            {
                node.Value = newValue;
                return;
            }
            while (!pom.pointsToParent)
            {
                pom = pom.right;
            }
            pom = pom.right;
            if (pom.child == node)
            {
                if (node.pointsToParent)
                {
                    pom.child = null;
                    node.pointsToParent = false;
                    node.right = null;
                    node.Value = newValue;
                    this.Concatenate(new PairingHeap<T>(node, compareFunctor));
                }
                else
                {
                    pom.child = node.right;
                    node.pointsToParent = false;
                    node.right = null;
                    node.Value = newValue;
                    this.Concatenate(new PairingHeap<T>(node, compareFunctor));
                }
                return;
            }
            pom = pom.child;
            while (pom.right != node)
            {
                pom = pom.right;
            }
            pom.right = node.right;
            if (node.pointsToParent)
            {
                pom.pointsToParent = true;
            }
            node.pointsToParent = false;
            node.right = null;
            node.Value = newValue;
            this.Concatenate(new PairingHeap<T>(node, compareFunctor));
        }
    }
}
