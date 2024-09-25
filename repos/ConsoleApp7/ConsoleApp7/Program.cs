class Program
{
    static void Main(string[] args)
    {
        Rectangle rectangle = new Rectangle(10, 6);
        Square square = new Square(7);
        Circle circle = new Circle(7);
        
        rectangle.Area();
        rectangle.Perimeter();
        
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
        Console.WriteLine("Finding the Area");
    }
    public virtual void Perimeter()
    {
        Console.WriteLine("Finding the Perimeter");
    }
}

class Rectangle : Shape
{
    int length, width;
    public Rectangle(int l, int w)
    {
        this.length = l;
        this.width = w;
    }
    public override void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {length * width}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Rectangle is {2 * (length + width)}");
    }
}

class Square : Shape
{
    int length;
    public Square(int l)
    {
        this.length = l;
    }
    public override void Area()
    {
        Console.WriteLine($"The Area of the Square is {length * length}");
    }
    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Square is {4 * length}");
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
