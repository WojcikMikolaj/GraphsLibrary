using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Interfaces
{
    public interface IEdgeCollection
    {
        Edge Pop();
        Edge Peek();
        void Push(Edge e);
        bool IsEmpty();
    }
}
