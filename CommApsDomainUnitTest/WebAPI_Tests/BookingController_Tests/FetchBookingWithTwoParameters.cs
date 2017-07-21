using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace CommApsDomainUnitTest.WebAPI_Tests.BookingController_Tests
{
    public class FetchBookingWithTwoParameters
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchBookingWithTwoParameters()
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
        public void FetchBooking_RegularValues_OkResult()
        {
            //Arrange
            var booking = new Mock<IBooking>();
            booking.SetupGet(b => b.Id).Returns(2);
            booking.SetupGet(b => b.FacilityId).Returns(3);

            facade.Setup(f => f.FetchBooking("3445", 3, 2)).Returns(booking.Object);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBooking(3, 2);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IBooking>>(result);
            Assert.Equal(2, okresult.Content.Id);
        }

        [Fact]
        public void FetchBooking_ThrowsError_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Booking");

            facade.Setup(f => f.FetchBooking("3445", 3,  56)).Throws(exception);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBooking(3, 56);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Booking you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchBooking_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchBooking("3445", 3, 2)).Throws(excpetion);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBooking(3, 2);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
