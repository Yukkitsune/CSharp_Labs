using MyLib;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace ReflectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Укажем путь к сборке библиотеки классов
            string assemblyPath = "MyLib.dll";
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            // Создадим корневой xml элемент
            XElement xmlRoot = new XElement("AnimalLib");
            // Перебираем все классы в сборке с помощью рефлексии
            foreach (Type type in assembly.GetTypes())
            {
                XElement classElement = new XElement("Class", new XAttribute("Name", type.Name));
                // Пользовательские атрибуты класса, если они есть
                var classAtributes = type.GetCustomAttributes(typeof(CommentAtt), false);
                foreach (CommentAtt attr in classAtributes)
                {
                    classElement.Add(new XElement("Comment", attr.Comment));
                }
                foreach (PropertyInfo property in type.GetProperties())
                {
                    XElement propertyElement = new XElement("Property",
                        new XAttribute("Name", property.Name),
                        new XAttribute("Type", property.PropertyType.Name));
                    classElement.Add(propertyElement);
                }
                //Все методы класса
                foreach (MethodInfo method in type.GetMethods())
                {
                    XElement methodElement = new XElement("Method",
                        new XAttribute("Name", method.Name),
                        new XAttribute("ReturnType", method.ReturnType.Name));
                    // Если метод принимает параметры, добавляем информацию о каждом параметре
                    foreach (ParameterInfo param in method.GetParameters())
                    {
                        XElement paramElement = new XElement("Parameter",
                            new XAttribute("Name", param.Name),
                            new XAttribute("Type", param.ParameterType.Name));
                        methodElement.Add(paramElement);
                    }
                    classElement.Add(methodElement);
                }
                if (type.IsEnum)
                {
                    foreach (var enumValue in Enum.GetValues(type))
                    {
                        XElement enumElement = new XElement("EnumValue",
                            new XAttribute("Name", enumValue.ToString()),
                            new XAttribute("Value", ((int)enumValue).ToString()));
                        classElement.Add(enumElement);
                    }
                }
                xmlRoot.Add(classElement);
            }
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), xmlRoot);
            xmlDocument.Save("LibStructure.xml");
            Console.WriteLine("File generated succesful");
        }
    }
}