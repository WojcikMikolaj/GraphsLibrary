namespace GraphLibrary.GraphExtenders.ShortestPathsExtenders
{
    public static class JohnsonExtender
    {
        public static double[][] Johnson(this Graph g)
        {

            Graph pomGraph = g.NewGraphOfSameType(g.VerticesCount + 1, g.Directed);
            for (int i = 0; i < g.VerticesCount; i++)
            {
                pomGraph.AddEdge(g.VerticesCount, i);
            }

            double[] h = new double[g.VerticesCount];
            (double, int)[] pom = pomGraph.BellmanFord(g.VerticesCount);
            for (int i = 0; i < g.VerticesCount; i++)
            {
                h[i] = pom[i].Item1;
            }

            Graph pomGraph2 = g.IsolatedGraph(g.Directed);
            for (int i = 0; i < g.VerticesCount; i++)
            {
                foreach (var e in g.GetEdgesFrom(i))
                {
                    pomGraph2.AddEdge(e.From, e.To, e.Weight + h[e.From] - h[e.To]);
                }
            }

            double[][] p = new double[g.VerticesCount][];
            for (int i = 0; i < g.VerticesCount; i++)
            {
                p[i] = new double[g.VerticesCount];
            }

            for (int i = 0; i < g.VerticesCount; i++)
            {
                (double, int)[] pomD = pomGraph2.Dijkstra(i);
                for (int j = 0; j < g.VerticesCount; j++)
                {
                    p[i][j] = pomD[j].Item1;
                }
            }

            for (int i = 0; i < g.VerticesCount; i++)
            {
                for (int j = 0; j < g.VerticesCount; j++)
                {
                    p[i][j] = p[i][j] - h[i] + h[j];
                }
            }
            return p;
        }
    }
}
