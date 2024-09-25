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