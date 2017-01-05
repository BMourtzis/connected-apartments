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
    public class FetchTenantNoParametersTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchTenantNoParametersTests()
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
        public void FetchTenant_RegularValues_OkResults()
        {
            //Arrange
            var tenant = new Mock<ITenant>();
            tenant.SetupGet(a => a.Id).Returns(2);

            facade.Setup(f => f.FetchTenant("3445")).Returns(tenant.Object);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchTenant();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<ITenant>>(result);
            Assert.NotNull(okresult.Content);
            Assert.Equal(2, okresult.Content.Id);
        }

        [Fact]
        public void FetchTenant_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Tenant");

            facade.Setup(f => f.FetchTenant("3445")).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchTenant();

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Tenant you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchTenant_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchTenant("3445")).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchTenant();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        } 
    }
}
