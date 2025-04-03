class Vehicle
{
    private double x;
    private double y;
    private double price;
    private double speed;
    private int year;
    
    public Vehicle(double x, double y, double price, double speed, int year)
    {
        this.x = x;
        this.y = y;
        this.price = price;
        this.speed = speed;
        this.year = year;
    }
    public void ShowInfo()
    {
        Console.WriteLine("Vehicle information:");
        Console.WriteLine($"Coordinates: ({x}, {y})");
        Console.WriteLine($"Price: {price}");
        Console.WriteLine($"Speed: {speed}");
        Console.WriteLine($"Year: {year}");
    }

}
class Plane : Vehicle
{
    private double height;
    private int passengerQuantity;
    public Plane(double x, double y, double price, double speed, int year,
        double height, int passengerQuantity) : base(x, y, price, speed, year)
    {
        this.passengerQuantity = passengerQuantity;
        this.height = height;
    }
    public void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Height: {height}");
        Console.WriteLine($"Quantity of passengers: {passengerQuantity}");
    }
}
class Car : Vehicle
{
    public Car(double x, double y, double price, double speed, int year) : base(x, y, price, speed, year)
    {
    }
    public void ShowInfo()
    {
        base.ShowInfo();
    }
}
class Ship : Vehicle
{
    private int passengerQuantity;
    private string port;
    public Ship(double x, double y, double price, double speed, int year, int passengerQuantity, string port)
    : base(x, y, price, speed, year)
    {
        this.passengerQuantity = passengerQuantity;
        this.port = port;
    }
    public void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Quantity of passengers: {passengerQuantity}");
        Console.WriteLine($"Port: {port}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Vehicle vehicle = new Vehicle(46.21, 25.7, 3000, 175, 1991);
        vehicle.ShowInfo();
        Console.WriteLine();
        Plane plane = new Plane(300, 436, 50000, 700, 2006,7650,256);
        plane.ShowInfo();
        Console.WriteLine();
        Car car = new Car(143, 0, 500, 90, 2002);
        car.ShowInfo();
        Console.WriteLine();
        Ship ship = new Ship(54, 78, 600, 120, 1996, 178, "Portland");
        ship.ShowInfo();
        Console.WriteLine();
    }
}