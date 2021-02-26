using System;
using Xunit;
using GraphLibrary;
using GraphLibrary.GraphExtenders.SearchExtenders;

namespace XUnitTestsForGraphLibrary
{
    public class DFSExtenderTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SimpleDFSTest(bool value)
        {
            Random random = new Random();
            Graph g = new MatrixGraph(9, value);
            g.AddEdge(0, 1, random.NextDouble());
            g.AddEdge(0, 2, random.NextDouble());
            g.AddEdge(0, 3, random.NextDouble());
            g.AddEdge(1, 4, random.NextDouble());
            g.AddEdge(1, 6, random.NextDouble());
            g.AddEdge(2, 4, random.NextDouble());
            g.AddEdge(2, 5, random.NextDouble());
            g.AddEdge(3, 7, random.NextDouble());
            g.AddEdge(4, 7, random.NextDouble());
            g.AddEdge(6, 7, random.NextDouble());
            g.AddEdge(5, 8, random.NextDouble());

            bool[] tab;
            g.RecursiveDFS(null, null, null, 0, out tab);
            foreach (bool b in tab)
            {
                Assert.True(b);
            }

            bool[] invTab;
            Graph invGraph = g.ReversedGraph();
            invGraph.RecursiveDFS(null, null, null, 0, out invTab);
            Assert.True(invTab[0]);
            for(int i=1; i<invTab.Length; i++)
            {
                Assert.Equal(!value, invTab[i]);
            }
        }
        
    }
}
