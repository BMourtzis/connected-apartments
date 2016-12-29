using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace ConnApsWebAPIUnitTest.ApartmentControllerTests
{
    public class FetchApartmentWithParametersTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchApartmentWithParametersTests()
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
        public void FetchApartment_RegularValues_OkResult()
        {
            //Arrange
            var apt = new Mock<IApartment>();
            apt.SetupGet(a => a.Id).Returns(2);

            facade.Setup(f => f.FetchApartment(2, "3445")).Returns(apt.Object);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchApartment(2);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IApartment>>(result);
            Assert.Equal(2, okresult.Content.Id);
        }

        [Fact]
        public void FetchApartment_InvalidValue_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Apartment");

            facade.Setup(f => f.FetchApartment(2, "3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) {ControllerContext = controllerContext};

            //Act
            var result = controller.FetchApartment(2);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<BadRequestErrorMessageResult>(result);
        }

        [Fact]
        public void FetchApartment_ThrowsError_InternalServerError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchApartment(2, "3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) {ControllerContext = controllerContext};

            //Act
            var result = controller.FetchApartment(2);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
