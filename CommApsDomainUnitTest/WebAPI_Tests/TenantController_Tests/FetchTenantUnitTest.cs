using System;
using Xunit;
using Moq;
using ConnApsDomain;
using ConnApsWebAPI.Controllers;

namespace CommApsDomainUnitTest.WebAPI_Tests.TenantController_Tests
{
    public class FetchTenantUnitTest
    {

        [Fact]
        public void TestMethod1()
        {
            //Assemble
            var tenantFacade = new Mock<TenantFacade>();
            var tenant = new Mock<ITenant>();
            //tenantFacade.Setup(t => t.FetchTenant("")).Returns(tenant.Object);
            //TenantController controller = new TenantController(tenantFacade.Object);

            //Action


            //Result
            Assert.Equal(1, 1);

        }
    }
}
