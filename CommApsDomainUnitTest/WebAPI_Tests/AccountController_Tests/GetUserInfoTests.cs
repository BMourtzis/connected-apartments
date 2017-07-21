using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using ConnApsDomain;
using Moq;

namespace CommApsDomainUnitTest.WebAPI_Tests.AccountController_Tests
{
    //TODO: Fix this
    public class GetUserInfoTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;

        public GetUserInfoTests()
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

        //[Fact]
        //public void GetUserInfo_RegularValues_OkResults()
        //{
        //    //Arrange
        //    var tenant = new Mock<ITenant>();
        //    tenant.SetupGet(a => a.Id).Returns(2);

        //    facade.Setup(f => f.FetchTenant("3445")).Returns(tenant.Object);

        //    var controller = new AccountController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = controller.GetUserInfo();

        //    //Assert
        //    Assert.NotNull(result);

        //    var okresult = Assert.IsType<OkNegotiatedContentResult<TenantInformationModel>>(result);
        //    Assert.NotNull(okresult.Content);
        //    Assert.Equal("3445", okresult.Content.UserId);
        //}

        //[Fact]
        //public void GetUserInfo_InvalidValues_BadRequest()
        //{
        //    //Arrange
        //    var exception = new ConnApsDomain.Exceptions.NotFoundException("Tenant");

        //    facade.Setup(f => f.FetchTenant("3445")).Throws(exception);

        //    var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = controller.FetchTenant();

        //    //Assert
        //    Assert.NotNull(result);

        //    var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
        //    Assert.Equal("The Tenant you requested was not found. Please search again.", badRequest.Message);
        //}

        //[Fact]
        //public void GetUserInfo_ThrowsError_InternalError()
        //{
        //    //Arrange
        //    var exception = new ConnApsDomain.Exceptions.InternalException();

        //    facade.Setup(f => f.FetchTenant("3445")).Throws(exception);

        //    var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

        //    //Act
        //    var result = controller.FetchTenant();

        //    //Assert
        //    Assert.NotNull(result);

        //    Assert.IsType<InternalServerErrorResult>(result);
        //}
    }
}
