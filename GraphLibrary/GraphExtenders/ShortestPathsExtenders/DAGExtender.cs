using GraphLibrary.ExceptionClasses;
using System;

namespace GraphLibrary.GraphExtenders.ShortestPathsExtenders
{
    public static class DAGExtender
    {
        /// <summary>
        /// DAG - shorthest paths in topologically sorted acyclic graphs.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="startingVertex"></param>
        /// <returns></returns>
        public static (double distance, int previous)[] DAG(this Graph g, int startingVertex)
        {
            if (!g.Directed)
            {
                throw new WrongGraphException("Graph must be directed");
            }

            if (startingVertex < 0 || startingVertex >= g.VerticesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }

            (double distance, int previous)[] tab = new (double, int)[g.VerticesCount];
            for (int i = 0; i < g.VerticesCount; i++)
            {
                foreach (var e in g.GetEdgesFrom(i))
                {
                    if (e.To < i)
                    {
                        throw new WrongGraphException("Graph must be topologically sorted, and acyclic");
                    }
                }
                tab[i] = (double.MaxValue, -1);
            }

            tab[startingVertex] = (0, -1);

            for (int i = startingVertex; i < g.VerticesCount; i++)
            {
                foreach (var e in g.GetEdgesFrom(i))
                {
                    if (tab[e.To].distance > tab[i].distance + e.Weight)
                    {
                        tab[e.To] = (tab[i].distance + e.Weight, i);
                    }
                }
            }
            return tab;
        }
    }
}
