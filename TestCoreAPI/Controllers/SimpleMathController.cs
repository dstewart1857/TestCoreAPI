using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleMathController : ControllerBase
    {
        [HttpGet(Name = "simpleMath")]
        public int simpleMath(int operandOne, int operandTwo, char operation)
        {
            int result = operandOne + operandTwo;

            return result;
        }
    }
}
