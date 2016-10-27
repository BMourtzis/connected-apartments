using ConnApsDomain;
using ConnApsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConnApsWebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected Facade CAD;

        protected BaseController()
        {
            CAD = new Facade();
        }

        protected BaseController(Facade facade)
        {
            CAD = facade;
        }

        protected IHttpActionResult getResponse()
        {
            var response = new GenericResponse
            {
                IsSuccess = true
            };
            return Ok<GenericResponse>(response);
        }
    }
}