<Query Kind="Program">
  <Namespace>static MyExtensions</Namespace>
</Query>

/*
ProjectEuler6: Sum square difference
https://projecteuler.net/problem=6

The sum of the squares of the first ten natural numbers is,
1^2 + 2^2 + ... + 10^2 = 385

The square of the sum of the first ten natural numbers is,
(1 + 2 + ... + 10)^2 = 55^2 = 3025

Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.

Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
*/


void Main()
{
    Console.WriteLine("ProjectEuler6: Sum square difference");
    ProjectEuler6.GetAnswer(1, 10);     // 2640
    ProjectEuler6.GetAnswer(1, 100);    // correct answer
}

class ProjectEuler6
{
    // Get the sum of the squares of all numbers between min and max
    // e.g.: SumOfTheSquare(1,10) = 1^2 + 2^2 + ... + 10^2 = 385
    private static int SumOfTheSquares(int min, int max)
    {
        int sum = 0;
        for(var i = min; i <= max; i++)
        {
            sum += (int) Math.Pow((double)i, 2);
        }
        return sum;
    }
    // Get the square of the sum of all numbers between min and max
    // e.g.: SquareOfTheSum(1,10) = (1 + 2 + ... + 10)^2 = 55^2 = 3025
    private static int SquareOfTheSum(int min, int max)
    {
        int sum = 0;
        for(var i = min; i <= max; i++)
        {
            sum += i;
        }
        return (int) Math.Pow((long) sum, 2);
    }
    
    public static int GetAnswer(int min, int max)
    {
        int sumOfTheSquares = SumOfTheSquares(min, max);
        int squareOfTheSum = SquareOfTheSum(min, max);
        int answer = squareOfTheSum - sumOfTheSquares;
        Console.WriteLine("min: {0} | max: {1} | SumOfTheSquares: {2} | SquareOfTheSum: {3} | Difference: {4}",
            min, max, sumOfTheSquares, squareOfTheSum, answer
        );
        return answer;
    }
}