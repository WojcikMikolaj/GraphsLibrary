using System;
using Xunit;
using GraphLibrary;

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
            graph.DeleteEdge(0, 9);
            Assert.Equal(0, graph.GetOutDegree(0));
            Assert.Equal(1, graph.GetInDegree(0));
        }

        [Fact]
        public void UndirectedGraphAddEdgeAndDeleteEdge()
        {
            Graph graph = new MatrixGraph(10, false);
            graph.AddEdge(0, 9, 10);
            graph.AddEdge(2, 0, 5);
            Assert.Equal(2, graph.GetOutDegree(0));
            Assert.Equal(2, graph.GetInDegree(0));
            graph.DeleteEdge(0, 9);
            Assert.Equal(1, graph.GetOutDegree(0));
            Assert.Equal(1, graph.GetInDegree(0));
        }
    }
}
