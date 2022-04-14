using NUnit.Framework;

namespace TestCoreAPI.MathHelpers
{
    public class PrimeFactors
    {
        public string GetPrimeFactorsAsEquation(int number)
        {
            List<int> primeFactors = GetPrimeFactors(number);
            String result = String.Empty;

            primeFactors.Sort();

            primeFactors.ForEach(factor => result += factor + " x ");

            result = result.Substring(0, result.Length - 2);

            result += "= " + number;
            
            return result;
        }

        public List<int> GetPrimeFactors(int number)
        {
            List<int> primeFactors = new List<int>();
            
            return primeFactors;
        }
    }
}
