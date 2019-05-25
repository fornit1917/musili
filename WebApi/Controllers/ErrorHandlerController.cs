using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("error")]
    public class ErrorHandlerController : ControllerBase
    {
        [HttpGet("")]
        public object Error() {
            return new { Message = "Internal server error" };
        }
    }
}