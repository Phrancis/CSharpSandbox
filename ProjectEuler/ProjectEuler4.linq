<Query Kind="Program">
  <Namespace>static MyExtensions</Namespace>
</Query>

/*
ProjectEuler4: Largest palindrome product
https://projecteuler.net/problem=4
various improvements from answers to this post: http://codereview.stackexchange.com/q/133345/42632

A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
Find the largest palindrome made from the product of two 3-digit numbers.
*/
void Main()
{
    Console.WriteLine("ProjectEuler4: Largest palindrome product");
    // this is the test case from the original statement/problem
    var numDigits = 2;
    var PE4 = new ProjectEuler4(numDigits);
    PE4.GetAnswer();
    // this is the challenge
    numDigits = 3;
    PE4 = new ProjectEuler4(numDigits);
    PE4.GetAnswer();
    // another test with 4 digits, for performance
    numDigits = 4;
    PE4 = new ProjectEuler4(numDigits);
    PE4.GetAnswer();
}

public class ProjectEuler4 
{
    private int numberOfDigits;
    
    public ProjectEuler4(int numberOfDigits)
    {
        this.numberOfDigits = numberOfDigits;
    }
    
    private int[] GetMinAndMax()
    {
        int min = 1;
        int max = 1;
        int[] minAndMax = new int[2];
        for (int i = numberOfDigits; i > 1; i--)
        {
            min *= 10;
        }
        max = (min * 10) - 1;
        minAndMax[0] = min;
        minAndMax[1] = max;
        return minAndMax;
    }
    
    private IEnumerable<int> GetAllPalindromes(int min, int max)
    {
        for(var value = (max * max); value >= (min * min); value--)
        {
            if (IsPalindrome(value))
            {
                yield return value;
            }
        }
    }

    // Iterate the list of palindromes starting with the largest, 
    // and returns the first instance of a palindrome number having both factors within the min and max boundaries.
    private int[] FindLargestPalindromeProduct(int min, int max, IEnumerable<int> allPalindromes)
    {
        int firstFactor = max;
        int secondFactor;
        var results = new int[3];
        results[0] = 0;
        while (results[0] == 0)
        {
            foreach (int n in allPalindromes)
            {
                for (int i = firstFactor; i > 0; i--)
                {
                    if (n % i == 0)
                    {
                        secondFactor = n / i;
                        if (secondFactor >= min && secondFactor <= max)
                        {
                            //Console.WriteLine("Digits: {3} | firstFactor: {0} | secondFactor: {1} | Answer: {2}", i, secondFactor, n, this.numberOfDigits);
                            results[0] = i;
                            results[1] = secondFactor;
                            results[2] = n;
                            return results;   
                        }
                    }
                }
            }
        }
        return results;
    }

    internal int GetAnswer()
    {
        var minAndMax = GetMinAndMax();
        var allPalindromes = GetAllPalindromes(minAndMax[0], minAndMax[1]);
        var results = FindLargestPalindromeProduct(minAndMax[0], minAndMax[1], allPalindromes);
        Console.WriteLine("Digits: {3} | firstFactor: {0} | secondFactor: {1} | Answer: {2}", results[0], results[1], results[2], this.numberOfDigits);
        return results[2];
    }
}