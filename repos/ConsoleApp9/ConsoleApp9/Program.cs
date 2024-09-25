class Program
{
    static void Main(string[] args)
    {
        //Create a collection to store list of employees
        List<Employee> empList = new List<Employee>
        {
            new Employee() { Name = "Anurag", Gender = 0 },
            new Employee() { Name = "Pranaya", Gender = 1 },
            new Employee() { Name = "Priyanka", Gender = 2 },
            new Employee() { Name = "Sambit", Gender = 3 }
        };

        //Loop through Each Employees and Print the Name and Gender
        foreach (var emp in empList)
        {
            //To Print the Actual Gender of the Employee, 
            //we need to call the GetGender Method by passing the Integer Gender Value
            Console.WriteLine($"Name is {emp.Name} and Gender is {GetGender(emp.Gender)}");
        }

        Console.ReadKey();
    }

    //This Method is used to return the Actual Gender Based on the Enum Gender Value
    public static string GetGender(int gender)
    {
        // The switch case here is now more readable and maintainable because 
        // of replacing the integral numbers with Gender Enum
        switch (gender)
        {
            case (int)Gender.Unknown:
                return "Unknown";
            case (int)Gender.Male:
                return "Male";
            case (int)Gender.Female:
                return "Female";
            default:
                return "Invalid Data for Gender";
        }
    }
}

public class Employee
{
    public string Name { get; set; }

    // 0 - Unknown
    // 1 - Male
    // 2 - Female
    public int Gender { get; set; }
}

public enum Gender
{
    Unknown,
    Male,
    Female
}
