class Program
{
    static void Main(string[] args)
    {
        Shape rectangle = new Rectangle(10, 6);
        Shape square = new Square(6);
        Shape circle = new Circle(7);
        
        rectangle.Area();
        rectangle.Perimeter();
        
        square.Area();
        square.Perimeter();
        
        circle.Area();
        circle.Perimeter();
        
        Console.ReadKey();
    }
}

public abstract class Shape
{
    public abstract void Area();

    public abstract void Perimeter();
}

public class Rectangle : Shape
{
    int x, y;

    public Rectangle(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public override void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {x * y}");
    }
    public override void Perimeter()
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
    public override void Area()
    {
        Console.WriteLine($"The Area of Square is {x * x}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of Square is {4 * x}");
    }
}

class Circle : Shape
{
    const double pi = Math.PI;
    int radius;
    public Circle(int r)
    {
        this.radius = r;
    }
    public override void Area()
    {
        Console.WriteLine($"The area of the Circle is: {pi * radius * radius}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Circle is: {pi * (radius + radius)}");
    }
}
