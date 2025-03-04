using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[20];

        Random rnd = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rnd.Next(1, 1001);
        }

        Console.Write("Display the Numbers: ");
        foreach (int i in numbers)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine("\n");

        SortArray.Sort(numbers);

        Console.Write("Display the Sorted Array: ");
        foreach (int w in numbers)
        {
            Console.Write($"{w} ");
        }
        Console.WriteLine("\n");
    }
}

class SortArray
{
    public static void Sort(int[] numbers)
    {
        int n = numbers.Length;
        int temp;

        //for (int k = 0; k < n; k++)
        //{
        //    for (int j = 0; j < n - k - 1; j++)
        //    {
        //        if (numbers[j] > numbers[j + 1])
        //        {
        //            temp = numbers[j];
        //            numbers[j] = numbers[j + 1];
        //            numbers[j + 1] = temp;
        //        }
        //    }
        //}

        int k = 0;
        while (k < n)
        {
            int j = 0;
            while (j < (n - k - 1))
            {
                if (numbers[j] > numbers[j + 1])
                {
                    temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
                j++;
            }
            k++;
        }

        //for (int k = n; k >= 1; k--)
        //{
        //    for (int j = 0; j < k - 1; j++)
        //    {
        //        if (numbers[j] > numbers[j + 1])
        //        {
        //            temp = numbers[j];
        //            numbers[j] = numbers[j + 1];
        //            numbers[j + 1] = temp;
        //        }
        //    }
        //}

        //int minpos;
        //for (int k = 0; k < n - 1; k++)
        //{
        //    minpos = k;
        //    for (int j = k + 1; j < n; j++)
        //        if (numbers[j] < numbers[minpos])
        //            minpos = j;
        //    temp = numbers[k];
        //    numbers[k] = numbers[minpos];
        //    numbers[minpos] = temp;
        //}
    }
}