class Program
{
    static void Main(string[] args)
    {
        ListEvenNumbers();
    }
    static void ListEvenNumbers()
    {
        foreach (int number in EvenSequence(5, 18))
        {
            Console.Write(number.ToString() + " ");
        }
        Console.WriteLine();
        // Output: 6 8 10 12 14 16 18
    }

    static IEnumerable<int> EvenSequence(
        int firstNumber, int lastNumber)
    {
        // Yield even numbers in the range.
        for (var number = firstNumber; number <= lastNumber; number++)
        {
            if (number % 2 == 0)
            {
                yield return number;
            }
        }
    }
}