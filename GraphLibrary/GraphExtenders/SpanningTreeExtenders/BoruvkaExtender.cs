using GraphLibrary.DataStructuresClasses;
using GraphLibrary.ExceptionClasses;
using System.Collections.Generic;

namespace GraphLibrary.GraphExtenders.SpanningTreeExtenders
{
    public static class BoruvkaExtender
    {

        public static (bool connected, Graph tree) Boruvka(this Graph g)
        {
            if (g.Directed)
            {
                throw new WrongGraphException("Graph must be an undirected graph");
            }

            int numberOfTrees = g.VerticesCount;
            Graph tree = g.IsolatedGraph(false);
            UnionFind unionFind = new UnionFind(g.VerticesCount);
            PairingHeap<Edge>[] heaps = new PairingHeap<Edge>[g.VerticesCount];
            HashSet<int> activeHeaps = new HashSet<int>();

            for (int i = 0; i < g.VerticesCount; i++)
            {
                heaps[i] = new PairingHeap<Edge>((x, y) => x.Weight > y.Weight);
                activeHeaps.Add(i);
                foreach (Edge e in g.GetEdgesFrom(i))
                {
                    heaps[i].Insert(e);
                }
            }

            OrdinaryList<(int, int)> heapsToMerge = new OrdinaryList<(int, int)>();
            EdgesList edges = new EdgesList();

            while (true)
            {
                foreach (int heapId in activeHeaps)
                {
                    var result = heaps[heapId].ExtractMinimum();
                    if (result.HasValue)
                    {
                        edges.Push(result.value);
                    }
                }

                if (edges.IsEmpty() && numberOfTrees > 1)
                {
                    return (false, tree);
                }

                while (!edges.IsEmpty())
                {
                    Edge e = edges.Pop();
                    if (null == tree.GetEdge(e.From, e.To))
                    {
                        int heapOneId = unionFind.FindParent(e.From);
                        int heapTwoId = unionFind.FindParent(e.To);
                        if (heapOneId != heapTwoId)
                        {
                            tree.AddEdge(e);
                            unionFind.Union(e.From, e.To);
                            heapsToMerge.Push((heapOneId, heapTwoId));

                            if (unionFind.FindParent(e.From) == heapOneId)
                            {
                                heaps[heapOneId].Concatenate(heaps[heapTwoId]);
                                heaps[heapTwoId] = null;
                                activeHeaps.Remove(heapTwoId);
                            }
                            else
                            {
                                heaps[heapTwoId].Concatenate(heaps[heapOneId]);
                                heaps[heapOneId] = null;
                                activeHeaps.Remove(heapOneId);
                            }

                            numberOfTrees--;
                        }

                    }
                }

                if (numberOfTrees == 1)
                {
                    return (true, tree);
                }
            }
        }
    }
}
