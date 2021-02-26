using GraphLibrary.UtilityClasses;
using System;

namespace GraphLibrary.GraphExtenders.SearchExtenders
{
    public static class DFSExtender
    {
        public static bool DFS(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
          int startingIndex, out bool[] visited)
        {
            if (startingIndex < 0 || startingIndex >= g.VerticesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }
            bool[] visitedTab = new bool[g.VerticesCount];
            visited = visitedTab;
            return RecursiveDFS(g, preVisit, postVisit, visitEdge, startingIndex, visitedTab);
        }

        private static bool RecursiveDFS(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
         int startingIndex, bool[] visited)
        {
            if (visited[startingIndex])
            {
                return true;
            }
            visited[startingIndex] = true;
            if (null != preVisit)
            {
                bool preVisitResult = preVisit(startingIndex);
                if (false == preVisitResult)
                {
                    return false;
                }
            }
            foreach (Edge e in g.GetEdgesFrom(startingIndex))
            {
                if (null != visitEdge)
                {
                    bool visitEdgeResult = visitEdge(e);
                    if (false == visitEdgeResult)
                    {
                        continue;
                    }                    
                }
                if (false == g.RecursiveDFS(preVisit, postVisit, visitEdge, e.To, visited))
                {
                    return false;
                }
            }
            if (null != postVisit)
            {
                bool postVisitResult = postVisit(startingIndex);
                if (false == postVisitResult)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
