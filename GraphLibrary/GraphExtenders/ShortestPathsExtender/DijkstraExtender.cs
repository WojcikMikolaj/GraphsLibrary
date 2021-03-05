using GraphLibrary.ExceptionClasses;
using System;
using System.Collections.Generic;
using static GraphLibrary.ContainerClasses.PairingHeap;

namespace GraphLibrary.GraphExtenders.ShortestPathsExtender
{
    public static class DijkstraExtender
    {
        public static (double distance, int previous)[] Dijkstra(this Graph g, int startingVertex)
        {
            if (startingVertex < 0 || startingVertex >= g.EdgesCount)
            {
                throw new IndexOutOfRangeException("startingIndex must be between (inclusive) 0 and VerticesCount-1");
            }

            (double distance, int previous)[] tab = new (double, int)[g.VerticesCount];

            for (int i = 0; i < g.VerticesCount; i++)
            {
                tab[i] = (double.MaxValue, -1);
            }
            tab[startingVertex] = (0, -1);

            PairingHeap<paths_struct> queue = new PairingHeap<paths_struct>((x, y) => x.distance > y.distance);
            List<Node<paths_struct>> lista = new List<Node<paths_struct>>();

            for (int i = 0; i < g.VerticesCount; i++)
            {
                lista.Add(queue.Insert(new paths_struct { ID = i, distance = double.MaxValue }));
            }

            while (!queue.isEmpty())
            {
                var v = queue.ExtractMinimum();
                foreach (var e in g.GetEdgesFrom(v.value.ID))
                {
                    if (e.Weight < 0)
                    {
                        throw new WrongWeightException("weight of each edge must be >=0");
                    }
                    if (tab[e.To].distance > tab[e.From].distance + e.Weight)
                    {
                        tab[e.To] = (tab[e.From].distance + e.Weight, e.From);
                        queue.DecreaseKey(lista[e.To], new paths_struct { ID = lista[e.To].Value.ID, distance = tab[e.From].distance + e.Weight });
                    }
                }
            }
            return tab;
        }
    }
}
