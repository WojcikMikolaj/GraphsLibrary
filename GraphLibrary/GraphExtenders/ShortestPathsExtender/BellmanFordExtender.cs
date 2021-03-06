using GraphLibrary.ExceptionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.GraphExtenders.ShortestPathsExtender
{
    public static class BellmanFordExtender
    {
        public static (double distance, int previous)[] BellmanFord(this Graph g, int startingVertex)
        {
            if (startingVertex < 0 || startingVertex >= g.VerticesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }

            (double distance, int previous)[] tab = new (double, int)[g.VerticesCount];

            for (int i = 0; i < g.VerticesCount; i++)
            {
                tab[i] = (double.MaxValue, -1);
            }
            tab[startingVertex] = (0, -1);

            int iterationNumber = 0;
            bool change = true;

            while (change)
            {
                iterationNumber++;
                change = false;
                for (int i = 0; i < g.VerticesCount; i++)
                {
                    foreach (var e in g.GetEdgesFrom(i))
                    {
                        if (tab[e.To].distance > tab[i].distance + e.Weight)
                        {
                            if (iterationNumber > g.VerticesCount)
                                throw new WrongGraphException("Graph contains cycle of negative cost");
                            change = true;
                            tab[e.To] = (tab[i].distance + e.Weight, i);
                        }
                    }
                }
            }
            return tab;
        }
    }
}
