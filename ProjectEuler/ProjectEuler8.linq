<Query Kind="Program" />

/*
ProjectEuler8: Largest product in a series
https://projecteuler.net/problem=8

The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.

Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?
*/
void Main()
{
    Console.WriteLine("ProjectEuler8: Largest product in a series");
    string theBigNumber = 
        "73167176531330624919225119674426574742355349194934" +
        "96983520312774506326239578318016984801869478851843" +
        "85861560789112949495459501737958331952853208805511" +
        "12540698747158523863050715693290963295227443043557" +
        "66896648950445244523161731856403098711121722383113" +
        "62229893423380308135336276614282806444486645238749" +
        "30358907296290491560440772390713810515859307960866" +
        "70172427121883998797908792274921901699720888093776" +
        "65727333001053367881220235421809751254540594752243" +
        "52584907711670556013604839586446706324415722155397" +
        "53697817977846174064955149290862569321978468622482" +
        "83972241375657056057490261407972968652414535100474" +
        "82166370484403199890008895243450658541227588666881" + // <-- here 9989 has highest product of 4 adjacent digits
        "16427171479924442928230863465674813919123162824586" +
        "17866458359124566529476545682848912883142607690042" +
        "24219022671055626321111109370544217506941658960408" +
        "07198403850962455444362981230987879927244284909188" +
        "84580156166097919133875499200524063689912560717606" +
        "05886116467109405077541002256983155200055935729725" +
        "71636269561882670428252483600823257530420752963450" ;
    //theBigNumber.Dump();
    //ProjectEuler8.GetAnswer(theBigNumber, 4);
    ProjectEuler8.GetAnswer(theBigNumber, 13);
}

public class ProjectEuler8
{
    public int[] numbersBigToSmall = new int[10] {9,8,7,6,5,4,3,2,1,0};
    
    // Brute force
    public static int[] GetSumsOfAdjacentDigits(string numberToParse, int numberOfAdjacentDigits)
    {
        char[] characters = numberToParse.ToCharArray();
        var numberOfCombinations = (characters.Length - numberOfAdjacentDigits) + 1;
        int[] sums = new int[numberOfCombinations];
        for(var arrIndex = 0; arrIndex < numberOfCombinations; arrIndex++)
        {
            for(var digit = arrIndex; digit < arrIndex + numberOfAdjacentDigits; digit++)
            {
                sums[arrIndex] += (int) char.GetNumericValue(characters[digit]);
            }
            //Console.WriteLine("ix[{0}]: {1}", arrIndex, sums[arrIndex]);
        }
        return sums;
    }
    
    public static int GetIndexOfLargest(int[] numbers)
    {
        var maxNumber = 0;
        var index = 0;
        for(var i = 0; i < numbers.Length; i++)
        {
            if(numbers[i] > maxNumber)
            {
                maxNumber = numbers[i];
                index = i;
            }
        }
        return index;
    }
    
    public static string GetConsecutiveDigits(int[] numbers, int index, int numberOfAdjacentDigits)
    {
        char[] chars = new char[numberOfAdjacentDigits];
        for(int i = index, c = 0; i < index + numberOfAdjacentDigits; i++, c++)
        {
            chars[c] = (char) numbers[i];
        }
        string s = new string(c);
        return s;
    }
    
    public static int GetAnswer(string numberToParse, int numberOfAdjacentDigits)
    {
        int[] sums = GetSumsOfAdjacentDigits(numberToParse, numberOfAdjacentDigits);
        var indexOfMax = GetIndexOfLargest(sums);
        Console.WriteLine(indexOfMax);
        return indexOfMax;
        //string answer = GetConsecutiveDigits(
    }
}