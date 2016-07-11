<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\mscorlib.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	// Write code to test your extensions here. Press F5 to compile and run.
}

public static class MyExtensions
{

    /*****************************************************
    COLLECTION / LIST UTILITIES
    *****************************************************/

    /*
    Given a List<T> source and List<T> exceptions, create a List<T> result which contains all of source, without exceptions, 
    only omitting exact instances of a value, i.e., if a value appears twice in source, but once in exceptions, 
    then result will have that value exactly one time.
    For example:
    Source: { 2, 2, 2, 6 }
    Exceptions: { 2, 2, 3, 4, 5}
    Result: { 2, 6 }
    */
    public static IEnumerable<T> ExceptExact<T>(this IEnumerable<T> source, IEnumerable<T> exceptions)
    {
        var tExceptions = exceptions.ToList();
    
        foreach (var el in source)
        {
            var index = tExceptions.IndexOf(el);
            if (index >= 0)
            {
                tExceptions.RemoveAt(index);
                continue;
            }
            yield return el;
        }
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
    
    
    /*****************************************************
    INT UTILITIES
    *****************************************************/
    
    // Verify if a number is evenly divisible by another
    public static bool IsEvenlyDivisibleBy(int num, int divisor)
    {
        if (num % divisor == 0)
            { return true; }
        else 
            { return false; }
    }
    // Verify if a number is even
    public static bool IsEvenNumber(int n) 
    {
        return (n % 2 == 0) ? true : false;
    }
    // Checks whether a number is prime
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
        BigInteger i = 7;
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
    // Get all primes between the min and max numbers provided
    public static List<int> GetPrimesBetween(int min, int max)
    {
        var primes = new List<int>();
        for(var i = min; i <= max; i++)
        {
            if(IsPrime(i) && i != 1)
            {
                primes.Add(i);
            }
        }
        return primes;
    }
    // Generate prime factors for a number, e.g., input 12 returns 2 2 3
    public static IEnumerable<int> PrimeFactorize(int number)
    {
        // Improved based on http://codereview.stackexchange.com/a/134397/42632
        // Check divisibility by 2:
        while (number % 2 == 0) 
        {
            yield return 2;
            number /= 2;
        }
        // Check divisibility by 3, 5, 7, ...
        for (var i = 3; i * i <= number; i += 2) 
        {
            while (number % i == 0) 
            {
                yield return i;
                number /= i;
            }
        }
        if (number > 1) 
        {
            yield return number;
        }
    }
    // Equivalent to Math.Pow(long, long) for int type.
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
    // Returns the Nth root of an int
    public static int NthRoot(int number, int nthRoot)
    {
        double _number = (double)number;
        double _nthRoot = (double)nthRoot;
        double result = Math.Pow(_number, 1/_nthRoot);
        return (int)Math.Floor(result);
    }
    // Mimics Math.Sqrt(long) but for int type
    public static int Sqrt(int number)
    {
        return NthRoot(number, 2);
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
    
    /*****************************************************
    STRING UTILITIES
    *****************************************************/
    
    public static string Reverse(string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    
    /*****************************************************
    BigInteger UTILITIES
    *****************************************************/
    
    // Sqrt extension methods below sourced from Stack Overflow answer:
    // http://stackoverflow.com/a/6084813/3626537
    // Author: RedGreenCode http://stackoverflow.com/users/4803/redgreencode
    public static BigInteger Sqrt(BigInteger n)
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