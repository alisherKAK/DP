using System;

namespace Decorator.Concrete._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Coffee coffee1 = new Capuchino();
            coffee1 = new Sugar(coffee1); // капучино с сахаром
            Console.WriteLine("Название: {0}", coffee1.Name);
            Console.WriteLine("Цена: {0}", coffee1.GetCost());

            Coffee coffee2 = new Capuchino();
            coffee2 = new Cinnamon(coffee2);// капучино с корицей
            Console.WriteLine("Название: {0}", coffee2.Name);
            Console.WriteLine("Цена: {0}", coffee2.GetCost());

            Coffee coffee3 = new Latte();
            coffee3 = new Sugar(coffee3);
            coffee3 = new Cinnamon(coffee3);// латте с корицей и сахаром
            Console.WriteLine("Название: {0}", coffee3.Name);
            Console.WriteLine("Цена: {0}", coffee3.GetCost());

            Console.ReadLine();
        }
    }

    abstract class Coffee
    {
        public Coffee(string n)
        {
            this.Name = n;
        }
        public string Name { get; protected set; }
        public abstract int GetCost();
    }

    class Capuchino : Coffee
    {
        public Capuchino() : base("Капучино")
        { }
        public override int GetCost()
        {
            return 110;
        }
    }
    class Latte : Coffee
    {
        public Latte()
        : base("Латте")
        { }
        public override int GetCost()
        {
            return 90;
        }
    }

    abstract class Additions : Coffee // decorator
    {
        protected Coffee coffee;
        public Additions(string n, Coffee coffee) : base(n)
        {
            this.coffee = coffee;
        }
    }

    class Sugar : Additions
    {
        public Sugar(Coffee p)
        : base(p.Name + ", с сахаром", p)
        { }

        public override int GetCost()
        {
            return coffee.GetCost() + 40;
        }
    }

    class Cinnamon : Additions
    {
        public Cinnamon(Coffee p)
        : base(p.Name + ", с корицей", p)
        { }

        public override int GetCost()
        {
            return coffee.GetCost() + 60;
        }
    }
}
