using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvancedMathController : Controller
    {
        [HttpGet(Name = "AdvancedMath")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "-- Similar to the SimpleMath endpoint but with added functionality.",
            Description = "An operation is performed between two provided numbers. Operations are represented by a single letter (upper or lower case). Available operations are: A - add, S - subtract, M - multiply, D - divide and R - remainder")]
        public ActionResult<float> AdvancedMath(float operandOne, float operandTwo, char operation)
        {
            float result;

            switch (Char.ToUpper(operation))
            {
                case 'A':
                    {
                        result = operandOne + operandTwo;
                        break;
                    }
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
                case 'D':
                    {
                        result = operandOne / operandTwo;
                        break;
                    }
                case 'R':
                    {
                        result = operandOne % operandTwo;
                        break;
                    }
                default:
                    {
                        return BadRequest("The provided operation '" + operation + "' is not supported!  Please select one of the available operations:\nA = add\nS = subtract\nM = multiply\nD = divide\nR = remainder");
                    }
            }

            return result;
        }

    }
}
