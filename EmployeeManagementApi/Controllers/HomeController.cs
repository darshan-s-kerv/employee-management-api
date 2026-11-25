using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Employee Management API");
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok();
        }
    }
}
