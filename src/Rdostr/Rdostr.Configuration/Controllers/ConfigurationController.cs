using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Rdostr.Configuration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            if (User == null || User.Claims == null)
            {
                return new UnauthorizedResult();
            }

            ClaimsCheck(User.Claims);

            //TODO: Get Key from KeyVault
            var config = new { Foo = "Bar" };

            return new JsonResult(config);
        }

        private void ClaimsCheck(IEnumerable<Claim> claims)
        {
            //TODO: Implement check claims business logic
            return;
        }
    }
}
