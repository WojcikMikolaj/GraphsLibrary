using System;
using Xunit;
using GraphLibrary;
using GraphLibrary.DataStructuresClasses;
using System.Collections.Generic;

namespace XUnitTestsForGraphLibrary.ContainerClassesTests
{
    public class PairingHeapTests
    {
        [Fact]
        public void OneValueTest()
        {
            PairingHeap<int> pairingHeap = new PairingHeap<int>((x, y) => x > y);
            pairingHeap.Insert(10);
            var value = pairingHeap.ExtractMinimum();
            Assert.Equal(10, value.value);
        }

        [Fact]
        public void TwoValueTest()
        {
            PairingHeap<int> pairingHeap = new PairingHeap<int>((x, y) => x > y);
            pairingHeap.Insert(10);
            pairingHeap.Insert(-2);
            var value = pairingHeap.ExtractMinimum();
            Assert.Equal(-2, value.value);
            value = pairingHeap.ExtractMinimum();
            Assert.Equal(10, value.value);
        }

        [Theory]
        [InlineData(1000)]
        public void NValueTest(int n)
        {
            PairingHeap<int> pairingHeap = new PairingHeap<int>((x, y) => x > y);
            List<int> lista = new List<int>();

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                int value = random.Next() % n * (0 == random.Next() % 2 ? -1 : 1);
                lista.Add(value);
                pairingHeap.Insert(value);
            }

            lista.Sort();

            for (int i = 0; i < n; i++)
            {
                var value = pairingHeap.ExtractMinimum();
                Assert.Equal(lista[i], value.value);
            }
        }
    }
}
