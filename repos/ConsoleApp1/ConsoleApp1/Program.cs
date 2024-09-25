//// Create a list of integers by using a collection initializer.
//List<int> numbers = [2, 6, 7, 1, 2, 9, 7, 12, 9, 3, 0];

//foreach (var n in numbers)
//{
//    Console.Write(n + " ");
//}
//Console.WriteLine();

//for (int i = numbers.Count - 1; i >= 0; i--)
//{
//    Console.Write(numbers[i] + " ");
//}
//Console.WriteLine();

//numbers.ForEach(n => Console.Write(n + " "));

class Program
{
    static void Main(string[] args)
    {
        //Create a collection to store list of employees
        List<Employee> empList = new List<Employee>
        {
            new Employee() { Name = "Anurag", Gender = "Male" },
            new Employee() { Name = "Pranaya", Gender = "Female" },
            new Employee() { Name = "Priyanka", Gender = "Female" },
            new Employee() { Name = "Sambit", Gender = "Male" }
        };

        //Loop through Each Employees and Print the Name and Gender
        foreach (Employee emp in empList)
        {
            Console.WriteLine($"Name: {emp.Name}, Gender: {emp.Gender}.");
        }

        Console.ReadKey();
    }
}
public class Employee
{
    public string Name { get; set; }
    public string Gender { get; set; }
}
