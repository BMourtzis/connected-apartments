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
    public class TenantCancelBookingTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private BookingCancelModel model;

        public TenantCancelBookingTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new BookingCancelModel() { BookingId = 3};

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void CreateBooking_RegularValues_OkResult()
        {
            //Arrange

            facade.Setup(f => f.CancelBooking("3445", model.BookingId));

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CancelBooking(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void CreateBooking_ThrowsError_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Booking");

            facade.Setup(f => f.CancelBooking("3445", model.BookingId)).Throws(exception);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CancelBooking(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Booking you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void CreateBooking_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.CancelBooking("3445", model.BookingId)).Throws(excpetion);

            var controller = new BookingController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.CancelBooking(model);

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
            var result = controller.CancelBooking(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
