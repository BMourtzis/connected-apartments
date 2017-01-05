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

namespace ConnApsWebAPIUnitTest.AccountControllerTests
{
    public class EditAccountTests
    {
        private Mock<IFacade> facade;
        private HttpControllerContext controllerContext;
        private EditAccountModel model;

        public EditAccountTests()
        {
            facade = new Mock<IFacade>();

            var username = "3445";
            var identity = new GenericIdentity(username, "");
            var nameIdentityClaim = new Claim(ClaimTypes.NameIdentifier, username);
            identity.AddClaim(nameIdentityClaim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity);
            principal.Setup(p => p.IsInRole("Tenant")).Returns(true);

            model = new EditAccountModel() { DoB = new DateTime(1995, 03, 16), FirstName = "Vasileios", LastName = "Papadopoulos", Phone = "0123456789" };

            controllerContext = new HttpControllerContext { RequestContext = { Principal = principal.Object } };
        }

        [Fact]
        public void UpdateTenant_RegularValues_OkResults()
        {
            //Arrange
            var tenant = new Mock<IPerson>();
            tenant.SetupGet(a => a.Id).Returns(4);

            facade.Setup(f => f.UpdatePerson(model.FirstName, model.LastName, model.DoB, model.Phone, "3445"));

            var controller = new AccountController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.EditAccount(model);

            //Assert
            Assert.NotNull(result);

            var okresult = Assert.IsType<OkNegotiatedContentResult<GenericResponse>>(result);
            Assert.Equal(true, okresult.Content.IsSuccess);
        }

        [Fact]
        public void UpdateTenant_InvalidValues_BadRequest()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.NotFoundException("Person");

            facade.Setup(f => f.UpdatePerson(model.FirstName, model.LastName, model.DoB, model.Phone, "3445")).Throws(exception);

            var controller = new AccountController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.EditAccount(model);

            //Assert
            Assert.NotNull(result);

            var badRequest = Assert.IsType<BadRequestErrorMessageResult>(result);
            Assert.Equal("The Person you requested was not found. Please search again.", badRequest.Message);
        }

        [Fact]
        public void UpdateTenant_ThrowsError_InternalError()
        {
            //Arrange
            var exception = new ConnApsDomain.Exceptions.InternalException();

            facade.Setup(f => f.UpdatePerson(model.FirstName, model.LastName, model.DoB, model.Phone, "3445")).Throws(exception);

            var controller = new AccountController(facade.Object) { ControllerContext = controllerContext };

            //Act
            var result = controller.EditAccount(model);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void UpdateTenant_ThrowsError_BadModel()
        {
            //Arrange
            var controller = new AccountController(facade.Object) { ControllerContext = controllerContext };
            controller.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.EditAccount(model);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InvalidModelStateResult>(result);
        }
    }
}
