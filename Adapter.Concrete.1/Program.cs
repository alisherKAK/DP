using System;

namespace Adapter.Concrete._1
{

    public interface IPerson
    {
        string Name { get; set; }
    }

    public interface IFrenchPerson
    {
        string Nom { get; set; }
    }

    public class Person : IPerson
    {
        public string Name { get; set; }
    }

    public class FrenchPerson : IFrenchPerson
    {
        public string Nom { get; set; }
    }

    public class PersonService
    {
        public void PrintName(IPerson person)
        {
            Console.WriteLine("Hello, " + person.Name + "!");
        }
    }

    public class FrenchPersonAdapter : IPerson
    {
        private readonly IFrenchPerson frenchPerson;

        public FrenchPersonAdapter(IFrenchPerson frenchPerson)
        {
            this.frenchPerson = frenchPerson;
        }

        public string Name
        {
            get { return frenchPerson.Nom; }
            set { frenchPerson.Nom = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var service = new PersonService();
            var person = new Person
            {
                Name = "Nick"
            };

            var frenchPerson = new FrenchPerson
            {
                Nom = "Nikolya"
            };

            service.PrintName(person);
            service.PrintName(new FrenchPersonAdapter(frenchPerson));
        }
    }
}
