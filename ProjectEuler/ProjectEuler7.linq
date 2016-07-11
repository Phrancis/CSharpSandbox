<Query Kind="Program">
  <Namespace>static MyExtensions</Namespace>
</Query>

/*
ProjectEuler7: 10001st prime
https://projecteuler.net/problem=7

By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
What is the 10 001st prime number?
*/
void Main()
{
    Console.WriteLine("ProjectEuler7: 10001st prime");
    ProjectEuler7.NthPrime(6).Dump();       // 13
    ProjectEuler7.NthPrime(10001).Dump();   // 104743
}

class ProjectEuler7
{
    // brute force method
    public static int NthPrime(int N)
    {
        var countOfPrimes = 1;
        var prime = 0;
        for(var i = 1;  countOfPrimes <= N; i++)
        {
            if(IsPrime(i))
            {
                prime = i;
                countOfPrimes++;
            }
        }
        return prime;
    }
}