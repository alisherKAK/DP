// Паттерн Посетитель
//
// Назначение: Позволяет создавать новые операции, не меняя классы объектов, над
// которыми эти операции могут выполняться.

using System;
using System.Collections.Generic;

namespace Visitor
{
    // Интерфейс Компонента объявляет метод accept, который в качестве
    // аргумента может получать любой объект, реализующий интерфейс посетителя.
    public interface IPlace
    {
        void Accept(IVisitor visitor);
    }

    // Каждый Конкретный Компонент должен реализовать метод Accept таким
    // образом, чтобы он вызывал метод посетителя, соответствующий классу
    // компонента.
    public class Home : IPlace
    {
        // Обратите внимание, мы вызываем VisitConcreteComponentA, что
        // соответствует названию текущего класса. Таким образом мы позволяем
        // посетителю узнать, с каким классом компонента он работает.
        public void Accept(IVisitor visitor)
        {
            visitor.VisitDom(this);
        }

        // Конкретные Компоненты могут иметь особые методы, не объявленные в
        // их базовом классе или интерфейсе. Посетитель всё же может
        // использовать эти методы, поскольку он знает о конкретном классе
        // компонента.
        public string TakeOrderForHome()
        {
            return "Home takes insurance";
        }
    }

    public class Bank : IPlace
    {
        // То же самое здесь: VisitConcreteComponentB => ConcreteComponentB
        public void Accept(IVisitor visitor)
        {
            visitor.VisitBank(this);
        }

        public string TakeOrderForBank()
        {
            return "Bank takes insurance";
        }
    }

    // Интерфейс Посетителя объявляет набор методов посещения,
    // соответствующих классам компонентов. Сигнатура метода посещения позволяет
    // посетителю определить конкретный класс компонента, с которым он имеет
    // дело.
    public interface IVisitor
    {
        void VisitDom(Home element);

        void VisitBank(Bank element);
    }

    // Конкретные Посетители реализуют несколько версий одного и того же
    // алгоритма, которые могут работать со всеми классами конкретных
    // компонентов.
    //
    // Максимальную выгоду от паттерна Посетитель вы почувствуете, используя его
    // со сложной структурой объектов, такой как дерево Компоновщика. В этом
    // случае было бы полезно хранить некоторое промежуточное состояние
    // алгоритма при выполнении методов посетителя над различными объектами
    // структуры.
    class KomeskAgent : IVisitor
    {
        public void VisitDom(Home element)
        {
            Console.WriteLine("KomeskAgent: " + element.TakeOrderForHome());
        }

        public void VisitBank(Bank element)
        {
            Console.WriteLine("KomeskAgent: " + element.TakeOrderForBank());
        }
    }

    class HalykAgent : IVisitor
    {
        public void VisitDom(Home element)
        {
            Console.WriteLine("HalykAgent: " + element.TakeOrderForHome());
        }

        public void VisitBank(Bank element)
        {
            Console.WriteLine("HalykAgent: " + element.TakeOrderForBank());
        }
    }

    public class Client
    {
        // Клиентский код может выполнять операции посетителя над любым
        // набором элементов, не выясняя их конкретных классов. Операция
        // принятия направляет вызов к соответствующей операции в объекте
        // посетителя.
        public static void ClientCode(List<IPlace> components, IVisitor visitor)
        {
            foreach (var component in components)
            {
                component.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<IPlace> places = new List<IPlace>
            {
                new Home(),
                new Bank()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new KomeskAgent();
            Client.ClientCode(places, visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new HalykAgent();
            Client.ClientCode(places, visitor2);
        }
    }
}
