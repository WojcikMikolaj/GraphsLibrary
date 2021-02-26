using GraphLibrary.UtilityClasses;
using System;
using System.Collections.Generic;

namespace GraphLibrary.GraphExtenders.SearchExtenders
{
    public static class BFSExtender
    {
        public static bool BFS(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
         int startingIndex, out bool[] visited)
        {
            if (startingIndex < 0 || startingIndex >= g.VerticesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }
            bool[] visitedTab = new bool[g.VerticesCount];
            visited = visitedTab;
            List<Edge> edges = new List<Edge>();
            Edge e = null;
            do
            {
                e = null;
                if (!visitedTab[startingIndex])
                {
                    visitedTab[startingIndex] = true;
                    if (null != preVisit)
                    {
                        if (!preVisit(startingIndex))
                        {
                            return false;
                        }
                    }
                    edges.AddRange(g.GetEdgesFrom(startingIndex));
                    if (null != postVisit)
                    {
                        if (!postVisit(startingIndex))
                        {
                            return false;
                        }
                    }
                }
                if (edges.Count > 0)
                {
                    e = edges[0];
                    edges.RemoveAt(0);
                    if (null != visitEdge)
                    {
                        if (!visitEdge(e))
                        {
                            return false;
                        }
                    }
                    startingIndex = e.To;
                }
            }
            while (null != e);

            return true;
        }
    }
}
