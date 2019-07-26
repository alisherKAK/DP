// Паттерн Абстрактная Фабрика
//
// Назначение: Предоставляет интерфейс для создания семейств связанных или
// зависимых объектов без привязки к их конкретным классам.

using System;

namespace DesignPatterns.AbstractFactory.Concrete
{
    // Интерфейс Абстрактной Фабрики объявляет набор методов, которые
    // возвращают различные абстрактные продукты.  Эти продукты называются
    // семейством и связаны темой или концепцией высокого уровня. Продукты
    // одного семейства обычно могут взаимодействовать между собой. Семейство
    // продуктов может иметь несколько вариаций,  но продукты одной вариации
    // несовместимы с продуктами другой.
    public interface ITelephoneFactory
    {
        IDisplay CreateDisplay();

        IAccumulator CreateAccumulator();
    }

    // Конкретная Фабрика производит семейство продуктов одной вариации.
    // Фабрика гарантирует совместимость полученных продуктов.  Обратите
    // внимание, что сигнатуры методов Конкретной Фабрики возвращают абстрактный
    // продукт, в то время как внутри метода создается экземпляр  конкретного
    // продукта.
    class Samsung : ITelephoneFactory
    {
        public IDisplay CreateDisplay()
        {
            return new SamsungDisplay();
        }

        public IAccumulator CreateAccumulator()
        {
            return new SamsungAccumulator();
        }
    }

    // Каждая Конкретная Фабрика имеет соответствующую вариацию продукта.
    class Nokia : ITelephoneFactory
    {
        public IDisplay CreateDisplay()
        {
            return new NokiaDisplay();
        }

        public IAccumulator CreateAccumulator()
        {
            return new NokiaAccumulator();
        }
    }

    // Каждый отдельный продукт семейства продуктов должен иметь базовый
    // интерфейс. Все вариации продукта должны реализовывать этот интерфейс.
    public interface IDisplay
    {
        string ShowMessage();
    }

    // Конкретные продукты создаются соответствующими Конкретными Фабриками.
    class SamsungDisplay : IDisplay
    {
        public string ShowMessage()
        {
            return "Samsung's display shows message:";
        }
    }

    class NokiaDisplay : IDisplay
    {
        public string ShowMessage()
        {
            return "Nokia's display shows message:";
        }
    }

    // Базовый интерфейс другого продукта. Все продукты могут
    // взаимодействовать друг с другом, но правильное взаимодействие возможно
    // только между продуктами одной и той же конкретной вариации.
    public interface IAccumulator
    {
        // Продукт B способен работать самостоятельно...
        string ShowBatteryVolume();

        // ...а также взаимодействовать с Продуктами Б той же вариации.
        //
        // Абстрактная Фабрика гарантирует, что все продукты, которые она
        // создает, имеют одинаковую вариацию и, следовательно, совместимы.
        string ShowBatteryChargeLevel(IDisplay collaborator);
    }

    // Конкретные Продукты создаются соответствующими Конкретными Фабриками.
    class SamsungAccumulator : IAccumulator
    {
        public string ShowBatteryVolume()
        {
            return "Samsung's battery volume is 2000 MAh";
        }

        // Продукт B1 может корректно работать только с Продуктом A1. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в
        // качестве аргумента.
        public string ShowBatteryChargeLevel(IDisplay collaborator)
        {
            var result = collaborator.ShowMessage();

            return $"({result}): battary charge level is 40%";
        }
    }

    class NokiaAccumulator : IAccumulator
    {
        public string ShowBatteryVolume()
        {
            return "Nokia's battery volume is 1500 MAh";
        }

        // Продукт B2 может корректно работать только с Продуктом A2. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в качестве
        // аргумента.
        public string ShowBatteryChargeLevel(IDisplay collaborator)
        {
            var result = collaborator.ShowMessage();

            return $"({result}): battary charge level is 40%";
        }
    }

    // Клиентский код работает с фабриками и продуктами только через
    // абстрактные типы: Абстрактная Фабрика и Абстрактный Продукт. Это
    // позволяет передавать любой подкласс фабрики или продукта клиентскому
    // коду, не нарушая его.
    class Client
    {
        public void Main()
        {
            // Клиентский код может работать с любым конкретным классом
            // фабрики.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new Samsung());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new Nokia());
        }

        public void ClientMethod(ITelephoneFactory factory)
        {
            IDisplay display = factory.CreateDisplay();
            IAccumulator accumulator = factory.CreateAccumulator();

            Console.WriteLine(accumulator.ShowBatteryVolume());
            Console.WriteLine(accumulator.ShowBatteryChargeLevel(display));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}