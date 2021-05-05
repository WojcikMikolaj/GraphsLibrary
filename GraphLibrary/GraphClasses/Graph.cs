using GraphLibrary.DataStructuresClasses;
using System.Collections.Generic;

namespace GraphLibrary
{
    public abstract class Graph
    {
        protected (int In, int Out)[] degrees;

        public int VerticesCount { get => verticesCount; }
        protected readonly int verticesCount;

        public int EdgesCount { get; protected set; }

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

        public abstract Graph Clone();

        public abstract Graph IsolatedGraph(bool directed);

        public abstract Graph ReversedGraph();

        public abstract Graph NewGraphOfSameType(int verticesCount, bool directed = false);

        // This method assumes that graphs are labbeled <=> isomorphism of graphs is not enough 
        public bool Equals(Graph other)
        {
            if (this.Directed != other.Directed)
            {
                return false;
            }

            if (this.VerticesCount != other.VerticesCount)
            {
                return false;
            }

            if (this.EdgesCount != other.EdgesCount)
            {
                return false;
            }

            return this.CompareVerticesAndEdges(other);
        }

        private bool CompareVerticesAndEdges(Graph other)
        {
            EdgesList firstGraphEdges = new EdgesList();
            EdgesList secondGraphEdges = new EdgesList();

            for (int i = 0; i < this.VerticesCount; i++)
            {
                if (this.GetInDegree(i) != other.GetInDegree(i))
                {
                    return false;
                }

                if (this.GetOutDegree(i) != other.GetOutDegree(i))
                {
                    return false;
                }

                foreach (Edge e in this.GetEdgesFrom(i))
                {
                    firstGraphEdges.Push(e);
                }

                foreach (Edge e in other.GetEdgesFrom(i))
                {
                    secondGraphEdges.Push(e);
                }
            }

            while (!firstGraphEdges.IsEmpty())
            {
                Edge fromFirst = firstGraphEdges.Pop();
                Edge fromSecond = secondGraphEdges.Pop();

                if (!fromFirst.Equals(fromSecond))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
