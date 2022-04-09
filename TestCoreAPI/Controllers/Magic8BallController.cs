using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Magic8BallController : Controller
    {
        [HttpGet(Name = "magic8Ball")]
        public String magic8Ball()
        {
            String result = "My Magic 8 Ball is Broken. :( -- Sadness...";

            return result;
        }
    }
}
