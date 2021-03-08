using GraphLibrary;
using GraphLibrary.GraphExtenders.SpanningTreeExtenders;
using Xunit;

namespace XUnitTestsForGraphLibrary.SpanningTreeExtendersTests
{
    public class KruskalExtenderTests
    {
        [Fact]
        public void SimpleKrusalTest()
        {
            Graph g = new MatrixGraph(7, false);
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
            var result = g.Kruskal();

            Assert.True(result.connected);
            Assert.Equal(0, result.tree.GetEdge(0, 3).Weight);
            Assert.Equal(2, result.tree.GetEdge(3, 2).Weight);
            Assert.Equal(1, result.tree.GetEdge(2, 5).Weight);
            Assert.Equal(2, result.tree.GetEdge(2, 4).Weight);
            Assert.Equal(3, result.tree.GetEdge(1, 4).Weight);
            Assert.Equal(3, result.tree.GetEdge(4, 6).Weight);
            Assert.Equal(6, result.tree.EdgesCount);
        }
    }
}
