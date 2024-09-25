////Program to show the use of Console Class Properties and Beep Method
//Console.BackgroundColor = ConsoleColor.Blue;
//Console.ForegroundColor = ConsoleColor.White;
//Console.Title = "Understanding Console Class";
//Console.WriteLine("BackgroundColor: Blue");
//Console.WriteLine("ForegroundColor: White");
//Console.WriteLine("Title: Understanding Console Class");
//Console.WriteLine("");

//// Decimal literal
////Allowed Digits: 0 to 9
//int a = 101; //No suffix is required
//// Hexa-Decimal Literal
////Allowed Digits: 0 to 9 and Character a to f
//int c = 0x123f; //Prefix with 0x, and suffix with f
////Binary literal
////Allowed Digits: 0 to 1
//int d = 0b1111; // //Prefix with 0b
//Console.WriteLine($"Decimal Literal: {a}");
//Console.WriteLine($"Hexa-Decimal Literal: {c}");
//Console.WriteLine($"Binary Literal: {d}");
//Console.WriteLine("");

////It will make Beep Sound
//Console.Beep();

//Console.ReadKey();

using System.Numerics;
using System.Reflection.Metadata.Ecma335;

//class Program
//{
//    static void Main(string[] args)
//    {
//        Employee employee = new Employee();
//        employee.Display();

//        Console.ReadKey()
//    }
//}
//class Employee
//{
//    int age, ID;
//    string name;
//    string email;

//    public Employee()
//    {
//        age = 23;
//        ID = 37834745;
//        name = "Denis Kiprotich";
//        email = "denis.k.too@gmail.com";
//    }
//    public void Display()
//    {
//        Console.WriteLine($"My name is {name}, ID Number {ID}, email address {email} and I'm {age} years old.");
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        CopyConstructor obj1 = new CopyConstructor(10);
//        obj1.Display();
//        CopyConstructor obj2 = new CopyConstructor(obj1);
//        obj2.Display();
//        Console.ReadKey();
//    }
//}

//public class CopyConstructor
//{
//    int x;

//    //Parameterized Constructor
//    public CopyConstructor(int i)
//    {
//        x = i;
//    }

//    //Copy Constructor
//    public CopyConstructor(CopyConstructor obj)
//    {
//        x = obj.x;
//    }

//    public void Display()
//    {
//        Console.WriteLine($"Value of X = {x}");
//    }
//}

class Example
{
    int i;
    static int j;

    //Default Constructor
    public Example()
    {
        Console.WriteLine("Default Constructor Executed");
        i = 100;
    }

    //static Constructor
    static Example()
    {
        Console.WriteLine("Static Constructor Executed");
        j = 100;
    }
    public void Increment()
    {
        i++;
        j++;
    }
    public void Display()
    {
        Console.WriteLine("Value of i : " + i);
        Console.WriteLine("Value of j : " + j);
    }
}
class Test
{
    static void Main(string[] args)
    {
        Example e1 = new Example();
        e1.Increment();
        e1.Display();
        e1.Increment();
        e1.Display();
        Example e2 = new Example();
        e2.Increment();
        e2.Display();
        e2.Increment();
        e2.Display();
        Console.ReadKey();
    }
}