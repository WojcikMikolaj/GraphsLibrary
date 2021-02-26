using GraphLibrary.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.GraphExtenders.SearchExtenders
{
    public static class DFSExtender
    {
        public static bool RecursiveDFS(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
         int startingIndex, out bool[] visited)
        {
            if (startingIndex < 0 || startingIndex >= g.VerticesCount)
            {
                throw new ArgumentOutOfRangeException("startingIndex");
            }

            bool[] v = new bool[g.VerticesCount];
            bool? result = g.DFS(preVisit, postVisit, visitEdge, startingIndex, v);
            visited = v;

            if (result.HasValue == false)
            {
                return false;
            }
            return result.Value;
        }

        private static bool? DFS(this Graph g, VisitVertexFunction preVisit, VisitVertexFunction postVisit, VisitEdgeFunction visitEdge,
          int startingIndex, bool[] visited)
        {
            visited[startingIndex] = true;
            if (preVisit != null)
            {
                bool? tmp = preVisit(startingIndex);
                if (tmp.HasValue == false)
                {
                    return null;
                }
                if (tmp.Value == false)
                {
                    return false;
                }
            }

            foreach (var e in g.GetEdgesFrom(startingIndex))
            {
                if (!visited[e.To])
                {
                    if (visitEdge != null)
                    {
                        bool? tmp = visitEdge(e);
                        if (tmp.HasValue == false)
                        {
                            continue;
                        }
                        if (tmp.Value == false)
                        {
                            return false;
                        }
                    }
                    bool? ret = g.DFS(preVisit, postVisit, visitEdge, e.To, visited);
                    if (ret.HasValue)
                    {
                        if (ret.Value == false)
                        {
                            return false;
                        }
                    }
                }
            }

            if (postVisit != null)
            {
                bool? tmp = postVisit(startingIndex);
                if (tmp.HasValue == false)
                {
                    return null;
                }
                if (tmp.Value == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
