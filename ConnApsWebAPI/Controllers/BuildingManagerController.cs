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
        public IBuildingManager FetchBuildingManagerInfo()
        {
            var bm = CAD.FetchBuildingManager(User.Identity.GetUserId());
            return bm;
        }

        // Post api/BuildingManager/UpdateBuildingManager
        [HttpPut]
        [Route("UpdateBuildingManager")]
        public IBuildingManager UpdateBuildingManager(BuildingManagerBindingModel model)
        {
            var bm = CAD.UpdateBuildingManager(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB , model.Phone);
            return bm;
        }

        #endregion

        #region Building

        // GET api/BuildingManager/BuildingInfo
        [HttpGet]
        [Route("BuildingInfo")]
        public IBuilding FetchBuildingInfo()
        {
            var building = CAD.FetchBuildingManagerBuilding(User.Identity.GetUserId());
            return building;
        }

        // Post api/BuildingManager/UpdateBuilding
        [HttpPut]
        [Route("UpdateBuilding")]
        public IBuilding UpdateBuilding(BuildingBindingModel model)
        {
            var b = CAD.UpdateBuilding(User.Identity.GetUserId(), model.BuildingName, model.Address);
            return b;
        }

        #endregion

        #region Apartment

        // GET api/BuildingManager/FetchApartments
        [HttpGet]
        [Route("FetchApartments")]
        public IEnumerable<IApartment> FetchApartment()
        {
            var apt = CAD.FetchApartments(User.Identity.GetUserId());
            return apt;
        }

        // GET api/BuildingManager/FetchApartment
        [HttpGet]
        [Route("FetchApartment")]
        public IApartment FetchApartment(int aptId)
        {
            var apt = CAD.FetchApartment(aptId);
            return apt;
        }

        // POST api/BuildingManager/CreateApartment
        [HttpPost]
        [Route("CreateApartment")]
        public IApartment CreateApartment(ApartmentBindingModel model)
        {
            var apt = CAD.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, model.BuildingId);
            return apt;
        }

        // POST api/BuildingManager/UpdateApartment
        [HttpPut]
        [Route("UpdateApartment")]
        public IApartment UpdateApartment(ApartmentUpdateModel model)
        {
            var apt = CAD.UpdateApartment(model.Id, model.Level, model.Number, model.TenantsAllowed, model.FacingDirection);
            return apt;
        }

        #endregion

        #region Tenant

        // GET api/BuildingManager/FetchTenant
        [HttpGet]
        [Route("FetchTenant")]
        public ITenant FetchTenant(string userId)
        {
            var t = CAD.FetchTenant(userId);
            return t;
        }

        // PUT api/BuildingManager/UpdateTenant
        [HttpPut]
        [Route("UpdateTenant")]
        public ITenant UpdateTenant(BMTenantUpdateModel model)
        {
            var tenant = CAD.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DoB, model.Phone);
            return tenant;
        }

        // PUT api/BuildingManager/ChangeApartment
        [HttpPut]
        [Route("ChangeApartment")]
        public ITenant ChangeApartment(ChangeApartmentModel model)
        {
            var tenant = CAD.ChangeApartment(model.UserId, model.ApartmentId);
            return tenant;
        }

        // GET api/BuildingManager/FetchBuildingTenants
        [HttpGet]
        [Route("FetchBuildingTenants")]
        public IEnumerable<ITenant> FetchBuildingTenants()
        {
            var t = CAD.FetchBuildingTenants(User.Identity.GetUserId());
            return t;
        }

        #endregion
    }
}
