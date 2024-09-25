class Program
{
    static void Main(string[] args)
    {
        Shape rec = new Rectangle(10, 6);
        Shape sq = new Square(5);
        rec.Area();
        rec.Perimeter();
        sq.Area();
        sq.Perimeter();

        Console.ReadKey();
    }
}

class Shape
{
    public virtual void Area()
    {
        Console.WriteLine("Calculating the Area of The Shape");
    }
    public virtual void Perimeter()
    {
        Console.WriteLine("Calculating the Perimeter of the Shape");
    }
}

class Rectangle : Shape
{
    private int l, w;
    public Rectangle(int l, int w)
    {
        this.l = l;
        this.w = w;
    }

    public override void Area()
    {
        Console.WriteLine($"The Area of the Rectangle is {l * w}");
    }

    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Rectangle is: {2 * (l + w)}");
    }
}

class Square : Shape
{
    private int l;
    public Square(int l)
    {
        this.l = l;
    }

    public override void Area()
    {
        Console.WriteLine($"The Area of the Square is {l * l}");
    }

    public override void Perimeter()
    {
        Console.WriteLine($"The Perimeter of the Square is {4 * l}");
    }
}