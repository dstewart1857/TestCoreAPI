using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleMathController : ControllerBase
    {

        [HttpGet(Name = "simpleMath")]
        [SwaggerOperation(Summary = "-- Performs a specified operation between two provided operands. Available operations: A or a - add, S or s - subtract, M or m - multiply, R or r - remainder")]
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
