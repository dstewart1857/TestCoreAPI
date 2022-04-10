using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.MathHelpers;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimeFactorController : Controller
    {
        [HttpGet(Name = "primeFactorsEquation")]
        public String primeFactorsEquation(int number)
        {
            PrimeFactors primeFactors = new PrimeFactors();
            return primeFactors.GetPrimeFactorsAsEquation(number);
        }
    }
}
