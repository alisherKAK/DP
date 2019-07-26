// Паттерн Строитель
//
// Назначение: Позволяет создавать сложные объекты пошагово. Строитель даёт
// возможность использовать один и тот же код строительства для получения разных
// представлений объектов.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
    // Интерфейс Строителя объявляет создающие методы для различных частей
    // объектов Продуктов.
    public interface IVendor
    {
        void BuildBody();

        void BuildBatteryPlus();

        void BuildCover();
    }

    // Классы Конкретного Строителя следуют интерфейсу Строителя и
    // предоставляют конкретные реализации шагов построения. Ваша программа
    // может иметь несколько вариантов Строителей, реализованных по-разному.
    public class Samsung : IVendor
    {
        private Telephone _telephone = new Telephone();

        // Новый экземпляр строителя должен содержать пустой объект
        // продукта, который используется в дальнейшей сборке.
        public Samsung()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._telephone = new Telephone();
        }

        // Все этапы производства работают с одним и тем же экземпляром
        // продукта.
        public void BuildBody()
        {
            this._telephone.Add("Samsung's body");
        }

        public void BuildBatteryPlus()
        {
            this._telephone.Add("Samsung's battery plus");
        }

        public void BuildCover()
        {
            this._telephone.Add("Samsung's cover");
        }

        // Конкретные Строители должны предоставить свои собственные методы
        // получения результатов. Это связано с тем, что различные типы
        // строителей могут создавать совершенно разные продукты с разными
        // интерфейсами. Поэтому такие методы не могут быть объявлены в базовом
        // интерфейсе Строителя (по крайней мере, в статически типизированном
        // языке программирования).
        //
        // Как правило, после возвращения конечного результата клиенту,
        // экземпляр строителя должен быть готов к началу производства
        // следующего продукта. Поэтому обычной практикой является вызов метода
        // сброса в конце тела метода GetProduct. Однако такое поведение не
        // является обязательным, вы можете заставить своих строителей ждать
        // явного запроса на сброс из кода клиента, прежде чем избавиться от
        // предыдущего результата.
        public Telephone GetProduct()
        {
            Telephone result = this._telephone;

            this.Reset();

            return result;
        }
    }

    // Имеет смысл использовать паттерн Строитель только тогда, когда ваши
    // продукты достаточно сложны и требуют обширной конфигурации.
    //
    // В отличие от других порождающих паттернов, различные конкретные строители
    // могут производить несвязанные продукты. Другими словами, результаты
    // различных строителей  могут не всегда следовать одному и тому же
    // интерфейсу.
    public class Telephone
    {
        private List<object> _parts = new List<object>();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Product parts: " + str + "\n";
        }
    }

    // Директор отвечает только за выполнение шагов построения в
    // определённой последовательности. Это полезно при производстве продуктов в
    // определённом порядке или особой конфигурации. Строго говоря, класс
    // Директор необязателен, так как клиент может напрямую управлять
    // строителями.
    public class Director
    {
        private IVendor _builder;

        public IVendor Builder
        {
            set { _builder = value; }
        }

        // Директор может строить несколько вариаций продукта, используя
        // одинаковые шаги построения.
        public void buildBasic()
        {
            this._builder.BuildBody();
        }

        public void buildStandard()
        {
            this._builder.BuildBody();
            this._builder.BuildBatteryPlus();
        }

        public void buildLux()
        {
            this._builder.BuildBody();
            this._builder.BuildBatteryPlus();
            this._builder.BuildCover();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            Samsung builder = new Samsung();
            director.Builder = builder;

            Console.WriteLine("Basic product:");
            director.buildBasic();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard product:");
            director.buildStandard();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Full featured product:");
            director.buildLux();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Custom product:");
            builder.BuildBody();
            builder.BuildCover();
            Console.Write(builder.GetProduct().ListParts());
        }
    }
}
