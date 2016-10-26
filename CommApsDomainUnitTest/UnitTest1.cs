using ConnApsDomain;
using System;
using Xunit;

namespace CommApsDomainUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            int i = 3;
            i = i * 3;
            Assert.Equal(7, i);
        }
    }
}
