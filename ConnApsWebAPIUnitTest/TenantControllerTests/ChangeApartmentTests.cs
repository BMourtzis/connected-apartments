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

namespace ConnApsWebAPIUnitTest.TenantControllerTests
{
    public class ChangeApartmentTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private ChangeApartmentModel model;

        public ChangeApartmentTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new ChangeApartmentModel() {ApartmentId = 2, UserId = "3445"};

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void ChangeApartment_RegularValues_OkResults()
        {
            //Arrange
            var facility = new Mock<ITenant>();
            facility.SetupGet(a => a.Id).Returns(2);

            facade.Setup(f => f.ChangeApartment(model.UserId, model.ApartmentId)).Returns(facility.Object);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.ChangeApartment(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void ChangeApartment_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Tenant");

            facade.Setup(f => f.ChangeApartment(model.UserId, model.ApartmentId)).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.ChangeApartment(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Tenant you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void ChangeApartment_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.ChangeApartment(model.UserId, model.ApartmentId)).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.ChangeApartment(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void ChangeApartment_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.ChangeApartment(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
