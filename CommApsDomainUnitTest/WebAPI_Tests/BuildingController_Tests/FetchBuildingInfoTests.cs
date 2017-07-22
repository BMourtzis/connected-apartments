using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using ConnApsWebAPI.Models;
using Moq;
using Xunit;

namespace CommApsDomainUnitTest.WebAPI_Tests.BuildingController_Tests
{
    public class FetchBuildingInfoTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchBuildingInfoTests()
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
        public void FetchBuildingInfo_RegularValues_OkResult()
        {
            //Arrange
            var building = new Mock<IBuilding>();
            building.SetupGet(b => b.Id).Returns(1);

            facade.Setup(f => f.FetchBuilding("3445")).Returns(building.Object);

            var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingInfo();

            //Assert
            Assert.NotNull(result);
            var okresult = Assert.IsType<OkNegotiatedContentResult<IBuilding>>(result);
            Assert.Equal(1, okresult.Content.Id);
        }

        [Fact]
        public void FetchBuildingInfo_ThrowsError_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Building");

            facade.Setup(f => f.FetchBuilding("3445")).Throws(exception);

            var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingInfo();

            //Assert
            Assert.NotNull(result);
            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Building you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchBuildingInfo_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchBuilding("3445")).Throws(excpetion);

            var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingInfo();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
