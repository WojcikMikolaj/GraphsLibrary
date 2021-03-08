using GraphLibrary.DataStructuresClasses;
using Xunit;

namespace XUnitTestsForGraphLibrary
{
    public class OrdinaryListTests
    {
        [Theory]
        [InlineData(200)]
        public void OrdinaryListSimpleTest(int n)
        {
            OrdinaryList<int> list = new OrdinaryList<int>();
            for (int i = 0; i < n; i++)
            {
                list.Push(i);
            }
            for (int i = 0; i < n; i++)
            {
                Assert.False(list.IsEmpty());

                int num = list.Peek();
                Assert.Equal(i, num);

                num = list.Pop();
                Assert.Equal(i, num);
            }

            Assert.True(list.IsEmpty());

            int smth = list.Peek();
            Assert.Equal(default(int), smth);

            smth = list.Pop();
            Assert.Equal(default(int), smth);

            Assert.True(list.IsEmpty());
        }
    }
}
