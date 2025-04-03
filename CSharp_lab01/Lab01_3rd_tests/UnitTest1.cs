using NUnit.Framework;
using Lab01_3rd;
namespace Lab01_cs_3rd;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests
{
    [Test]
    public void PointConstructor_CorrectInitialization()
    {
        int x = 3; int y = 4;
        Point point = new Point(x, y);
        Assert.AreEqual(x, point.X);
        Assert.AreEqual(y, point.Y);
    }
    [Test]
    public void FigureConstructor_ThreePoints()
    {
        Point p1 = new Point(1, 2);
        Point p2 = new Point(2, 3);
        Point p3 = new Point(3, 4);

        Figure triangle = new Figure(p1, p2, p3);

        Assert.AreEqual("Треугольник", triangle.Name);
    }
    [Test]
    public void FigureConstructor_FourPoints() {
        Point p1 = new Point(1, 2);
        Point p2 = new Point(2, 3);
        Point p3 = new Point(3, 4);
        Point p4 = new Point(4, 5);

        Figure quadrilateral = new Figure(p1, p2, p3, p4);

        Assert.AreEqual("Четырехугольник", quadrilateral.Name);
    }
    [Test]
    public void FigureConstructor_FivePoints() {
        Point p1 = new Point(1, 2);
        Point p2 = new Point(2, 3);
        Point p3 = new Point(3, 4);
        Point p4 = new Point(4, 5);
        Point p5 = new Point(5, 6);

        Figure pentagon = new Figure(p1, p2, p3, p4, p5);

        Assert.AreEqual("Пятиугольник", pentagon.Name);
    }
    [Test]
    public void LengthSide_CorrectDistance()
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(1, 1);
        Point p3 = new Point(1, 0);
        Figure figure = new Figure(p1, p2, p3);

        double length = figure.LengthSide(p1, p2);

        Assert.AreEqual(Math.Sqrt(2), length, 0.001); 
    }
    [Test]
    public void PerimeterCalculator_CorrectPerimeter()
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(0, 4);
        Point p3 = new Point(3, 0);
        Figure triangle = new Figure(p1, p2, p3);

        double perimeter = triangle.PerimeterCalculator();

        Assert.AreEqual(12.0, perimeter, 0.001);
    }
}
