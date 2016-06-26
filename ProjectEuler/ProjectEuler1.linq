<Query Kind="Program">
  <Namespace>System</Namespace>
</Query>

// ProjectEuler1: Multiples of 3 and 5
// https://projecteuler.net/problem=1
void Main()
{
    Console.WriteLine("ProjectEuler1: Multiples of 3 and 5");
    ProjectEuler1 PE1 = new ProjectEuler1(1, 1000);
    Console.WriteLine("Answer is: {0}", PE1.GetAnswer());
}

class ProjectEuler1
{
    private int min;
    private int max;
    
    public ProjectEuler1(int min, int max) 
    { 
        this.min = min;
        this.max = max;
    }

    private bool IsMultipleOf3Or5(int n)
    {
        return (n % 3 == 0 || n % 5 == 0) ? true : false;
    }
    
    private int SumMultiplesOf3Or5()
    {
        int sum = 0;
        for( ; min < max; min++)
        {
            if(IsMultipleOf3Or5(min)) { sum += min; }
        }
        return sum;
    }
    
    internal int GetAnswer()
    {
        return SumMultiplesOf3Or5();
    }
    
}

