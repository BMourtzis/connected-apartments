using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace CommApsDomainUnitTest.WebAPI_Tests.FacilityController_Tests
{
    public class FetchFacilityTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchFacilityTests()
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
        public void FetchFacility_RegularValues_OkResults()
        {
            //Arrange
            var bm = new Mock<IFacility>();
            bm.SetupGet(a => a.Id).Returns(2);

            facade.Setup(f => f.FetchFacility("3445", 2)).Returns(bm.Object);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacility(2);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IFacility>>(result);
            Assert.NotNull(okresult.Content);
            Assert.Equal(2, okresult.Content.Id);
        }

        [Fact]
        public void FetchFacility_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Facility");

            facade.Setup(f => f.FetchFacility("3445", 2)).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacility(2);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Facility you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchFacility_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchFacility("3445", 2)).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacility(2);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
