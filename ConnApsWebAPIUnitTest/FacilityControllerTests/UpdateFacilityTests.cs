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
using ConnApsWebAPI.Models;
using Moq;
using Xunit;

namespace ConnApsWebAPIUnitTest.FacilityControllerTests
{
    public class UpdateFacilityTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private FacilityUpdateModel model;

        public UpdateFacilityTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new FacilityUpdateModel() { Level = "3", Number = "1", Id = 2};

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void UpdateFacility_RegularValues_OkResults()
        {
            //Arrange
            var facility = new Mock<IFacility>();
            facility.SetupGet(a => a.Id).Returns(4);
            facility.SetupGet(a => a.BuildingId).Returns(1);
            facility.SetupGet(a => a.Level).Returns(model.Level);
            facility.SetupGet(a => a.Number).Returns(model.Number);

            facade.Setup(f => f.UpdateFacility("3445", model.Id,  model.Level, model.Number)).Returns(facility.Object);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateFacility(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void UpdateFacility_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Facility");

            facade.Setup(f => f.UpdateFacility("3445", model.Id, model.Level, model.Number)).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateFacility(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Facility you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void UpdateFacility_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.UpdateFacility("3445", model.Id, model.Level, model.Number)).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateFacility(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void UpdateFacility_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.UpdateFacility(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
