class Program
{
    static void Main(string[] args)
    {
        Shape rectangle = new Rectangle(10, 6);
        Shape square = new Square(6);
        rectangle.Area();
        rectangle.Perimeter();
        square.Area();
        square.Perimeter();

        Console.ReadKey();
    }
}

public interface Shape
{
    void Area();

    void Perimeter();
}

public class Rectangle : Shape
{
    int x, y;
     
    public Rectangle(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {x * y}");
    }
    public void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Rectangle is {2 * (x + y)}");
    }
}

public class Square : Shape
{
    int x;
    public Square(int x)
    {
        this.x = x;
    }
    public void Area()
    {
        Console.WriteLine($"The Area of Square is {x * x}");
    }
    public void Perimeter()
    {
        Console.WriteLine($"The Perimeter of Square is {4 * x}");
    }
}
