using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleMathController : Controller
    {

        [HttpGet(Name = "simpleMath")]
        public float simpleMath(float operandOne, float operandTwo, char operation)
        {
            float result;

            switch (Char.ToUpper(operation))
            {
                case 'S':
                {
                    result = operandOne - operandTwo;
                    break;
                }
                case 'M':
                {
                    result = operandOne * operandTwo;
                    break;
                }
                case 'R':
                {
                    result = operandOne % operandTwo;
                    break;
                }
                default:
                {
                    result = operandOne + operandTwo;
                    break;
                }
            }

            return result;
        }

    }
}
