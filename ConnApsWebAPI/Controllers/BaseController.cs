using System;
using System.Net.Http;
using System.Text;
using ConnApsDomain;
using ConnApsWebAPI.Models;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ConnApsWebAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly Facade Cad;
        protected ApplicationUserManager _userManager;

        protected BaseController()
        {
            Cad = new Facade();
        }

        protected BaseController(Facade facade)
        {
            Cad = facade;
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        #region Helpers

        protected IHttpActionResult GetResponse()
        {
            var response = new GenericResponse
            {
                IsSuccess = true
            };
            return Ok<GenericResponse>(response);
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        protected string GeneratePassword()
        {
            var builder = new StringBuilder();
            var random = new Random();
            var length = 3 * random.NextDouble() + 5;

            for (var i = 0; i < length; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(93 * random.NextDouble() + 33)));
                builder.Append(ch);
            }

            //Insert Number
            builder.Insert((int)(random.NextDouble() * length), Convert.ToChar(Convert.ToInt32(Math.Floor(9 * random.NextDouble() + 48))));
            length++;

            //Insert Upper Character
            builder.Insert((int)(random.NextDouble() * length), Convert.ToChar(Convert.ToInt32(Math.Floor(25 * random.NextDouble() + 65))));
            length++;

            //Insert Lower Character
            builder.Insert((int)(random.NextDouble() * length), Convert.ToChar(Convert.ToInt32(Math.Floor(25 * random.NextDouble() + 97))));

            return builder.ToString();
        }

        #endregion
    }
}