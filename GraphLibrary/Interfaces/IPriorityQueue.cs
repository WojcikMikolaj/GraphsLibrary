using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Interfaces
{
    public interface IPriorityQueue<T>
       where T : new()
    {
        bool Concatenate(IPriorityQueue<T> priorityQueue);
        INode<T> Insert(T newElement);
        (bool HasValue, T value) GetMinimum();
        (bool HasValue, T value) ExtractMinimum();
        public bool IsEmpty();
    }
}
