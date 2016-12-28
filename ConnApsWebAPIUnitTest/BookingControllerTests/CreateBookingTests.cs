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

namespace ConnApsWebAPIUnitTest.WebAPI.BookingControllerTests
{
    public class CreateBookingTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private BookingCreateModel model;

        public CreateBookingTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new BookingCreateModel() {EndTime = new DateTime(2016, 12, 25, 23, 59, 00), FacilityId = 3, StartTime = new DateTime(2016, 12, 25, 20, 00, 00) };

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void CreateBooking_RegularValues_OkResult()
        {
            //Arrange
            var booking = new Mock<IBooking>();
            booking.SetupGet(b => b.Id).Returns(15);
            booking.SetupGet(b => b.FacilityId).Returns(model.FacilityId);
            booking.SetupGet(b => b.StartTime).Returns(model.StartTime);
            booking.SetupGet(b => b.EndTime).Returns(model.EndTime);
            booking.SetupGet(b => b.PersonId).Returns(4);

            facade.Setup(f => f.CreateBooking("3445", model.FacilityId, model.StartTime, model.EndTime))
                .Returns(booking.Object);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateBooking(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void CreateBooking_ThrowsError_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.BookingOverlapingException();

            facade.Setup(f => f.CreateBooking("3445", model.FacilityId, model.StartTime, model.EndTime)).Throws(exception);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateBooking(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("Your Booking is overlaping another booking. Please pick another time", badRequest.Message);
        }

        [Fact]
        public void CreateBooking_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.CreateBooking("3445", model.FacilityId, model.StartTime, model.EndTime)).Throws(excpetion);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CreateBooking(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void CreateBooking_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.CreateBooking(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
