using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Rdostr.Configuration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        readonly IConfiguration _config;

        public ConfigurationController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            if (User == null || User.Claims == null || !User.Claims.Any())
            {
                return new UnauthorizedResult();
            }

            ClaimsCheck(User.Claims);

            //TODO: Get Key from KeyVault

            var result = new List<string>(User.Claims.Select(c => c.ToString()));
            result.Add(_config["Rdostr-Mobile--DataKey1"]);
            result.Add(_config["Rdostr-Mobile:DataKey1"]);

            return new JsonResult(result.ToArray());
        }

        private void ClaimsCheck(IEnumerable<Claim> claims)
        {
            //TODO: Implement check claims business logic
            return;
        }
    }
}
