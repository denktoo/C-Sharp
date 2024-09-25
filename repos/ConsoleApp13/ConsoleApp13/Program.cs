using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        ShowLINQ();
    }

    private static void ShowLINQ()
    {
        List<Employee> employee = BuildList();

        // LINQ Query.
        var subset = from emp in employee
                     where emp.Age > 20
                     orderby emp.Name
                     select emp;

        foreach (Employee emp in subset)
        {
            Console.WriteLine(emp.Name + " " + emp.Age);
        }
    }

    private static List<Employee> BuildList() => new()
        {
            { new Employee() { Name = "Sambit", Age = 30, Gender = "Male" } },
            { new Employee() { Name = "Priyanka", Age = 40, Gender = "Female" }},
            { new Employee() { Name = "Pranaya", Age = 34, Gender = "Male" }},
            { new Employee() { Name = "Anurag", Age = 38, Gender = "Female" }}
        };

}
public class Employee
{
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Gender { get; init; }
}