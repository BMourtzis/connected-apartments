using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ConnApsDomain;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ConnApsWebAPI.Controllers.API.V1
{
    /// <summary>
    /// Base Controller Class for the API section.
    /// </summary>
    public abstract class BaseController : ApiController
    {
        protected readonly IFacade Cad;
        protected ApplicationUserManager _userManager;

        /// <summary>
        /// Default Constructor
        /// </summary>
        protected BaseController()
        {
            Cad = new Facade();
        }

        /// <summary>
        /// Constructor that allows for Injection on the Facade
        /// </summary>
        /// <param name="facade"></param>
        protected BaseController(IFacade facade)
        {
            Cad = facade;
        }

        /// <summary>
        /// Constructor that allows for injection on both the Facade and the userManager
        /// </summary>
        /// <param name="facade"></param>
        /// <param name="userManager"></param>
        protected BaseController(IFacade facade, ApplicationUserManager userManager)
        {
            Cad = facade;
            UserManager = userManager;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if(Cad != null)
                {
                    Cad.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        /// <summary>
        /// Get a Default response with an HTTP 200 code
        /// </summary>
        /// <returns>Returns a default response</returns>
        protected IHttpActionResult GetResponse()
        {
            var response = new GenericResponse
            {
                IsSuccess = true
            };
            return Ok<GenericResponse>(response);
        }

        /// <summary>
        /// Returns a Bad Request response with the list of errors
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates a random password with the correct password patterns.
        /// This password needs to be changed as soon as it is given to the user.
        /// </summary>
        /// <returns>A Random password</returns>
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