using ConnApsWebAPI;
using Moq;
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
            Assert.Equal(9, i);
        }
    }
}
