﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsEmailService;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Manager")]
    public class BuildingManagerController : BaseController
    {
        // GET api/BuildingManager
        [HttpGet, Route()]
        public IHttpActionResult Index()
        {
            IEnumerable<IBuildingManager> bm;
            try
            {
                bm = Cad.FetchBuildingManagers(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<IEnumerable<IBuildingManager>>(bm);
        }

        // POST api/BuildingManager/Create
        [Authorize(Roles = "BuildingManager"), HttpPost, Route("Create")]
        public async Task<IHttpActionResult> CreateBuildingManager(BuildingManagerRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);
            model.Password = "";
            if (result.Succeeded)
            {
                try
                {
                    Cad.CreateBuildingManager(model.FirstName, model.LastName, model.DateOfBirth, model.Phone, user.Id,
                        model.BuildingId);
                    UserManager.AddToRole(user.Id, "BuildingManager");
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

                EmailService.SendBuildingCreationEmail(model.Email);
                return GetResponse();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        // POST api/BuildingManager/Updateaaaa
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("Update")]
        public IHttpActionResult UpdateBuildingManager(BuildingManagerUpdateModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateBuildingManager(model.UserId, model.FirstName, model.LastName, model.DateOfBirth, model.Phone);
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
