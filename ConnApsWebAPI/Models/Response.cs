using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{

    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public String Message { get; set; }
        public T Result { get; set; }
    }
}