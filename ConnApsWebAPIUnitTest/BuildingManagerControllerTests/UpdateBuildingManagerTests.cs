using System;
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

namespace ConnApsWebAPIUnitTest.BuildingManagerControllerTests
{
    public class UpdateBuildingManagerTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private BuildingManagerUpdateModel model;

        public UpdateBuildingManagerTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new BuildingManagerUpdateModel() {UserId = "3445", DateOfBirth = new DateTime(1995, 03, 16), FirstName = "Bill", LastName = "Mourtzis", Phone = "0123456789"};

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void FetchBuildingManagers_RegularValues_OkResults()
        {
            //Arrange
            var bm = new Mock<IBuildingManager>();
            bm.SetupGet(a => a.Id).Returns(2);

            facade.Setup(f => f.UpdateBuildingManager(model.UserId, model.FirstName, model.LastName, model.DateOfBirth, model.Phone)).Returns(bm.Object);

            var controller = new BuildingManagerController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateBuildingManager(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.NotNull(okresult.Content);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void FetchBuildingManagers_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Building Manager");

            facade.Setup(f => f.UpdateBuildingManager(model.UserId, model.FirstName, model.LastName, model.DateOfBirth, model.Phone)).Throws(exception);

            var controller = new BuildingManagerController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateBuildingManager(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Building Manager you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchBuildingManagers_BadModel_BadRequest()
        {
            //Arrange
            var controller = new BuildingManagerController(facade.Object);
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.UpdateBuildingManager(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact]
        public void FetchBuildingManagers_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.UpdateBuildingManager(model.UserId, model.FirstName, model.LastName, model.DateOfBirth, model.Phone)).Throws(exception);

            var controller = new BuildingManagerController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateBuildingManager(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
