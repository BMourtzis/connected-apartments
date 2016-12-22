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
    /// This controller is responsible for all the functions of the Building Manager Class
    /// </summary>
    [Authorize, RoutePrefix("api/v1/Manager")]
    public class BuildingManagerController : BaseController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BuildingManagerController(): base (){ }

        /// <summary>
        /// Constructor that allowes for Dependency Injection
        /// </summary>
        /// <param name="facade"></param>
        public BuildingManagerController(IFacade facade): base(facade) { }

        /// <summary>
        /// Fetches the Building Managers of the Building
        /// </summary>
        /// <returns>Returns a list of the Building Manager details or an Error Message</returns>

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

        /// <summary>
        /// Creates a new Building Manager
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

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

        /// <summary>
        /// Updates a Building Managers details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

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
