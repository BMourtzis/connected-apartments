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
using ConnApsWebAPI;
using ConnApsWebAPI.Controllers.API.V1;
using ConnApsWebAPI.Models;
using Moq;
using Xunit;

namespace ConnApsWebAPIUnitTest.WebAPI.BuildingControllerTests
{
    public class CreateBuildingTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private RegisterBuildingModel model;
        private ApplicationUserManager userManager;

        public CreateBuildingTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);


            model = new RegisterBuildingModel() { Address = "32 Francis st, Sydney, 2000", BuildingName = "Metro Apartments" };

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        //[Fact]
        //public async Task CreateBuilding_RegularValues_OkResult()
        //{
        //    //Arrange
        //    var building = new Mock<IBuilding>();
        //    building.SetupGet(b => b.BuildingName).Returns(model.BuildingName);
        //    building.SetupGet(b => b.Address).Returns(model.Address);
        //    building.SetupGet(b => b.Id).Returns(3);

        //    facade.Setup(f => f.UpdateBuilding("3445", model.BuildingName, model.Address)).Returns(building.Object);

        //    var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = await controller.CreateBuilding(model);

        //    //Assert
        //    Assert.NotNull(result);

        //    var okresult = Assert.IsType<OkNegotiatedContentResult<IBuilding>>(result);
        //    Assert.Equal(1, okresult.Content.Id);
        //}

        //[Fact]
        //public void CreateBuilding_ThrowsError_BadRequest()
        //{
        //    //Arrange
        //    var exception = new ConnApsDomain.Exceptions.NotFoundException("Building");

        //    facade.Setup(f => f.UpdateBuilding("3445", model.BuildingName, model.Address)).Throws(exception);

        //    var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = controller.CreateBuilding(model);

        //    //Assert
        //    Assert.NotNull(result);

        //    var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
        //    Assert.Equal("The Building you requested was not found. Please search again.", badRequest.Message);
        //}

        //[Fact]
        //public void CreateBuilding_ThrowsError_InternalServerError()
        //{
        //    //Arrange
        //    var excpetion = new ConnApsDomain.Exceptions.InternalException();

        //    facade.Setup(f => f.UpdateBuilding("3445", model.BuildingName, model.Address)).Throws(excpetion);

        //    var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = controller.CreateBuilding(model);

        //    //Assert
        //    Assert.NotNull(result);

        //    Assert.IsType<InternalServerErrorResult>(result);
        //}

        //[Fact]
        //public void CreateBuilding_BadModel_BadRequest()
        //{
        //    //Arrange
        //    var controller = new BuildingController(facade.Object) { ControllerContext = controllerContext };
        //    controller.ModelState.AddModelError("Key", "ErrorMessage");

        //    //Act
        //    var result = controller.CreateBuilding(model);

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<InvalidModelStateResult>(result);
        //}
    }
}
