using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleMathController : Controller
    {
        [HttpGet(Name = "simpleMath")]
        public String simpleMath(int operandOne, int operandTwo, char operation)
        {
            int result = operandOne + operandTwo;

            return result.ToString();
        }
    }
}
