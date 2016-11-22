using ConnApsDomain;
using ConnApsWebAPI.Models;
using System.Web.Http;

namespace ConnApsWebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected Facade Cad;

        protected BaseController() { }

        protected BaseController(Facade facade)
        {
            Cad = facade;
        }

        protected IHttpActionResult GetResponse()
        {
            var response = new GenericResponse
            {
                IsSuccess = true
            };
            return Ok<GenericResponse>(response);
        }
    }
}