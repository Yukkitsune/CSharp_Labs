using System.Diagnostics;

 public class Rectangle
{
    private double sideA;
    private double sideB;

    public Rectangle(double sideA, double sideB)
    {
        this.sideA = sideA;
        this.sideB = sideB; 
    }
    public double getSideA
    {
        get
        {
            return sideA;
        }
    }
    public double getSideB
    {
        get
        {
            return sideB;
        }
    }
    private double CalculateArea()
    {
        return sideA * sideB;
    }
    private double CalculatePerimeter()
    {
        return 2 * (sideA + sideB);
    }

    public double Area
    {
        get {
            return CalculateArea();
        }
    }
    public double Perimeter
    {
        get
        {
            return CalculatePerimeter();
        }
    }
}
class Program
{
    static void Main()
    {
        Console.Write("Введите длину первой стороны прямоугольника: ");
        double sideA = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите длину второй стороны прямоугольника: ");
        double sideB = Convert.ToDouble(Console.ReadLine());
        Rectangle rectangle = new Rectangle(sideA, sideB);
        Console.WriteLine($"Стороны прямоугольника равны {rectangle.getSideA} и {rectangle.getSideB}");
        Console.WriteLine($"Периметр прямоугольника равен {rectangle.Perimeter}");
        Console.WriteLine($"Площадь прямоугольника равна {rectangle.Area}");
    }
}
