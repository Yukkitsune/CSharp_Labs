class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public double MaxSpeed { get; set; }
    
    public Car(string name,int productionYear, double maxSpeed)
    {
        (Name,ProductionYear,MaxSpeed) = (name,productionYear,maxSpeed);
    }
    public override string ToString()
    {
        return $"{Name},{ProductionYear},{MaxSpeed}";
    }
}
class CarComparer : IComparer<Car>
{
    private string sortBy;
    public CarComparer(string sortBy)
    {
        this.sortBy = sortBy;
    }
    public int Compare(Car? car1, Car? car2)
    {
        if (car1 == null || car2 == null)
        {
            throw new ArgumentNullException("Car object is null");
        }
        switch (sortBy) {
            case "Name":
                return string.Compare(car1.Name, car2.Name);
            case "ProductionYear":
                return car1.ProductionYear.CompareTo(car2.ProductionYear);
            case "MaxSpeed":
                return car1.MaxSpeed.CompareTo(car2.MaxSpeed);
            default:
                throw new ArgumentException("Неверный выбор сортировки");
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Car[] cars =
        {
            new Car("Tesla", 2014, 240),
            new Car("Audi A8", 2019, 268),
            new Car("Kia", 2012, 225)
        };
        Console.WriteLine("Изначальный массив:");
        foreach (var car in cars) Console.WriteLine(car.ToString());

        Array.Sort(cars, new CarComparer("Name"));
        Console.WriteLine("\nСортировка по названию:");
        foreach (var car in cars) Console.WriteLine(car.ToString());

        Array.Sort(cars, new CarComparer("ProductionYear"));
        Console.WriteLine("\nСортировка по году выпуска:");
        foreach (var car in cars) Console.WriteLine(car.ToString());

        Array.Sort(cars, new CarComparer("MaxSpeed"));
        Console.WriteLine("\nСортировка по максимальной скорости:");
        foreach (var car in cars) Console.WriteLine(car.ToString());
    }
}