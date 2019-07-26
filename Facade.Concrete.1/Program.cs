// Паттерн Фасад
//
// Назначение: Предоставляет простой интерфейс к сложной системе классов,
// библиотеке или фреймворку.

using System;

namespace Facade
{
    // Класс Фасада предоставляет простой интерфейс для сложной логики одной
    // или нескольких подсистем. Фасад делегирует запросы клиентов
    // соответствующим объектам внутри подсистемы. Фасад также отвечает за
    // управление их жизненным циклом. Все это защищает клиента от нежелательной
    // сложности подсистемы.
    public class PostOffice
    {
        protected CourierSubsystem _subsystem1;

        protected PackingSubsystem _subsystem2;

        protected PayingSubsystem _subsystem3;

        public PostOffice(CourierSubsystem subsystem1, PackingSubsystem subsystem2, PayingSubsystem subsystem3)
        {
            this._subsystem1 = subsystem1;
            this._subsystem2 = subsystem2;
            this._subsystem3 = subsystem3;
        }

        // Методы Фасада удобны для быстрого доступа к сложной
        // функциональности подсистем. Однако клиенты получают только часть
        // возможностей подсистемы.
        public string SendParcel()
        {
          
            string result = this._subsystem1.operation1();
            result += this._subsystem2.operation1();            
            result += this._subsystem2.operationZ();
            result += this._subsystem1.operationN();                        
            result += this._subsystem1.operationN();
            result += this._subsystem2.operationZ();
            return result;
        }
    }

    // Подсистема может принимать запросы либо от фасада, либо от клиента
    // напрямую. В любом случае, для Подсистемы Фасад – это еще один клиент, и
    // он не является частью Подсистемы.
    public class CourierSubsystem
    {
        public string operation1()
        {
            return "Курьер вызван\n";
        }

        public string operationN()
        {
            return "Курьер принял посылку\n";
        }
    }

    // Некоторые фасады могут работать с разными подсистемами одновременно.
    public class PackingSubsystem
    {
        public string operation1()
        {
            return "Посылка упакована\n";
        }

        public string operationZ()
        {
            return "Посылка передана курьеру\n";
        }
    }

    // Некоторые фасады могут работать с разными подсистемами одновременно.
    public class PayingSubsystem
    {
        public string operation1()
        {
            return "Посылка оплачивается\n";
        }

        public string operationX()
        {
            return "Посылка оплачена\n";
        }
    }

    class Client
    {
        // Клиентский код работает со сложными подсистемами через простой
        // интерфейс, предоставляемый Фасадом. Когда фасад управляет жизненным
        // циклом подсистемы, клиент может даже не знать о существовании
        // подсистемы. Такой подход позволяет держать сложность под контролем.
        public static void ClientCode(PostOffice facade)
        {
            Console.Write(facade.SendParcel());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // В клиентском коде могут быть уже созданы некоторые объекты
            // подсистемы. В этом случае может оказаться целесообразным
            // инициализировать Фасад с этими объектами вместо того, чтобы
            // позволить Фасаду создавать новые экземпляры.
            CourierSubsystem subsystem1 = new CourierSubsystem();
            PackingSubsystem subsystem2 = new PackingSubsystem();
            PayingSubsystem subsystem3 = new PayingSubsystem();
            PostOffice facade = new PostOffice(subsystem1, subsystem2, subsystem3);
            Client.ClientCode(facade);
        }
    }
}
