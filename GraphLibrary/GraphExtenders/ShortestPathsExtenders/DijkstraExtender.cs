using GraphLibrary.ExceptionClasses;
using System;
using System.Collections.Generic;
using GraphLibrary.ContainerClasses;
using GraphLibrary.UtilityClasses;

namespace GraphLibrary.GraphExtenders.ShortestPathsExtenders
{
    public static class DijkstraExtender
    {
        public static (double distance, int previous)[] Dijkstra(this Graph g, int startingVertex)
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

            PairingHeap<PathsStruct> queue = new PairingHeap<PathsStruct>((x, y) => x.distance > y.distance);
            List<Node<PathsStruct>> lista = new List<Node<PathsStruct>>();

            for (int i = 0; i < g.VerticesCount; i++)
            {
                lista.Add((Node<PathsStruct>)queue.Insert(new PathsStruct { ID = i, distance = double.MaxValue }));
            }

            
            while (!queue.IsEmpty())
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
                        queue.DecreaseKey(lista[e.To], new PathsStruct { ID = lista[e.To].Value.ID, distance = tab[e.From].distance + e.Weight });                         
                    }
                }
            }
            return tab;
        }
    }
}
