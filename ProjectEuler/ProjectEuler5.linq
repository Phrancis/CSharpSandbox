<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Plugins\Framework 4.6\MyExtensions.FW46.dll</Reference>
</Query>

// ProjectEuler5: Smallest multiple
// https://projecteuler.net/problem=5
/*
2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
*/

void Main()
{
    Console.WriteLine("ProjectEuler5: Smallest multiple");
    ProjectEuler5.GetAnswer(1, 10); // OK 2520
    ProjectEuler5.GetAnswer(1, 20); // Correct answer!
}

public class ProjectEuler5
{
    public static int GetAnswer(int minDivisor, int maxDivisor)
    {
        var factors = new List<int>();
        var primeFactors = new List<int>();
        var remainingFactors = new List<int>();

        for(var i = minDivisor +1; i <= maxDivisor; i++)
        {
            // prime numbers can be added directly, as they are not divisible
            if(IntUtils.IsPrime(i)) 
            {
                factors.Add(i);
            }
            // non-primes need to be broken down into prime factors, and processed to make sure 
            // not to add factors that are already present from previous iterations
            else
            {
                primeFactors.Clear();
                remainingFactors.Clear();
                
                // copy to hold the factors needed for this operation
                var factorsTempCopy = new List<int>(factors);
                
                // get the prime factors of the current number
                foreach(var F in IntUtils.PrimeFactorize(i))
                {
                    primeFactors.Add(F);
                }
                // Compare a copy of the current main factors list and the primeFactors and trim out the excess,
                // keeping only the primeFactors 
                remainingFactors = (List<int>) primeFactors.ExceptExact(factorsTempCopy);
                
                // Add remaining factors into the current main factors list
                foreach(var F in remainingFactors)
                {
                    factors.Add(F);
                }
            }
        }
        // finally, multiply all the factors to get the answer
        var answer = 1;
        foreach(var f in factors)
        {
            answer *= f;
        }
        Console.WriteLine("minDivisor: {0} | maxDivisor: {1} | smallest multiple: {2}", minDivisor, maxDivisor, answer);
        return answer;
    }
}