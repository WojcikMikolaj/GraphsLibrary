using GraphLibrary.DataStructuresClasses;
using GraphLibrary.ExceptionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.GraphExtenders.SpanningTreeExtenders
{
    public static class KruskalExtender
    {
        public static (bool connected, Graph tree) Kruskal(this Graph g)
        {
            if (g.Directed)
            {
                throw new WrongGraphException("Graph must be an undirected graph");
            }

            Graph tree = g.IsolatedGraph(g.Directed);
            PairingHeap<Edge> queue = new PairingHeap<Edge>((x, y) => x.Weight > y.Weight);

            int[] tab = new int[g.VerticesCount];
            for (int i = 0; i < g.VerticesCount; i++)
            {
                tab[i] = i;
            }

            for (int i = 0; i < g.VerticesCount; i++)
            {
                foreach (var e in g.GetEdgesFrom(i))
                {
                    queue.Insert(e);
                }
            }

            int c = 0;
            while (true)
            {
                if (queue.IsEmpty() || c == g.VerticesCount - 1)
                {
                    return (c == g.VerticesCount - 1 ? true : false, tree);
                }

                var e = queue.ExtractMinimum();

                if (tab[e.value.From] != tab[e.value.To])
                {
                    tree.AddEdge(e.value);
                    if (e.value.From > e.value.To)
                    {
                        tab[e.value.From] = tab[e.value.To];
                    }
                    else
                    {
                        tab[e.value.To] = tab[e.value.From];
                    }
                    c++;
                }
            }
        }
    }
}
