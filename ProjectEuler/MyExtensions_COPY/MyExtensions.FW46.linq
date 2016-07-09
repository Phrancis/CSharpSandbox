<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	// Write code to test your extensions here. Press F5 to compile and run.
}

public static class MyExtensions
{
	// Write custom extension methods here. They will be available to all queries.
	
}

//////// FRANCIS CUSTOM STUFF

public static class IntUtils
{ 
    public static Boolean IsPrime(int n)
    {
        // short-circuit very common numbers
        if (n <= 1)
        {
            return false;
        }
        else if (n <= 3) 
        {
            return true;
        }   
        else if (  n % 2 == 0 
                || n % 3 == 0 
                || (n != 5 && n % 5 == 0))
        {
            return false;
        }
        // iterate with trial division
        BigInteger i = 5;
        while (i * i <= n)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
            i++;
        }
        return true;
    }
    
    public static List<int> GetPrimesBetween(int min, int max)
    {
        var primes = new List<int>();
        for(var i = min; i <= max; i++)
        {
            if(IntUtils.IsPrime(i) && i != 1)
            {
                primes.Add(i);
            }
        }
        return primes;
    }
    
    // Generate prime factors for a number, e.g., input 12 returns 2 2 3
    public static IEnumerable<int> PrimeFactorize(int number)
    {
        if(IntUtils.IsPrime(number))
        {
            yield return number;
        }
        else
        {
            List<int> primes = IntUtils.GetPrimesBetween(2, number);            
            // TODO this works but is a little slow, maybe it can be made faster with better algorithm
            while(!IntUtils.IsPrime(number))
            {
                foreach(var p in primes)
                {
                    if(number % p == 0)
                    {
                        yield return p;
                        number /= p;
                        break;
                    }
                }
            }
            yield return number;
        }
    }
    
    
    // Equivalent to Math.Pow(long) for int type.
    public static int Pow(int baseNum, int exponent)
    {
        if (exponent == 0) { return 1; }
        else if (exponent == 1) { return baseNum; }
        else 
        {
            while (exponent > 1)
            {
                baseNum *= baseNum;
                exponent--;
            }
        }
        return baseNum;
    }
    // Check if a number is palindrome, i.e., reads the same backward or forward.
    public static bool IsPalindrome(int number)
    {
        // improvements from: http://codereview.stackexchange.com/a/133372/42632
        string lexicalNumber = number.ToString();
        int start = 0;
        int end = lexicalNumber.Length - 1;
        while (start < end)
        {
            if (lexicalNumber[start++] != lexicalNumber[end--])
            {
                return false;
            }
        }
        return true;
    }
    
    public static bool IsDivisibleBy(int num, int divisor)
    {
        if (num % divisor == 0)
            { return true; }
        else 
            { return false; }
    }
    
    // This method is designed to remove multiples from a list of numbers
    // For example, an input of [2,3,6,8] should return [2,3]
    public static List<int> RemoveMultiples(List<int> numbers)
    {
        // based on remove_multiples function at: http://codereview.stackexchange.com/q/133612/42632
        numbers.Sort();
        var length = numbers.Count;
        var i = 0;
        var j = 0;
        while (i < length)
        {
            j = i + 1;
            while (j < length)
            {
                if (numbers[j] % numbers[i] == 0)
                {
                    numbers.RemoveAt(j);
                    length -= 1;
                }
                else
                {
                    j += 1;
                }
            }
            i += 1;
        }
        return numbers;
    }
}

public static class BigIntegerUtils
{
    // Sqrt extension methods below sourced from Stack Overflow answer:
    // http://stackoverflow.com/a/6084813/3626537
    // Author: RedGreenCode http://stackoverflow.com/users/4803/redgreencode
    public static BigInteger Sqrt(this BigInteger n)
    {
        if (n == 0) { return 0; }
        
        int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
        BigInteger root = BigInteger.One << (bitLength / 2);

        while (!IsSqrt(n, root))
        {
            root += n / root;
            root /= 2;
        }

        return root;

        throw new ArithmeticException("NaN");
    }
    // companion method to Sqrt extension method
    public static Boolean IsSqrt(BigInteger n, BigInteger root)
    {
        BigInteger lowerBound = root*root;
        BigInteger upperBound = (root + 1)*(root + 1);

        return (n >= lowerBound && n < upperBound);
    }
    
    private static bool IsFactor(BigInteger n, BigInteger factorOf)
    {
        return (factorOf % n == 0) ? true : false;
    }
    
    // Returns whether a BigInteger is a prime number
    // Based on Wikipedia page for "Primality test"
    // https://en.wikipedia.org/wiki/Primality_test#Simple_methods
    private static Boolean IsPrime(BigInteger n)
    {
        // short-circuit very common numbers
        if (n <= 1)
        {
            return false;
        }
        else if (n <= 3) 
        {
            return true;
        }   
        else if (  n % 2 == 0 
                || n % 3 == 0 
                || (n != 5 && n % 5 == 0))
        {
            return false;
        }
        // iterate with trial division
        BigInteger i = 5;
        while (i * i <= n)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
            i++;
        }
        return true;
    }
}

public class StringUtils
{
    public static string Reverse(string s)
    {
        // Source: http://stackoverflow.com/a/228060/3626537
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}