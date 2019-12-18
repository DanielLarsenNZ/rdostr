using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

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

        // GET api/configuration
        //[Authorize(Policy = "Aucklanders")]     // example of a claims check - see Startup.ConfigureServices
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            if (User == null || User.Claims == null || !User.Claims.Any())
            {
                return new UnauthorizedResult();
            }

            // add the claims to the result
            var result = new List<string>(User.Claims.Select(c => c.ToString()));

            // Add the value of a specific Key Vault key named "Rdostr-Mobile--DataKey1" to the result
            result.Add(_config["Rdostr-Mobile:DataKey1"]);

            return new JsonResult(result.ToArray());
        }
    }
}
