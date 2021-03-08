using GraphLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace GraphLibrary.DataStructuresClasses
{
    public class Node<T> : INode<T>
        where T : new()
    {
        public T Value { get; internal set; }
        public Node<T> child { get; internal set; }
        public bool last { get; internal set; }
        public Node<T> right { get; internal set; }
        public Node<T> left { get; internal set; }
        internal Node(T val)
        {
            Value = val;
            child = left = right = null;
            last = true;
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

        public bool IsEmpty()
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
            if (null == priorityQueue || null == priorityQueue.head)
            {
                return;
            }

            if (null == head)
            {
                head = priorityQueue.head;
                priorityQueue = null;
                return;
            }

            if (head != priorityQueue.head)
            {
                if (!compareFunctor(head.Value, priorityQueue.head.Value))
                {
                    AddChildNode(priorityQueue.head);
                }
                else
                {
                    priorityQueue.AddChildNode(head);
                    head = priorityQueue.head;
                }
            }
            priorityQueue = null;
        }

        private void AddChildNode(Node<T> newChild)
        {
            if (null == newChild)
            {
                return;
            }

            if (null == head)
            {
                head = newChild;
                return;
            }

            newChild.last = true;
            newChild.right = null;
            newChild.left = null;

            if (null == head.child)
            {
                head.child = newChild;
                return;
            }

            var pom = head.child;
            newChild.right = pom;
            pom.left = newChild;
            newChild.left = head;
            newChild.last = false;
            head.child = newChild;

        }

        public INode<T> Insert(T elem)
        {
            Node<T> newNode = new Node<T>(elem);
            Concatenate(new PairingHeap<T>(newNode, compareFunctor));
            return newNode;
        }

        public (bool HasValue, T value) GetMinimum()
        {
            return (head != null, head != null ? head.Value : new T());
        }

        public (bool HasValue, T value) ExtractMinimum()
        {
            if (null == head)
            {
                return (false, default(T));
            }

            var prevHead = head;
            T returnValue = head.Value;

            if (null == head.child)
            {
                head = null;
                return (true, returnValue);
            }

            head = head.child;

            prevHead.last = true;
            prevHead.child = null;
            prevHead.left = null;
            prevHead.right = null;

            head.last = true;
            Node<T> nodeToMerge = head.right;
            head.right = null;
            head.left = null;

            while (null != nodeToMerge)
            {
                var mergedNode = nodeToMerge;
                nodeToMerge = nodeToMerge.right;
                mergedNode.right = null;
                mergedNode.left = null;
                mergedNode.last = true;
                PairingHeap<T> queueToMerge = new PairingHeap<T>(mergedNode, compareFunctor);
                Concatenate(queueToMerge);
            }

            return (true, returnValue);
        }

        public void DecreaseKey(Node<T> node, T newValue)
        {
            if (!compareFunctor(newValue, node.Value))
            {
                node.Value = newValue;
                if (null != node.left)
                {
                    if (node.left.child != node)
                    {
                        node.left.right = node.right;
                        if (null == node.right)
                        {
                            node.left.last = true;
                        }
                    }
                    else
                    {
                        node.left.child = node.right;
                    }
                }
                if (null != node.right)
                {
                    node.right.left = node.left;
                    if (null!=node.left && node.left.child == node)
                    {
                        node.left.child = node.right;
                    }
                }
                node.left = null;
                node.right = null;
                node.last = true;
                Concatenate(new PairingHeap<T>(node, compareFunctor));
            }
        }
    }
}
