using System;
using Xunit;
using GraphLibrary;
using GraphLibrary.GraphExtenders.ShortestPathsExtenders;

namespace XUnitTestsForGraphLibrary.ShorthestPathsExtenderTests
{
    public class DAGExtenderTests
    {
        [Theory]
        [InlineData(true)]
        public void SimpleDijkstraTest(bool directed)
        {
            Graph g = new MatrixGraph(7, directed);
            g.AddEdge(0, 1, 5);
            g.AddEdge(0, 2, 0);
            g.AddEdge(0, 3, 3);            
            g.AddEdge(1, 5, 3);
            g.AddEdge(1, 6, 4);
            g.AddEdge(2, 3, 2);
            g.AddEdge(2, 6, 15);
            g.AddEdge(3, 5, 2);
            g.AddEdge(3, 4, 1);            
            g.AddEdge(4, 5, 12);
            g.AddEdge(5, 6, 3);
            var result = g.DAG(0);
            Assert.Equal(0, result[0].distance);
            Assert.Equal(5, result[1].distance);
            Assert.Equal(0, result[2].distance);
            Assert.Equal(2, result[3].distance);
            Assert.Equal(3, result[4].distance);
            Assert.Equal(4, result[5].distance);
            Assert.Equal(7, result[6].distance);
        }
    }
}
