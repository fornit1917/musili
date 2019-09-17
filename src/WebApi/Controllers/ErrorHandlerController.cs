using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers {
    [Route("error")]
    public class ErrorHandlerController : ControllerBase
    {
        [HttpGet("")]
        public object Error() {
            return new { Message = "Internal server error" };
        }
    }
}