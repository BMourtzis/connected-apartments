using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ConnApsDomain.Exceptions;
using ConnApsEmailService;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace ConnApsWebAPI.Controllers.API.V1
{
    /// <summary>
    /// This Controller is responsible for all the functions of the User Class (ASP.Net Identity)
    /// </summary>
    [Authorize, RoutePrefix("api/v1/Account")]
    public class AccountController : BaseController
    {
        private const string LocalLoginProvider = "Local";

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        /// <summary>
        /// Gets the User's information
        /// </summary>
        /// <returns>Returns the user's details as well as the details of the user's roles</returns>

        // GET api/Account
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer), Authorize(), Route()]
        public IHttpActionResult GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            var model = new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin?.LoginProvider,
                Roles = UserManager.GetRoles(User.Identity.GetUserId())
            };

            try
            {
                if (model.Roles[0] == "Tenant")
                {
                    var tenant = Cad.FetchTenant(User.Identity.GetUserId());
                    var userinfo = new TenantInformationModel(model, tenant);
                    return Ok<TenantInformationModel>(userinfo);
                }
                else if (model.Roles[0] == "BuildingManager")
                {
                    var bm = Cad.FetchBuildingManager(User.Identity.GetUserId());
                    var userinfo = new PersonInformationModel(model, bm);
                    return Ok<PersonInformationModel>(userinfo);
                }
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<UserInfoViewModel>(model);
        }
        
        /// <summary>
        /// Edits the user's details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

        // PUT api/Account/Update
        [HttpPut, Authorize, Route("Update")]
        public IHttpActionResult EditAccount(EditAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            try
            {
                Cad.UpdatePerson(model.FirstName, model.LastName, model.DoB, model.Phone, User.Identity.GetUserId());
                return GetResponse();
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Change the user's password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            return !result.Succeeded ? GetErrorResult(result) : GetResponse();
        }

        /// <summary>
        /// Resets the users password, and sends an email to the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

        // POST api/Account/ResetPassword
        [HttpPost, AllowAnonymous, Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassord(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var context = new ApplicationDbContext();
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                var password = GeneratePassword();
                var hashedPass = userManager.PasswordHasher.HashPassword(password);
                var cUser = await store.FindByEmailAsync(model.Email);
                await store.SetPasswordHashAsync(cUser, hashedPass);
                await store.UpdateAsync(cUser);

                EmailService.SendPasswordResetEmail(cUser.Email, password);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return GetResponse();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns a default response</returns>

        //TODO: Add exception handling
        // DELETE api/Account/Delete
        [Authorize(Roles = "Admin"), Route("Delete")]
        public IHttpActionResult DeleteUser(string email)
        {
            var user = UserManager.FindByEmail(email);
            UserManager.Delete(user);
            return GetResponse();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                Claim providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                Random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
