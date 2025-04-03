class Car : IEquatable<Car>
{
    public string Name { get; set; }
    public string Engine { get; set; }
    public double MaxSpeed{get;set; }

    public Car(string Name, string Engine, double MaxSpeed)
    {
        (this.Name, this.Engine, this.MaxSpeed)=(Name, Engine, MaxSpeed);
    }
    public override string ToString()
    {
        return Name;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return Equals(obj as Car);
    }
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + Name.GetHashCode();
        hash = hash * 23 + Engine.GetHashCode();
        hash = hash * 23 + MaxSpeed.GetHashCode();
        return hash;
    }
    // Реализация IEquatable<Car> для сравнения объектов по значению
    public bool Equals(Car? other)
    {
        if (other == null) return false;
        return Name == other.Name && Engine == other.Engine && MaxSpeed == other.MaxSpeed;
    }
}
class CarsCatalog
{
    List<Car> cars = new List<Car>();

    public void AddCar(Car car)
    {
        cars.Add(car);
    }
    public List<Car> GetCars() { return cars; }
    public string this[int index]
    {
        get {
            if (index < 0 || index >= cars.Count) return "Введен неверный индекс";
            return $"Название: {cars[index].Name}, Двигатель: {cars[index].Engine}";
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car("Audi A4", "Gasoline", 240);
        Car car2 = new Car("Kia Picanto", "Hybrid", 280);
        Car car3 = new Car("Tesla Model 3", "Electric", 230);
        Car car4 = new Car("Kia Picanto", "Hybrid", 280);

        CarsCatalog catalog = new CarsCatalog();
        catalog.AddCar(car1);
        catalog.AddCar(car2);
        catalog.AddCar(car3);
        Console.WriteLine(catalog[0]);
        Console.WriteLine(catalog[1]);
        Console.WriteLine(catalog[2]);

        Console.WriteLine(car1.Equals(car2));
        Console.WriteLine(car2.Equals(car4));

    }
}