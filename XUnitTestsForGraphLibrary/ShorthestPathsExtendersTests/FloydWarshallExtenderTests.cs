using GraphLibrary;
using GraphLibrary.GraphExtenders.ShortestPathsExtenders;
using Xunit;

namespace XUnitTestsForGraphLibrary.ShorthestPathsExtendersTests
{
    public class FloydWarshallExtenderTests
    {
        [Theory]
        [InlineData(true)]
        public void FloydWarshallTest(bool directed)
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
            var result = g.FloydWarshall();

            for (int i = 0; i < g.VerticesCount; i++)
            {
                Assert.Equal(0, result[i][i]);
            }

            Assert.Equal(5, result[0][1]);
            Assert.Equal(2, result[0][2]);
            Assert.Equal(0, result[0][3]);
            Assert.Equal(4, result[0][4]);
            Assert.Equal(3, result[0][5]);
            Assert.Equal(7, result[0][6]);

            Assert.Equal(double.MaxValue, result[2][0]);
            Assert.Equal(double.MaxValue, result[2][1]);
            Assert.Equal(double.MaxValue, result[2][3]);
            Assert.Equal(2, result[2][4]);
            Assert.Equal(1, result[2][5]);
            Assert.Equal(5, result[2][6]);

            Assert.Equal(double.MaxValue, result[2][0]);
            Assert.Equal(double.MaxValue, result[2][1]);
            Assert.Equal(double.MaxValue, result[2][3]);
            Assert.Equal(2, result[2][4]);
            Assert.Equal(1, result[2][5]);
            Assert.Equal(5, result[2][6]);

            Assert.Equal(double.MaxValue, result[3][0]);
            Assert.Equal(double.MaxValue, result[3][1]);
            Assert.Equal(2, result[3][2]);
            Assert.Equal(4, result[3][4]);
            Assert.Equal(3, result[3][5]);
            Assert.Equal(7, result[3][6]);

            Assert.Equal(double.MaxValue, result[4][0]);
            Assert.Equal(double.MaxValue, result[4][1]);
            Assert.Equal(double.MaxValue, result[4][2]);
            Assert.Equal(double.MaxValue, result[4][3]);
            Assert.Equal(double.MaxValue, result[4][5]);
            Assert.Equal(3, result[4][6]);

            Assert.Equal(double.MaxValue, result[5][0]);
            Assert.Equal(double.MaxValue, result[5][1]);
            Assert.Equal(double.MaxValue, result[5][2]);
            Assert.Equal(double.MaxValue, result[5][3]);
            Assert.Equal(12, result[5][4]);
            Assert.Equal(15, result[5][6]);

            Assert.Equal(double.MaxValue, result[6][0]);
            Assert.Equal(double.MaxValue, result[6][1]);
            Assert.Equal(double.MaxValue, result[6][2]);
            Assert.Equal(double.MaxValue, result[6][3]);
            Assert.Equal(double.MaxValue, result[6][4]);
            Assert.Equal(double.MaxValue, result[6][5]);
        }
    }
}
