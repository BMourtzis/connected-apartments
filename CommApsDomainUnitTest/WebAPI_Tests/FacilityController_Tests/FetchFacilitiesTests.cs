﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ConnApsDomain;
using ConnApsDomain.Models;
using ConnApsWebAPI.Controllers.API.V1;
using Moq;
using Xunit;

namespace CommApsDomainUnitTest.WebAPI_Tests.FacilityController_Tests
{
    public class FetchFacilitiesTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchFacilitiesTests()
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
        public void FetchFacilities_RegularValues_OkResults()
        {
            //Arrange
            var bm = new Mock<IFacility>();
            bm.SetupGet(a => a.Id).Returns(2);

            var bmNew = new Mock<IFacility>();
            bmNew.SetupGet(a => a.Id).Returns(3);

            var bmList = new List<IFacility>() { bm.Object, bmNew.Object };

            facade.Setup(f => f.FetchFacilities("3445")).Returns(bmList);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacilities();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<IFacility>>>(result);
            Assert.NotNull(okresult.Content);
            Assert.Equal(2, okresult.Content.Count());

            var enumerator = okresult.Content.GetEnumerator();
            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            Assert.Equal(2, enumerator.Current.Id);

            enumerator.MoveNext();
            Assert.NotNull(enumerator.Current);
            Assert.Equal(3, enumerator.Current.Id);

            enumerator.Dispose();
        }

        [Fact]
        public void FetchFacilities_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Facility");

            facade.Setup(f => f.FetchFacilities("3445")).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacilities();

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Facility you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchFacilities_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchFacilities("3445")).Throws(exception);

            var controller = new FacilityController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchFacilities();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }
    }
}
