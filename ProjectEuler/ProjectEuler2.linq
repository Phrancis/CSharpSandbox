<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Plugins\Framework 4.6\MyExtensions.FW46.dll</Reference>
  <Namespace>static MyExtensions</Namespace>
</Query>

// ProjectEuler2: Even Fibonacci numbers
// https://projecteuler.net/problem=2

void Main()
{
    Console.WriteLine("ProjectEuler2: Even Fibonacci numbers");
    Int32 FOUR_MILLION = 4000000;
    ProjectEuler2 PE2 = new ProjectEuler2(FOUR_MILLION);
    Console.WriteLine("Answer is: {0}", PE2.GetAnswer());
}

class ProjectEuler2
{
    private List<Int32> fibonacciSequence;
    private Int32 maxValue;
    
    public ProjectEuler2(Int32 maxValue)
    {
        fibonacciSequence = new List<Int32>();
        this.maxValue = maxValue;
    }
    
    public Int32 GetAnswer()
    {
        PopulateFibonacciSequence();
        return SumEvenNumbers();
    }
    
    private void PopulateFibonacciSequence()
    {
        // populate first two values per challenge definition
        fibonacciSequence.Add(1);
        fibonacciSequence.Add(2);
        // indexes for list referral
        int firstIndex = 0;
        int secondIndex = 1;
        
        int currentNum = 0;
        while(currentNum < maxValue)
        {
            currentNum = fibonacciSequence[firstIndex] + fibonacciSequence[secondIndex];
            if(currentNum < maxValue) { fibonacciSequence.Add(currentNum); }
            firstIndex++;
            secondIndex++;
        }
    }  
    
    private int SumEvenNumbers()
    {
        var sum = 0;
        foreach (Int32 n in fibonacciSequence)
        {
            if (IsEvenNumber(n)) { sum += n; }
        }
        return sum;
    }
    
//    private bool IsEvenNumber(Int32 n) 
//    {
//        return (n % 2 == 0) ? true : false;
//    }
}