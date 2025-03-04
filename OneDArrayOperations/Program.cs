using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[5];

        int n = numbers.Length;

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

        // Call the sorting method from the BubbleSort class
        int[] arr = SortArray.Sort(numbers);
        //int[] arr = InsertArray.Insert(numbers);

        Console.Write("Display the Array: ");
        for (int w = 0; w < arr.Length; w++)
        {
            Console.Write($"{arr[w]} ");
        }
        Console.WriteLine("\n");
    }
}

class SortArray
{
    public static int[] Sort(int[] numbers)
    {
        int n = numbers.Length;
        int length = numbers.Length;
        int[] result = new int[n];
        int count = 0;

        do
        {
            int min = 0;
            // 1, 50, 20, 5
            // j = 1, min = 0, 50 < 1, min = 0
            // j = 2, min = 0, 20 < 1, min = 0
            // j = 3, min = 0, 5 < 1, min = 0

            if (count >= (length - 1))
            {
                result[count] = numbers[0];
                break;
            }

            // Find the index of the smallest element
            for (int j = 1; j < n; j++)
            {
                if (numbers[j] < numbers[min])
                {
                    min = j;
                }
            }

            // Store the smallest element in result array
            // 1, 50, 20, 5
            // min = 0, result[0] = 1, count = 1
            result[count] = numbers[min];
            count += 1;

            // 1, 50, 20, 5
            int[] arr = new int[n - 1];
            int counter = 0;
            for (int j = 0; j <= arr.Length; j++)
            {
                if (j != min)
                {
                    arr[counter] = numbers[j];
                    counter++;
                }
            }
            numbers = arr;

            n -= 1;

        } while (true);

        return result;
    }
}

//class SortArray
//{
//    public static int[] Sort(int[] numbers)
//    {
//        int n = numbers.Length;
//        int[] result = new int[n]; // Array to store sorted elements
//        int count = 0; // To track elements added to result array

//        do
//        {
//            int minIndex = 0;
//            //[5, 1, 7, 4, 8, 9]

//            // j = 1, => 1 < 5, => minIndex = 1
//            // j = 2, minIndex = 1, => 7 < 1 => minIndex = 1
//            // j = 3, minIndex = 1, => 4 < 1, => minIndex = 1
//            // j = 4, minIndex = 1, => 8 < 1, => minIndex = 1
//            // j = 5, minIndex = 1, => 9 < 1, => minIndex = 1

//            // Find the index of the smallest element
//            for (int j = 1; j < n; j++)
//            {
//                if (numbers[j] < numbers[minIndex])
//                {
//                    minIndex = j;
//                }
//            }

//            // Store the smallest element in result array
//            result[count] = numbers[minIndex];
//            count++;

//            // Remove the smallest element from numbers array by shifting elements left
//            for (int j = minIndex; j < n - 1; j++)
//            {
//                numbers[j] = numbers[j + 1];
//            }

//            n -= 1; // Reduce array size after removing element

//        } while (n > 0); // Continue until all elements are sorted

//        return result;
//    }
//}

class InsertArray
{
    public static int[] Insert(int[] numbers)
    {
        int n = numbers.Length;
        Console.WriteLine("Enter the position to insert:");
        int position_to_insert = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the element you want to insert in the array:");
        int element_to_insert = int.Parse(Console.ReadLine());

        int[] arr = new int[n + 1];
        int counter = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            if (i < position_to_insert)
            {
                arr[counter] = numbers[i];
            }
            else if (i == position_to_insert)
            {
                arr[i] = element_to_insert;
                counter++;
                continue;
            }
            else
            {
                arr[counter] = numbers[i - 1];
            }
            counter++;
        }

        // Shift the elements to the right
        //for (int i = n - 1; i >= position_to_insert; i--)
        //{
        //    numbers[i + 1] = numbers[i];
        //}

        //// Insert the new element
        //numbers[position_to_insert] = element_to_insert;
        //n += 1; // Increment the array

        return arr;
    }
}

//class InsertArray
//{
//    public static int[] Insert(int[] numbers)
//    {
//        int n = numbers.Length;
//        Console.WriteLine("Enter the position to insert:");
//        int position_to_insert = int.Parse(Console.ReadLine());
//        Console.WriteLine("Enter the element you want to insert in the array:");
//        int element_to_insert = int.Parse(Console.ReadLine());

//        // Adjust position_to_insert if it exceeds the length of numbers
//        if (position_to_insert > n)
//        {
//            position_to_insert = n;
//        }

//        int[] arr = new int[n + 1];
//        int counter = 0;

//        // Copy elements before the insertion point
//        for (int i = 0; i < position_to_insert; i++)
//        {
//            arr[counter] = numbers[i];
//            counter++;
//        }

//        // Insert the new element
//        arr[counter] = element_to_insert;
//        counter++;

//        // Copy the remaining elements
//        for (int i = position_to_insert; i < numbers.Length; i++)
//        {
//            arr[counter] = numbers[i];
//            counter++;
//        }

//        return arr;
//    }
//}



//static int[,] SortMatrix(int[,] arr)
//{
//    int row = arr.GetLength(0);
//    int col = arr.GetLength(1);
//    int temp;

//    //[{3, 2, 7},
//    //{4, 9 , 0}]
//    for (int k = 1; k < row; k++)
//    {
//        for (int j = 1; j < col; j++)
//        {
//            for (int m = 1; m < row; m++)
//            {
//                for (int z = 1; z < col; z++)
//                {
//                    if (arr[k, j] < arr[m, z])
//                    {
//                        temp = arr[k, j];
//                        arr[k, j] = arr[m, z];
//                        arr[m, z] = temp;
//                    }
//                }
//            }
//        }
//    }
//    return arr;
//}