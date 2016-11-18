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
        protected Facade Cad;

        protected BaseController()
        {
            Cad = new Facade();
        }

        protected BaseController(Facade facade)
        {
            Cad = facade;
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