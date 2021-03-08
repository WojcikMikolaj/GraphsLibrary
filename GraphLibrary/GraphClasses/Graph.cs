using System.Collections.Generic;

namespace GraphLibrary
{
    public abstract class Graph
    {
        protected (int In, int Out)[] degrees;

        public int VerticesCount { get => verticesCount; }
        protected readonly int verticesCount;

        public int EdgesCount { get; private set; }

        public bool Directed { get => directed; }
        protected readonly bool directed;


        protected Graph(int verticesCount, bool directed)
        {
            this.verticesCount = verticesCount;
            this.directed = directed;
            degrees = new (int In, int Out)[verticesCount];
        }

        public virtual bool AddEdge(Edge e)
        {
            return IncreaseDegreeAndEdgesCount(e.From, e.To);
        }
        public virtual bool AddEdge(int from, int to, double weight = 0)
        {
            return IncreaseDegreeAndEdgesCount(from, to);
        }

        private bool IncreaseDegreeAndEdgesCount(int from, int to)
        {
            EdgesCount++;
            degrees[from].Out++;
            degrees[to].In++;
            if (directed == false)
            {
                degrees[from].In++;
                degrees[to].Out++;
            }
            return true;
        }

        public virtual bool DeleteEdge(Edge e, bool checkWeight)
        {
            return DecreaseDegreeAndEdgesCount(e.From, e.To);
        }
        public virtual bool DeleteEdge(int from, int to)
        {
            return DecreaseDegreeAndEdgesCount(from, to);
        }

        private bool DecreaseDegreeAndEdgesCount(int from, int to)
        {
            EdgesCount--;
            degrees[from].Out--;
            degrees[to].In--;
            if (directed == false)
            {
                degrees[from].In--;
                degrees[to].Out--;
            }
            return true;
        }

        public abstract Edge GetEdge(int from, int to);
        public abstract IEnumerable<Edge> GetEdgesFrom(int vertexID);
        public abstract IEnumerable<Edge> GetEdgesTo(int vertexID);


        public int GetInDegree(int vertexID)
        {
            return degrees[vertexID].In;
        }

        public int GetOutDegree(int vertexID)
        {
            return degrees[vertexID].Out;
        }

        public abstract Graph IsolatedGraph(bool directed);

        public abstract Graph ReversedGraph();

        public abstract Graph NewGraphOfSameType(int verticesCount, bool directed = false);
    }
}
