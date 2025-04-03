using System.Numerics;
namespace Lab01_3rd;
public class Point
{
    private int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }
}
public class Figure
{
    private Point[] points;
    public string Name // Автосвойство
    {
        get;
        set;
    }
    public Figure(Point p1, Point p2, Point p3) // Стандартный конструктор
    {
        points = new Point[3];
        points[0] = p1;
        points[1] = p2;
        points[2] = p3;
        Name = "Треугольник";
    }
    public Figure(Point p1, Point p2, Point p3, Point p4) // Синтаксис кортежа
    {   
        Array.Resize(ref points,4);
        (points[0], points[1], points[2], points[3]) = (p1, p2, p3, p4);
        Name = "Четырехугольник";

    }
    
    public Figure(Point p1, Point p2, Point p3, Point p4, Point p5) : this(p1, p2, p3, p4)
    {
        Array.Resize (ref points,5);
        points[4] = p5;
        Name = "Пятиугольник";
    }

    public double LengthSide(Point A, Point B)  // Метод для вычисления длины стороны между двумя точками
    {
        int dx = A.X - B.X;
        int dy = A.Y - B.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    public double PerimeterCalculator() // Метод для вычисления периметра фигуры
    {
        double sum = 0;
        for (int i = 0; i < points.Length; i++)
        {
            sum += LengthSide(points[i], points[(i+1)%points.Length]);
        }
        return sum;
    }
}
class Program
{
    static void Main(string[] args)
    {

        bool flg = false;
        int numberVertices = 0;
        while (!flg)
        {
            Console.Write("Введите количество вершин многоугольника: ");
            numberVertices = Convert.ToInt32(Console.ReadLine());
            if (numberVertices < 3 || numberVertices > 5)
            {
                Console.WriteLine("Введено неверное число вершин!");
            }
            else flg = true;
        }
        List<Point> vertices = new List<Point>(numberVertices);
        for (int i = 0; i < numberVertices; i++) {
            Console.Write($"Введите координаты {i + 1} точки через пробел: ");
            string[] input = Console.ReadLine().Split();
            int pointX = int.Parse(input[0]);
            int pointY = int.Parse(input[1]);
            Point point = new Point(pointX, pointY);
            vertices.Add(point);
        }
        if (vertices.Count == 3)
        {
            Figure figure = new Figure(vertices[0], vertices[1], vertices[2]);
            Console.WriteLine($"Фигура {figure.Name} имеет периметр {figure.PerimeterCalculator()}");
        }
        else if (vertices.Count == 4)
        {
            Figure figure = new Figure(vertices[0], vertices[1], vertices[2], vertices[3]);
            Console.WriteLine($"Фигура {figure.Name} имеет периметр {figure.PerimeterCalculator()}");
        }
        else if (vertices.Count == 5)
        {
            Figure figure = new Figure(vertices[0], vertices[1], vertices[2], vertices[3], vertices[4]);
            Console.WriteLine($"Фигура {figure.Name} имеет периметр {figure.PerimeterCalculator()}");
        }

    }
}