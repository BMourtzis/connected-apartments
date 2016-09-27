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
        protected Response<T> getBadResponse<T>(String message)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message
            };
        }

        protected Response<T> getResponse<T>(T result)
        {
            return new Response<T>()
            {
                IsSuccess = true,
                Result = result
            };
        }
    }
}