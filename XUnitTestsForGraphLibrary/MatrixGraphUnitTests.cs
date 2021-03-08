using GraphLibrary;
using Xunit;

namespace XUnitTestsForGraphLibrary
{
    public class MatrixGraphUnitTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(110)]
        [InlineData(10100)]
        public void CreateSimpleUndirectedGraph(int value)
        {
            Graph graph = new MatrixGraph(value);
            Assert.Equal(value, graph.VerticesCount);
            Assert.False(graph.Directed);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(177)]
        [InlineData(1156)]
        [InlineData(13)]
        public void CreateSimpleDirectedGraph(int value)
        {
            Graph graph = new MatrixGraph(value, true);
            Assert.Equal(value, graph.VerticesCount);
            Assert.True(graph.Directed);
        }

        [Fact]
        public void DirectedGraphAddEdgeAndDeleteEdge()
        {
            Graph graph = new MatrixGraph(10, true);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Assert.Equal(1, graph.GetOutDegree(0));
            Assert.Equal(1, graph.GetInDegree(0));
            Assert.Equal(2, graph.EdgesCount);
            graph.DeleteEdge(0, 9);
            Assert.Equal(0, graph.GetOutDegree(0));
            Assert.Equal(1, graph.GetInDegree(0));
            Assert.Equal(1, graph.EdgesCount);
        }

        [Fact]
        public void UndirectedGraphAddEdgeAndDeleteEdge()
        {
            Graph graph = new MatrixGraph(10, false);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Assert.Equal(2, graph.GetOutDegree(0));
            Assert.Equal(2, graph.GetInDegree(0));
            Assert.Equal(2, graph.EdgesCount);
            graph.DeleteEdge(0, 9);
            Assert.Equal(1, graph.GetOutDegree(0));
            Assert.Equal(1, graph.GetInDegree(0));
            Assert.Equal(1, graph.EdgesCount);
        }

        [Fact]
        public void ReverseGraph()
        {
            Graph graph = new MatrixGraph(10, true);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Graph reversedGraph = graph.ReversedGraph();
            Assert.Equal(1, reversedGraph.GetOutDegree(9));
            Assert.Equal(1, reversedGraph.GetInDegree(2));
            Assert.Equal(2, reversedGraph.EdgesCount);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsolatedGraph(bool value)
        {
            Graph graph = new MatrixGraph(10, true);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Graph isolatedGraph = graph.IsolatedGraph(value);
            Assert.Equal(10, isolatedGraph.VerticesCount);
            Assert.Equal(value, isolatedGraph.Directed);
            Assert.Equal(0, isolatedGraph.EdgesCount);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GraphOfSameType(bool value)
        {
            Graph graph = new MatrixGraph(10, true);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Graph sameTypeGraph = graph.NewGraphOfSameType(5, value);
            Assert.Equal(5, sameTypeGraph.VerticesCount);
            Assert.Equal(value, sameTypeGraph.Directed);
            Assert.Equal(0, sameTypeGraph.EdgesCount);
            Assert.IsType<MatrixGraph>(sameTypeGraph);
        }
    }
}
