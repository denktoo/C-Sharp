class Program
{
    static void Main(string[] args)
    {
        //Creating the Employee instance
        Employee emp = new Employee(101, "Pranaya", "SSE", 10000, "Mumbai", "IT", "Male");
        //Accessing Employee Properties using Indexers i.e. using Index positions
        Console.WriteLine($"EID = {emp[0]}");
        Console.WriteLine($"Name = {emp[1]}");
        Console.WriteLine($"Job = {emp[2]}");
        Console.WriteLine($"Salary = {emp[3]}");
        Console.WriteLine($"Location = {emp[4]}");
        Console.WriteLine($"Department = {emp[5]}");
        Console.WriteLine($"Gender = {emp[6]}");

        Console.ReadKey();
    }
}

class Employee
{
    //Declare the Properties
    public int ID { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public double Salary { get; set; }
    public string Location { get; set; }
    public string Department { get; set; }
    public string Gender { get; set; }

    //Initialize the Properties through Constructor
    public Employee(int ID, string Name, string Job, int Salary, string Location,
                    string Department, string Gender)
    {
        this.ID = ID;
        this.Name = Name;
        this.Job = Job;
        this.Salary = Salary;
        this.Location = Location;
        this.Department = Department;
        this.Gender = Gender;
    }
    //Creating the Indexer using Integer Index Position
    public object this[int index]
    {
        //Get accessor is used for returning a value
        get
        {
            //based in the index position, return the appropriate property value
            if (index == 0)
                return ID;
            else if (index == 1)
                return Name;
            else if (index == 2)
                return Job;
            else if (index == 3)
                return Salary;
            else if (index == 4)
                return Location;
            else if (index == 5)
                return Department;
            else if (index == 6)
                return Gender;
            else
                return null;
        }
        //Set accessor is used to assigning a value
        set
        {
            //based in the index position, set the appropriate property value
            if (index == 0)
                ID = Convert.ToInt32(value);
            else if (index == 1)
                Name = value.ToString();
            else if (index == 2)
                Job = value.ToString();
            else if (index == 3)
                Salary = Convert.ToDouble(value);
            else if (index == 4)
                Location = value.ToString();
            else if (index == 5)
                Department = value.ToString();
            else if (index == 6)
                Gender = value.ToString();
        }
    }
}
