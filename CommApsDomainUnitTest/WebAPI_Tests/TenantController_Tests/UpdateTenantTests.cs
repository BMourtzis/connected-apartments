using System;
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

namespace CommApsDomainUnitTest.WebAPI_Tests.TenantController_Tests
{
    public class UpdateTenantTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private TenantUpdateModel model;

        public UpdateTenantTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new TenantUpdateModel() {DateofBirth = new DateTime(1995, 03, 16), FirstName = "Vasileios", LastName = "Papadopoulos", Phone = "0123456789", UserId = "3445"};

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void UpdateTenant_RegularValues_OkResults()
        {
            //Arrange
            var tenant = new Mock<ITenant>();
            tenant.SetupGet(a => a.Id).Returns(4);

            facade.Setup(f => f.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DateofBirth, model.Phone)).Returns(tenant.Object);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateTenant(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void UpdateTenant_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Tenant");

            facade.Setup(f => f.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DateofBirth, model.Phone)).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateTenant(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Tenant you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void UpdateTenant_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DateofBirth, model.Phone)).Throws(exception);

            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.UpdateTenant(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void UpdateTenant_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new TenantController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.UpdateTenant(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
