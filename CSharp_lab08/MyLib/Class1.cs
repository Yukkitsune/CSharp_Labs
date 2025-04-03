using System.Xml.Serialization;

namespace MyLib
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommentAtt : Attribute
    {
        public string Comment { get; set; }
        public CommentAtt(string comment)
        {
            Comment = comment;
        }
    }
    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }
    public enum eFavouriteFood
    {
        Meat,
        Plants,
        Everything
    }
    [CommentAtt("Абстрактный класс для объектов, представляющих животных")]
    [XmlInclude(typeof(Cow))]
    [XmlInclude(typeof(Lion))]
    [XmlInclude(typeof(Pig))]
    public abstract class Animal
    {
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }
        public string WhatAnimal { get; set; }

        protected Animal() { }
        public Animal(string country, bool hideFromAnimals, string name, string whatAnimal)
        {
            (Country, HideFromOtherAnimals, Name, WhatAnimal) = (country, hideFromAnimals, name, whatAnimal);
        }

        public abstract eClassificationAnimal GetClassificationAnimal();
        public abstract eFavouriteFood GetFavouriteFood();
        public abstract string SayHello();
        public void Deconstruct(out string country, out bool hideFromOtherAnimals, out string name, out string whatAnimal)
        {
            country = Country;
            hideFromOtherAnimals = HideFromOtherAnimals;
            name = Name;
            whatAnimal = WhatAnimal;
        }
    }
    [CommentAtt("Класс описания коровы")]
    public class Cow : Animal
    {
        public Cow() { }
        public Cow(string country, bool hideFromOtherAnimals, string name, string whatAnimal) :
            base(country, hideFromOtherAnimals, name, whatAnimal)
        { }
        public override eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Plants;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Herbivores;
        }
        public override string SayHello()
        {
            return "Moo";
        }
    }
    [CommentAtt("Класс описания льва")]
    public class Lion : Animal
    {
        public Lion() { }
        public Lion(string country, bool hideFromOtherAnimals, string name, string whatAnimal) :
            base(country, hideFromOtherAnimals, name, whatAnimal)
        { }
        public override eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Meat;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Omnivores;
        }
        public override string SayHello()
        {
            return "Meow";
        }
    }
    [CommentAtt("Класс описания свиньи")]
    public class Pig : Animal
    {
        public Pig() { }
        public Pig(string country, bool hideFromOtherAnimals, string name, string whatAnimal) :
            base(country, hideFromOtherAnimals, name, whatAnimal)
        { }
        public override eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Everything;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Omnivores;
        }
        public override string SayHello()
        {
            return "Oink";
        }
    }
}

