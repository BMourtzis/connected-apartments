using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "Tenant")]
    [RoutePrefix("api/Tenant")]
    public class TenantController : BaseController
    {
        protected TenantFacade CAD;

        public TenantController()
        {
            CAD = new TenantFacade();
        }

        #region Tenant

        // GET api/BuildingManager/TenantInfo
        [HttpGet]
        [Route("TenantInfo")]
        public Response<ITenant> FetchTenant()
        {
            ITenant t;
            try
            {
                t = CAD.FetchTenant(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<ITenant>(e.Message);
            }
            return getResponse<ITenant>(t);
        }

        // PUT api/BuildingManager/UpdateTenant
        [HttpPut]
        [Route("UpdateTenant")]
        public Response<ITenant> UpdateTenant(TenantUpdateModel model)
        {
            ITenant t;
            try
            {
                t = CAD.UpdateTenant(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return getBadResponse<ITenant>(e.Message);
            }
            return getResponse<ITenant>(t);
        }

        #endregion

    }
}
