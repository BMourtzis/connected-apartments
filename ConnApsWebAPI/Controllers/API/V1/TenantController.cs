using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ConnApsDomain;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsEmailService;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers.API.V1
{
    /// <summary>
    /// This controller is responsible for all the functions of the Tenant Class
    /// </summary>
    [Authorize, RoutePrefix("api/v1/Tenant")]
    public class TenantController : BaseController
    {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TenantController(): base (){ }

        /// <summary>
        /// Constructor that allowes for Dependency Injection
        /// </summary>
        /// <param name="facade"></param>
        public TenantController(IFacade facade): base(facade) { }

        /// <summary>
        /// Fetches the user's tenant information. The User needs to be a tenant
        /// </summary>
        /// <returns>Returns the tenant details or an Error Message</returns>

        // GET api/Tenant
        [Authorize(Roles = "Tenant"), HttpGet, Route()]
        public IHttpActionResult FetchTenant()
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<ITenant>(t);
        }

        /// <summary>
        /// Fetches a tenant
        /// </summary>
        /// <param name="id">The Id of the user that connects to the tenant</param>
        /// <returns>Returns the tenant details or an Error Message</returns>

        // GET api/Tenant?userId=string
        [HttpGet, Route()]
        public IHttpActionResult FetchTenant(string id)
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(id);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<ITenant>(t);
        }

        /// <summary>
        /// Fetches all the tenants of the building
        /// </summary>
        /// <returns>Returns a list of the tenant details or an Error Message</returns>

        // GET api/Tenant/Building
        [HttpGet, Route("Building")]
        public IHttpActionResult FetchBuildingTenants()
        {
            IEnumerable<ITenant> t;
            try
            {
                t = Cad.FetchTenants(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<IEnumerable<ITenant>>(t);
        }

        /// <summary>
        /// Creates a new tenant
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

        //POST api/Tenant/Create
        [Authorize(Roles = "BuildingManager"), HttpPost, Route("Create")]
        public async Task<IHttpActionResult> CreateTenant(RegisterTenantModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
            var password = GeneratePassword();

            var result = await UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                ITenant tenant;
                try
                {
                    tenant = Cad.CreateTenant(model.FirstName, model.LastName, model.DoB, model.Phone, user.Id,
                            model.ApartmentId, User.Identity.GetUserId());
                    UserManager.AddToRole(user.Id, "Tenant");
                }
                catch (ConnectedApartmentsException e)
                {
                    UserManager.Delete(user);
                    return BadRequest(e.Message);
                }
                catch (Exception)
                {
                    return InternalServerError();
                }

                var emailService = new EmailService();
                emailService.SendTenantCreationEmail(model.Email, password);
                return Ok<ITenant>(tenant);
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        /// <summary>
        /// Updates the Tenant's details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

        // PUT api/Tenant/Update
        [Authorize(Roles = "BuildingManager"),HttpPut, Route("Update")]
        public IHttpActionResult UpdateTenant(TenantUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DateofBirth, model.Phone);
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
        /// Changes the apartment that a tenant lives in
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

        // PUT api/Tenant/ChangeApartment
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("ChangeApartment")]
        public IHttpActionResult ChangeApartment(ChangeApartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.ChangeApartment(User.Identity.GetUserId(), model.UserId, model.ApartmentId);
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
    }
}
