class Program
{
    static void Main(string[] args)
    {
        IterateThruDictionary();
    }

    private static void IterateThruDictionary()
    {
        Dictionary<string, Employee> employee = BuildDictionary();

        foreach (KeyValuePair<string, Employee> em in employee)
        {
            Employee emp = em.Value;

            Console.WriteLine($"Key: {em.Key}");
            Console.WriteLine($"Values: {emp.Age}, {emp.Gender}");
        }
    }

    private static Dictionary<string, Employee> BuildDictionary() =>
    new()
    {
        {"Sambit", new Employee() { Name = "Sambit", Age = 30, Gender = "Male" }},
        {"Priyanka", new Employee() { Name = "Priyanka", Age = 40, Gender = "Female" }},
        {"Pranaya", new Employee() { Name = "Pranaya", Age = 34, Gender = "Male" }},
        {"Anurag", new Employee() { Name = "Anurag", Age = 38, Gender = "Female" }}
    };

}
public class Employee
{
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Gender { get; init; }
}