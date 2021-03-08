using GraphLibrary.Interfaces;
using GraphLibrary.UtilityClasses;
using System;

namespace GraphLibrary.GraphExtenders.SearchExtenders
{
    public static class GeneralSearchExtender
    {
        public static bool GeneralSearch<T>(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
         int startingIndex, out bool[] visited)
           where T : IEdgeCollection, new()
        {
            if (startingIndex < 0 || startingIndex >= g.VerticesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }

            bool[] visitedTab = new bool[g.VerticesCount];
            int[] post = new int[g.VerticesCount];

            visited = visitedTab;

            T collection = new T();

            foreach (var e in g.GetEdgesFrom(startingIndex))
            {
                collection.Push(e);
            }

            post[startingIndex] = g.GetOutDegree(startingIndex);

            visitedTab[startingIndex] = true;

            if (null != preVisit)
            {
                if (!preVisit(startingIndex))
                {
                    return false;
                }
            }
            post[startingIndex] = g.GetOutDegree(startingIndex);

            while (!collection.IsEmpty())
            {
                Edge e = collection.Pop();

                post[e.From]--;

                if (null != visitEdge)
                {
                    if (!visitEdge(e))
                    {
                        return false;
                    }
                }



                if (!visitedTab[e.To])
                {
                    visitedTab[e.To] = true;
                    if (null != preVisit)
                    {
                        if (!preVisit(startingIndex))
                        {
                            return false;
                        }
                    }

                    foreach (var f in g.GetEdgesFrom(e.To))
                    {
                        collection.Push(f);
                    }
                    post[e.To] = g.GetOutDegree(e.To);
                }

                if (0 == post[e.From])
                {
                    if (null != postVisit)
                    {
                        if (!postVisit(e.From))
                        {
                            return false;
                        }
                    }
                }



            }
            return true;
        }
    }
}
