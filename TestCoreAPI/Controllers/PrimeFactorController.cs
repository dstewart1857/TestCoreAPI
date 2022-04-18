using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.MathHelpers;
using Swashbuckle.AspNetCore.Annotations;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimeFactorController : ControllerBase
    {
        [HttpGet(Name = "primeFactorsEquation")]
        [SwaggerOperation(Summary = "-- Finds all prime factors for a number and returns an equation like string (i.e. for 10 you would get: 2 x 5 = 10).")]
        public String primeFactorsEquation(int number)
        {
            PrimeFactors primeFactors = new PrimeFactors();
            return primeFactors.GetPrimeFactorsAsEquation(number);
        }
    }
}
