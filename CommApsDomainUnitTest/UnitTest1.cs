using NUnit.Framework;
using System;

namespace CommApsDomainUnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase]
        public void TestMethod1()
        {
            int i = 3;
            i = i * 3;
            Assert.AreEqual(0, i);
        }
    }
}
