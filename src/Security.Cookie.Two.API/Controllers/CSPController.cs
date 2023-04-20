using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Security.Cookie.Two.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSPController : ControllerBase
    {
        [AllowAnonymous, HttpGet, Route("add")]
        public IActionResult AddCSPHeader()
        {
            // Header are not exposed in angular, add the header with CSP as key

            return Ok();
        }

        [AllowAnonymous, HttpPost, Route("violation")]
        public IActionResult violation()
        {
            return Ok();
        }
    }
}
