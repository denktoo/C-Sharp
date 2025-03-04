using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int[,] arr = new int[6, 7];

        Random rnd = new Random();
        int value = 0;

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                if (i == 0)
                {
                    arr[0, j] = j + 1;
                }
                else if (j == 0)
                {
                    arr[i, 0] = i + 1;
                }
                else if (i == 1)
                {
                    value = rnd.Next(10, 10001);
                    while (value % 2 == 0)
                    {
                        value = rnd.Next(10, 10001);
                    }
                    arr[1, j] = value;
                    //do
                    //{
                    //    value = rnd.Next(10, 10001);
                    //} while (value % 2 == 0);
                    //arr[1, j] = value;
                }
                else if (i == 2)
                {
                    value = rnd.Next(10, 10001);
                    while (value % 2 != 0)
                    {
                        value = rnd.Next(10, 10001);
                    }
                    arr[2, j] = value;
                    //do
                    //{
                    //    value = rnd.Next(10, 10001);
                    //} while (value % 2 != 0);
                    //arr[2, j] = value;
                }
                else if (i == 3)
                {
                    bool isDuplicate = false;
                    do
                    {
                        value = rnd.Next(10, 10001);
                        isDuplicate = false;
                        for (int k = 0; k < arr.GetLength(1); k++)
                        {
                            if (value == arr[1, k] || value == arr[2, k])
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    } while (isDuplicate);
                    arr[3, j] = value;
                }
                else
                {
                    value = rnd.Next(1, 1000);
                    arr[i, j] = value;
                }
            }
        }

        Console.WriteLine("Display the Matrix:");

        // Print the Matrix
        PrintMatrix(arr);

        Console.WriteLine("\n");

        Console.WriteLine("Display the Sorted Matrix:");

        // Call SortMatrix Method
        SortMatrix(arr);

        // Print Matrix to the console
        PrintMatrix(arr);

    }

    static void PrintMatrix(int[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write($"{arr[i, j]}\t");
            }
            Console.WriteLine("\n");
        }
    }

    static int[,] SortMatrix(int[,] arr)
    {
        int row = arr.GetLength(0);
        int col = arr.GetLength(1);
        //int n = ((row - 1) * (col - 1));
        int[] OneDarr = new int[((row - 1) * (col - 1))];
        int index = 0;

        // Convert 2D to 1D
        for (int k = 1; k < row; k++)
        {
            for (int j = 1; j < col; j++)
            {
                OneDarr[index] = arr[k, j];
                index++;
            }
        }

        // Sort 1D array
        int[] sortedArr = Sort(OneDarr);

        // Convert 1d back to 2D
        int count = 0;
        for (int k = 1; k < row; k++)
        {
            for (int j = 1; j < col; j++)
            {
                arr[k, j] = sortedArr[count];
                count++;
            }
        }

        return arr;
    }

    static int[] Sort(int[] numbers)
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