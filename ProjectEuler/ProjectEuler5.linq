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
    //ProjectEuler5.GetAnswer(1, 10);
    //ProjectEuler5.GetAnswer(1, 20);
    IntUtils.PrimeFactorize(100144).Dump();
}

public class ProjectEuler5
{
    
    public static void GetAnswer(int minDivisor, int maxDivisor)
    {
        var answer = 0;
               
        List<int> factors = new List<int>();
        for(int i = minDivisor; i <= maxDivisor; i++)
        {
            if(IntUtils.IsPrime(i))
            {
                factors.Add(i);
            }
            else
            {
                //
            }
        }
        factors.Dump();
        
        Console.WriteLine("Min Divisor: {0} | Max Divisor: {1} | Answer: {1}", 1, 10, answer);
    }
}