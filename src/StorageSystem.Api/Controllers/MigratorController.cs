using Microsoft.AspNetCore.Mvc;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigratorController : ControllerBase
    {
        [HttpGet("Execute")]
        public ActionResult Execute()
        {

            return Ok(true);
        }
    }
}
