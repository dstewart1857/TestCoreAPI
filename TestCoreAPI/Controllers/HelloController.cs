using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet(Name = "hello")]
        public String helloWorld(String name)
        {
            return "Hello " + name + "\n\nHave a GREAT DAY!!!";
        }
    }
}
