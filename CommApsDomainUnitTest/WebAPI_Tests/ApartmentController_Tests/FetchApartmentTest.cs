﻿using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace CommApsDomainUnitTest.WebAPI_Tests.ApartmentController_Tests
{
    public class FetchControllerTest
    {
        //https://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api

        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchControllerTest()
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

            facade.Setup(f => f.FetchApartment("3445")).Returns(apt.Object);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext};

            //Act
            var result = controller.FetchApartment();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IApartment>>(result);
            Assert.Equal(2, okresult.Content.Id);
        }

        [Fact]
        public void FetchApartment_ThrowsError_InternalServerError()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchApartment("3445")).Throws(excpetion);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchApartment();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void FetchApartment_ThrowsError_BadRequest()
        {
            //Arrange
            var excpetion = new ConnApsDomain.Exceptions.NotFoundException("Apartment");

            facade.Setup(f => f.FetchApartment("3445")).Throws(excpetion);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchApartment();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<BadRequestErrorMessageResult>(result);
        }
    }
}
