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

            if (number == 1)
            {
                primeFactors.Add(1);
            }
            else
            {
                for (int i = 2; number > 1; i++)
                {
                    for (; number % i == 0; number /= i)
                    {
                        primeFactors.Add(i);
                    }
                }
            }
            
            return primeFactors;
        }
    }
}
