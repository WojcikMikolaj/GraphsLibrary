using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.GraphExtenders.ShortestPathsExtenders
{
    public static class FloydWarshallExtender
    {
        //Graph must not contain cycle of negative sum of edges weight
        public static double[][] FloydWarshall(this Graph g)
        {
            double[][] tab = new double[g.VerticesCount][];
            for (int i = 0; i < g.VerticesCount; i++)
            {
                tab[i] = new double[g.VerticesCount];
                for (int j = 0; j < g.VerticesCount; j++)
                {
                    tab[i][j] = double.MaxValue;
                }
            }

            for (int i = 0; i < g.VerticesCount; i++)
            {
                tab[i][i] = 0;
                foreach(Edge e in g.GetEdgesFrom(i))
                {
                    tab[e.From][e.To] = e.Weight;
                }
            }

            for (int k = 0; k < g.VerticesCount; k++)
            {
                for (int i = 0; i < g.VerticesCount; i++)
                {
                    for (int j = 0; j < g.VerticesCount; j++)
                    {
                        if (tab[i][j] > tab[i][k] + tab[k][j])
                        {
                            tab[i][j] = tab[i][k] + tab[k][j];
                        }
                    }
                }
            }
            return tab;
        }
    }
}
