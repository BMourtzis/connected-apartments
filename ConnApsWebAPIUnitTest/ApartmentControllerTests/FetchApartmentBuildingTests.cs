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

namespace ConnApsWebAPIUnitTest.WebAPI.ApartmentControllerTests
{
    public class FetchApartmentBuildingTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public FetchApartmentBuildingTests()
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
        public void FetchApartmentBuilding_RegularValues_OkResults()
        {
            //Arrange
            var apt = new Mock<IApartment>();
            apt.SetupGet(a => a.Id).Returns(2);

            var aptNew = new Mock<IApartment>();
            aptNew.SetupGet(a => a.Id).Returns(3);

            var aptlist = new List<IApartment>() {apt.Object, aptNew.Object};

            facade.Setup(f => f.FetchApartments("3445")).Returns(aptlist);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingApartments();

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<IApartment>>>(result);
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
        public void FetchApartmentBuilding_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Apartment");

            facade.Setup(f => f.FetchApartments("3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingApartments();

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Apartment you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void FetchApartmentBuilding_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.FetchApartments("3445")).Throws(exception);

            var controller = new ApartmentController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.FetchBuildingApartments();

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

    }
}
