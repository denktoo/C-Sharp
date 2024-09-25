class Program
{
    static void Main(string[] args)
    {
        Rectangle rectangle = new Rectangle();
        Square square = new Square();
        rectangle.Area();
        rectangle.Perimeter();
        square.Area();
        square.Perimeter();
        


        Console.ReadKey();
    }
}

class Rectangle
{
    private int l, w;
    private string length, width;
    public Rectangle()
    {
        Console.Write("The Length of the Rectagle is: ");
        length = Console.ReadLine();
        l = int.Parse(length);
        Console.Write("The Width of the Rectangle is: ");
        width = Console.ReadLine();
        w = int.Parse(width);
    }

    public void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {l * w}");
    }

    public void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Rectangle is: {2 * (l + w)}");
    }
}

class Square
{
    private int l;
    private string length;
    public Square()
    {
        Console.Write("The Lenght of the Square is: ");
        length = Console.ReadLine();
        l = int.Parse(length);
    }

    public void Area()
    {
        Console.WriteLine($"The Area of the Square is {l * l}");
    }

    public void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Square is {4 * l}");
    }
}
