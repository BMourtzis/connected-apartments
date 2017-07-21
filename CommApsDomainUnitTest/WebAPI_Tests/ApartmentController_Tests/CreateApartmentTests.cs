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

namespace CommApsDomainUnitTest.WebAPI_Tests.ApartmentController_Tests
{
    public class CreateApartmentTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private ApartmentBindingModel model;

        public CreateApartmentTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new ApartmentBindingModel() { FacingDirection = "North", Level = "3", Number = "1", TenantsAllowed = 2 };

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void CreateApartment_RegularValues_OkResults()
        {
            //Arrange
            var apt = new Mock<IApartment>();
            apt.SetupGet(a => a.Id).Returns(4);
            apt.SetupGet(a => a.BuildingId).Returns(1);
            apt.SetupGet(a => a.FacingDirection).Returns(model.FacingDirection);
            apt.SetupGet(a => a.Level).Returns(model.Level);
            apt.SetupGet(a => a.Number).Returns(model.Number);
            apt.SetupGet(a => a.TenantsAllowed).Returns(model.TenantsAllowed);

            facade.Setup(
                f => f.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, "3445")).Returns(apt.Object);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateApartment(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void CreateApartment_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Apartment");

            facade.Setup(f => f.CreateApartment(model.Level, model.Number,model.TenantsAllowed, model.FacingDirection, "3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateApartment(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Apartment you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void CreateApartment_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, "3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateApartment(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void CreateFacility_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.CreateApartment(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
