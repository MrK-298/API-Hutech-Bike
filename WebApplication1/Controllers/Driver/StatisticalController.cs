using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.Driver
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Driver")]
    public class StatisticalController : ControllerBase
    {
    }
}
