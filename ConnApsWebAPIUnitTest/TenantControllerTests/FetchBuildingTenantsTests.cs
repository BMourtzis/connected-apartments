using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace ConnApsWebAPIUnitTest.TenantControllerTests
{
    public class FetchBuildingTenantsTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchBuildingTenantsTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void FetchBuildingTenants_RegularValues_OkResults()
        {
            //Arrange
            var tenant = new Mock<ITenant>();
            tenant.SetupGet(a => a.Id).Returns(2);

            var newTenant = new Mock<ITenant>();
            newTenant.SetupGet(a => a.Id).Returns(3);

            var tenantList = new List<ITenant>() {tenant.Object, newTenant.Object};

            facade.Setup(f => f.FetchTenants("3445")).Returns(tenantList);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingTenants();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<ITenant>>>(result);
            Assert.NotNull(okresult.Content);
            Assert.Equal(2, okresult.Content.Count());

            var enumerator = okresult.Content.GetEnumerator();
            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            Assert.Equal(2, enumerator.Current.Id);

            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            Assert.Equal(3, enumerator.Current.Id);

            enumerator.Dispose();
        }

        [Fact]
        public void FetchBuildingTenants_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Tenant");

            facade.Setup(f => f.FetchTenants("3445")).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingTenants();

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Tenant you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchBuildingTenants_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchTenants("3445")).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingTenants();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
