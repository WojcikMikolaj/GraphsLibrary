using GraphLibrary;
using GraphLibrary.GraphExtenders.ShortestPathsExtenders;
using Xunit;

namespace XUnitTestsForGraphLibrary.ShorthestPathsExtendersTests
{
    public class DijkstraExtenderTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SimpleDijkstraTest(bool directed)
        {
            Graph g = new MatrixGraph(7, directed);
            g.AddEdge(0, 1, 5);
            g.AddEdge(0, 2, 3);
            g.AddEdge(0, 3, 0);
            g.AddEdge(1, 4, 3);
            g.AddEdge(1, 6, 4);
            g.AddEdge(2, 4, 2);
            g.AddEdge(2, 5, 1);
            g.AddEdge(3, 2, 2);
            g.AddEdge(3, 6, 15);
            g.AddEdge(4, 6, 3);
            g.AddEdge(5, 4, 12);
            var result = g.Dijkstra(0);
            Assert.Equal(0, result[0].distance);
            Assert.Equal(5, result[1].distance);
            Assert.Equal(2, result[2].distance);
            Assert.Equal(0, result[3].distance);
            Assert.Equal(4, result[4].distance);
            Assert.Equal(3, result[5].distance);
            Assert.Equal(7, result[6].distance);
        }
    }
}
