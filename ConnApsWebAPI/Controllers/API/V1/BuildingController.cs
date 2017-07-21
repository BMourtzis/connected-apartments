using System;
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
    /// This Controller is responsible for all the functions of the Building Class
    /// </summary>
    
    [Authorize, RoutePrefix("api/v1/Building")]
    public class BuildingController : BaseController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BuildingController(): base (){ }

        /// <summary>
        /// Constructor that allowes for Dependency Injection on the Facade
        /// </summary>
        /// <param name="facade"></param>
        public BuildingController(IFacade facade) : base(facade) { }

        /// <summary>
        /// Constructor that allowes for Dependency Injection on both the Facade and the User Manager
        /// </summary>
        /// <param name="facade"></param>
        /// <param name="userManager"></param>
        public BuildingController(IFacade facade, ApplicationUserManager userManager): base(facade, userManager) { }

        /// <summary>
        /// Fetches a building
        /// </summary>
        /// <returns>Returns the building details or an error message</returns>

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

        /// <summary>
        /// Creates a new Building 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

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

                var emailService = new EmailService();
                emailService.SendBuildingCreationEmail(model.Email);
                return GetResponse();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        /// <summary>
        /// Updates a Building
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

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
