using GraphLibrary;
using Xunit;

namespace XUnitTestsForGraphLibrary
{
    public class GraphEqualityTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SimpleEqualsTest(bool directed)
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

            Graph g2 = new MatrixGraph(7, directed);
            g2.AddEdge(0, 1, 5);
            g2.AddEdge(0, 2, 0);
            g2.AddEdge(0, 3, 3);
            g2.AddEdge(1, 5, 3);
            g2.AddEdge(1, 6, 4);
            g2.AddEdge(2, 3, 2);
            g2.AddEdge(2, 6, 15);
            g2.AddEdge(3, 5, 2);
            g2.AddEdge(3, 4, 1);
            g2.AddEdge(4, 5, 12);
            g2.AddEdge(5, 6, 3);

            bool result = g.Equals(g2);

            Assert.True(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SimpleFailEqualsTest(bool directed)
        {
            Graph g = new MatrixGraph(7, directed);
            Graph g2 = new MatrixGraph(7, !directed);

            bool result = g.Equals(g2);

            Assert.False(result);
        }
    }
}
