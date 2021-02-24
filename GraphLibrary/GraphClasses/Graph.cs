using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    public abstract class Graph
    {               
        protected (int In, int Out)[] degrees;

        public int VerticesCount { get => verticesCount; }
        protected readonly int verticesCount;

        public int EdgesCount { get; private set; }

        public bool Directed { get; }
        protected readonly bool directed;


        protected Graph(int verticesCount, bool directed)
        {
            this.verticesCount = verticesCount;
            this.directed = directed;
            degrees = new (int In, int Out)[verticesCount];
        }

        public abstract bool AddEdge(Edge e);
        public abstract bool AddEdge(int from, int to, double weight = 0);


        public abstract bool DeleteEdge(Edge e, bool checkWeight);
        public abstract bool DeleteEdge(int from, int to);


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
