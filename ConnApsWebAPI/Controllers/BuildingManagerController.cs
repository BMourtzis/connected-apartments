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
    [Authorize(Roles = "BuildingManager")]
    [RoutePrefix("api/BuildingManager")]
    public class BuildingManagerController : BaseController
    {
        protected BuildingManagerFacade CAD;

        public BuildingManagerController()
        {
            CAD = new BuildingManagerFacade();
        }

        #region Building Manager

        // GET api/BuildingManager/BuildingManagerInfo
        [HttpGet]
        [Route("BuildingManagerInfo")]
        public Response<IBuildingManager> FetchBuildingManagerInfo()
        {
            IBuildingManager bm;
            try
            {
                bm = CAD.FetchBuildingManager(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<IBuildingManager>(e.Message);
            }
            return getResponse<IBuildingManager>(bm);
        }

        // Post api/BuildingManager/UpdateBuildingManager
        [HttpPut]
        [Route("UpdateBuildingManager")]
        public Response<IBuildingManager> UpdateBuildingManager(BuildingManagerBindingModel model)
        {
            IBuildingManager bm;
            try
            {
                bm = CAD.UpdateBuildingManager(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return getBadResponse<IBuildingManager>(e.Message);
            }
            return getResponse<IBuildingManager>(bm);
        }

        #endregion

        #region Building

        // GET api/BuildingManager/BuildingInfo
        [HttpGet]
        [Route("BuildingInfo")]
        public Response<IBuilding> FetchBuildingInfo()
        {
            IBuilding building;
            try
            {
                building = CAD.FetchBuildingManagerBuilding(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<IBuilding>(e.Message);
            }
            return getResponse<IBuilding>(building);
        }

        // Post api/BuildingManager/UpdateBuilding
        [HttpPut]
        [Route("UpdateBuilding")]
        public Response<IBuilding> UpdateBuilding(BuildingBindingModel model)
        {
            IBuilding building;
            try
            {
                building = CAD.UpdateBuilding(User.Identity.GetUserId(), model.BuildingName, model.Address);
            }
            catch (Exception e)
            {
                return getBadResponse<IBuilding>(e.Message);
            }
            return getResponse<IBuilding>(building);
        }

        #endregion

        #region Apartment

        /// <summary>
        /// Returns a List of Apartments that belong to the building
        /// </summary>
        /// <returns>A Response that includes a list of apartments</returns>

        // GET api/BuildingManager/FetchApartments
        [HttpGet]
        [Route("FetchApartments")]
        public Response<IEnumerable<IApartment>> FetchApartments()
        {
            IEnumerable<IApartment> apt;
            try
            {
                apt = CAD.FetchApartments(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<IEnumerable<IApartment>>(e.Message);
            }
            return getResponse<IEnumerable<IApartment>>(apt);
        }

        // GET api/BuildingManager/FetchApartment
        [HttpGet]
        [Route("FetchApartment")]
        public Response<IApartment> FetchApartment(int aptId)
        {
            IApartment apt;
            try
            {
                apt = CAD.FetchApartment(aptId);
            }
            catch (Exception e )
            {
                return getBadResponse<IApartment>(e.Message);
            }
            return getResponse<IApartment>(apt);
        }

        // POST api/BuildingManager/CreateApartment
        [HttpPost]
        [Route("CreateApartment")]
        public Response<IApartment> CreateApartment(ApartmentBindingModel model)
        {
            IApartment apt;
            try
            {
                apt = CAD.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, CAD.GetBuildingId(User.Identity.GetUserId()));
            }
            catch (Exception e)
            {
                return getBadResponse<IApartment>(e.Message);
            }
            return getResponse<IApartment>(apt);
        }

        // POST api/BuildingManager/UpdateApartment
        [HttpPut]
        [Route("UpdateApartment")]
        public Response<IApartment> UpdateApartment(ApartmentUpdateModel model)
        {
            IApartment apt;
            try
            {
                apt = CAD.UpdateApartment(model.Id, model.Level, model.Number, model.TenantsAllowed, model.FacingDirection);
            }
            catch (Exception e)
            {
                return getBadResponse<IApartment>(e.Message);
            }
            return getResponse<IApartment>(apt);
        }

        #endregion

        #region Tenant

        // GET api/BuildingManager/FetchTenant
        [HttpGet]
        [Route("FetchTenant")]
        public Response<ITenant> FetchTenant(string userId)
        {
            ITenant t;
            try
            {
                t = CAD.FetchTenant(userId);
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
        public Response<ITenant> UpdateTenant(BMTenantUpdateModel model)
        {
            ITenant t;
            try
            {
                t = CAD.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return getBadResponse<ITenant>(e.Message);
            }
            return getResponse<ITenant>(t);
        }

        // PUT api/BuildingManager/ChangeApartment
        [HttpPut]
        [Route("ChangeApartment")]
        public Response<ITenant> ChangeApartment(ChangeApartmentModel model)
        {
            ITenant t;
            try
            {
                t = CAD.ChangeApartment(model.UserId, model.ApartmentId);
            }
            catch (Exception e)
            {
                return getBadResponse<ITenant>(e.Message);
            }
            return getResponse<ITenant>(t);
        }

        // GET api/BuildingManager/FetchBuildingTenants
        [HttpGet]
        [Route("FetchBuildingTenants")]
        public Response<IEnumerable<ITenant>> FetchBuildingTenants()
        {
            IEnumerable<ITenant> t;
            try
            {
                t = CAD.FetchBuildingTenants(User.Identity.GetUserId());
            }
            catch (Exception e)
            {

                return getBadResponse<IEnumerable<ITenant>>(e.Message);
            }
            return getResponse<IEnumerable<ITenant>>(t);
        }

        #endregion
    }
}
