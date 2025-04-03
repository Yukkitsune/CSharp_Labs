using MyLib;
using System.Xml.Serialization;

namespace CSharp_lab08
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Animal[] animals = [
                new Cow("Russia", false, "Borya", "Cow"),
            new Lion("Zambia", false, "Tom", "Lion"),
            new Pig("Germany", true, "Peppa", "Pig")
            ];
            XmlSerializer xmlSerializer = new(typeof(Animal[]));
            using (FileStream fs = new("animals.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, animals);
            }
            using (FileStream fs = new("animals.xml", FileMode.OpenOrCreate))
            {
                Animal?[]? newAnimals = xmlSerializer.Deserialize(fs) as Animal[];
                if (newAnimals != null)
                {
                    foreach (Animal? animal in newAnimals)
                    {
                        Console.WriteLine($"Country: {animal?.Country}");
                        Console.WriteLine($"Hide from other animals: {animal?.HideFromOtherAnimals}");
                        Console.WriteLine($"Name: {animal?.Name}");
                        Console.WriteLine($"What animal: {animal?.WhatAnimal}");
                        Console.WriteLine("----------");
                    }
                }
            }
        }
    }
}