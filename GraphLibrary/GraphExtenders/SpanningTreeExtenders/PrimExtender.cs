using GraphLibrary.DataStructuresClasses;
using GraphLibrary.ExceptionClasses;
using System;

namespace GraphLibrary.GraphExtenders.SpanningTreeExtenders
{
    public static class PrimExtender
    {

        public static (bool connected, Graph tree) Prim(this Graph g, int startingVertex = 0)
        {
            if (g.Directed)
            {
                throw new WrongGraphException("Graph must be an undirected graph");
            }

            if (startingVertex < 0 || startingVertex >= g.VerticesCount)
            {
                throw new ArgumentOutOfRangeException("startingVertex");
            }

            Graph tree = g.IsolatedGraph(g.Directed);
            PairingHeap<Edge> queue = new PairingHeap<Edge>((x, y) => x.Weight > y.Weight);

            UnionFind unionFind = new UnionFind(g.VerticesCount);

            foreach (var e in g.GetEdgesFrom(startingVertex))
            {
                queue.Insert(e);
            }

            int c = 0;
            while (true)
            {
                if (queue.IsEmpty() || c == g.VerticesCount - 1)
                {
                    return (c == g.VerticesCount - 1 ? true : false, tree);
                }

                var e = queue.ExtractMinimum();

                if (unionFind.FindParent(e.value.From) != unionFind.FindParent(e.value.To))
                {
                    tree.AddEdge(e.value);

                    unionFind.Union(e.value.From, e.value.To);

                    foreach (var f in g.GetEdgesFrom(e.value.To))
                    {
                        queue.Insert(f);
                    }

                    c++;
                }
            }

        }
    }
}
