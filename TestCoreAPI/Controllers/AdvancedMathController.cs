using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvancedMathController : Controller
    {
        [HttpGet(Name = "AdvancedMath")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<float> AdvancedMath(float operandOne, float operandTwo, char operation)
        {
            float result;

            // Check for invalid entries
            if (operation == '\0')
                return BadRequest("An operation was NOT specified!  Please select one of the available operations:\nA = add\nS = subtract\nM = multiply\nD = divide\nR = remainder");

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
                        result = -1;
                        break;
                    }
            }

            return result;
        }


    }

}
