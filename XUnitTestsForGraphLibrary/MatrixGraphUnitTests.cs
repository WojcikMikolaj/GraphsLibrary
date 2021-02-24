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
    }
}
