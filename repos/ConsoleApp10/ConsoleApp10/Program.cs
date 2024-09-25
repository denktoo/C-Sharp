class Program
{
    static void Main(string[] args)
    {
        Rectangle rec = new Rectangle();
        Square square = new Square();
        Circle circle = new Circle();

        rec.width = 15;
        rec.height = 18;
        square.width = 7;
        circle.radius = 9;

        rec.Area();
        rec.Perimeter();
        
        square.Area();
        square.Perimeter();

        circle.Area();
        circle.Perimeter();

        Console.ReadKey();
    }
}

class Shape
{
    public virtual void Area()
    {
        Console.WriteLine("Calculating the Area...");
    }

    public virtual void Perimeter()
    {
        Console.WriteLine("Calculating the Perimeter...");
    }
}

class Rectangle : Shape
{
    public int width { get; set; }
    public int height { get; set; }

    public override void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {width * height}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Rectangle is {2 * (width + height)}");
    }
}

class Square : Shape
{
    public int width { get; set; }
    
    public override void Area()
    {
        Console.WriteLine($"The Area of the Square is {width * width}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Square is {4 * width}");
    }
}

class Circle : Shape
{
    const double pi = Math.PI;
    public int radius { get; set; }
    
    public override void Area()
    {
        Console.WriteLine($"The area of the Circle is: {pi * radius * radius}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Circle is: {pi * (radius + radius)}");
    }
}
