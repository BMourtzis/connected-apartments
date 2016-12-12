using System;
using System.Threading.Tasks;
using System.Web.Http;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsEmailService;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Building")]
    public class BuildingController : BaseController
    {
        // GET api/Building
        [HttpGet, Route()]
        public IHttpActionResult FetchBuildingInfo()
        {
            IBuilding building;
            try
            {
                building = Cad.FetchBuilding(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IBuilding>(building);
        }

        //POST api/Building/Create
        [AllowAnonymous, HttpPost, Route("Create")]
        public async Task<IHttpActionResult> CreateBuilding(RegisterBuildingModel model)
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
                    Cad.CreateBuilding(model.FirstName, model.LastName, model.DateOfBirth, model.Phone, user.Id,
                        model.BuildingName, model.Address);
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

        // POST api/Building/Update
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("Update")]
        public IHttpActionResult UpdateBuilding(BuildingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateBuilding(User.Identity.GetUserId(), model.BuildingName, model.Address);
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
