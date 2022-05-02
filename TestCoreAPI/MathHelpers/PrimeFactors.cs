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

#pragma warning disable IDE0057 // Use range operator
            result = result.Substring(0, result.Length - 2);
#pragma warning restore IDE0057 // Use range operator

            result += "= " + number;
            
            return result;
        }

        public List<int> GetPrimeFactors(int number)
        {
            List<int> primeFactors = new();

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
