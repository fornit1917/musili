using Microsoft.AspNetCore.Mvc;

namespace Musili.ApiApp.Controllers {
    [Route("error")]
    public class ErrorHandlerController : ControllerBase
    {
        [HttpGet("")]
        public object Error() {
            return new { Message = "Internal server error" };
        }
    }
}