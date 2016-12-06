using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ConnApsDomain.Models;
using ConnApsEmailService;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Tenant")]
    public class TenantController : BaseController
    {
        // GET api/Tenant
        [Authorize(Roles = "Tenant"), HttpGet, Route()]
        public IHttpActionResult FetchTenant()
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<ITenant>(t);
        }

        // GET api/Tenant?userId=string
        [HttpGet, Route()]
        public IHttpActionResult FetchTenant(string userId)
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<ITenant>(t);
        }

        // GET api/Tenant/Building
        [HttpGet, Route("Building")]
        public IHttpActionResult FetchBuildingTenants()
        {
            IEnumerable<ITenant> t;
            try
            {
                t = Cad.FetchTenants(User.Identity.GetUserId());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<ITenant>>(t);
        }

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
                    using (Cad)
                    {
                        tenant = Cad.CreateTenant(model.FirstName, model.LastName, model.DoB, model.Phone, user.Id, model.ApartmentId, User.Identity.GetUserId());
                    }
                    UserManager.AddToRole(user.Id, "Tenant");
                }
                catch (Exception e)
                {
                    UserManager.Delete(user);
                    return BadRequest(e.Message);
                }

                EmailService.SendTenantCreationEmail(model.Email, password);
                return Ok<ITenant>(tenant);
            }
            else
            {
                return GetErrorResult(result);
            }
        }

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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

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
                Cad.ChangeApartment(model.UserId, model.ApartmentId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }
    }
}
