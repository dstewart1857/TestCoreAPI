using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet(Name = "helloWorld")]
        public String helloWorld(String name)
        {
            return "Hello " + name;
        }
    }
}
