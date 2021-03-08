using GraphLibrary;
using Xunit;

namespace XUnitTestsForGraphLibrary
{
    public class EdgeEqualityTests
    {
        [Fact]
        public void SimpleEqualTest()
        {
            Edge first = new Edge(5, 6, 7);
            Edge second = new Edge(5, 6, 7);

            bool result = first.Equals(second);
        }

        [Fact]
        public void CopyEqualTest()
        {
            Edge first = new Edge(5, 6, 7);
            Edge second = new Edge(first);

            bool result = first.Equals(second);
        }
    }
}
