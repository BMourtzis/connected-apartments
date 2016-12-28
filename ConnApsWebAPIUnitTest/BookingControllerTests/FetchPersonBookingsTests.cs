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

namespace ConnApsWebAPIUnitTest.WebAPI.BookingControllerTests
{
    public class FetchPersonBookingsTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchPersonBookingsTests()
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
        public void FetchPersonBookings_RegularValues_OkResult()
        {
            //Arrange
            var booking = new Mock<IBooking>();
            booking.SetupGet(b => b.Id).Returns(2);
            booking.SetupGet(b => b.PersonId).Returns(10);
            booking.SetupGet(b => b.FacilityId).Returns(5);

            var bookingNew = new Mock<IBooking>();
            bookingNew.SetupGet(b => b.Id).Returns(3);
            bookingNew.SetupGet(b => b.PersonId).Returns(10);
            bookingNew.SetupGet(b => b.FacilityId).Returns(3);

            var bookingList = new List<IBooking>() { booking.Object, bookingNew.Object };

            facade.Setup(f => f.FetchBookings("3445")).Returns(bookingList);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchPersonBookings();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<IBooking>>>(result);
            Assert.Equal(2, okresult.Content.Count());
        }

        [Fact]
        public void FetchPersonBookings_ThrowsError_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Booking");

            facade.Setup(f => f.FetchBookings("3445")).Throws(exception);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchPersonBookings();

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Booking you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchPersonBookings_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchBookings("3445")).Throws(excpetion);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchPersonBookings();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
