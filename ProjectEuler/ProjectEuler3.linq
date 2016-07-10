<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>static MyExtensions</Namespace>
  <Namespace>System.Numerics</Namespace>
</Query>

// ProjectEuler3: Largest prime factor
// https://projecteuler.net/problem=3
void Main()
{
    Console.WriteLine("ProjectEuler3: Largest prime factor");
    BigInteger testCase = 600851475143;
    ProjectEuler3 PE3 = new ProjectEuler3(testCase);
    Console.WriteLine("Prime factor of {0} is: {1}", testCase, PE3.GetAnswer());
}

class ProjectEuler3
{
    private BigInteger number;
    
    public ProjectEuler3(BigInteger number)
    {
        this.number = number;
    }
    
    public BigInteger GetAnswer()
    {
        // largest possible prime factor of a number is its square root [citation needed]
        BigInteger maxPrimeFactor = Sqrt(number);
        // make sure number we start from is odd, as even numbers are never going to be prime
        if (maxPrimeFactor % 2 == 0) { maxPrimeFactor += 1; }
        // iterating by 2s to skip even numbers
        for (BigInteger i = maxPrimeFactor; i >= 1; i = i - 2)
        {
            if (IsFactor(i, number) && IsPrime(i))
            {
                return i;
            }
        }
        return 1;
    }
    
    private bool IsFactor(BigInteger n, BigInteger factorOf)
    {
        return (factorOf % n == 0) ? true : false;
    }
    
    private bool IsPrime(BigInteger n)
    // Based on Wikipedia page for "Primality test"
    // https://en.wikipedia.org/wiki/Primality_test#Simple_methods
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
        else if (n % 2 == 0 || n % 3 ==0)
        {
            return false;
        }
        else if (  (n != 5 && n % 5 == 0))
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

//public static class BigNumbersUtils 
//{
//    // Extension methods below sourced from Stack Overflow answer:
//    // http://stackoverflow.com/a/6084813/3626537
//    // Author: RedGreenCode http://stackoverflow.com/users/4803/redgreencode
//    public static BigInteger Sqrt(this BigInteger n)
//    {
//        if (n == 0) { return 0; }
//        
//        int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
//        BigInteger root = BigInteger.One << (bitLength / 2);
//
//        while (!isSqrt(n, root))
//        {
//            root += n / root;
//            root /= 2;
//        }
//
//        return root;
//
//        throw new ArithmeticException("NaN");
//    }
//
//    private static Boolean isSqrt(BigInteger n, BigInteger root)
//    {
//        BigInteger lowerBound = root*root;
//        BigInteger upperBound = (root + 1)*(root + 1);
//
//        return (n >= lowerBound && n < upperBound);
//    }
//}