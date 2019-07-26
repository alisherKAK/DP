// Паттерн Шаблонный метод
//
// Назначение: Определяет скелет алгоритма, перекладывая ответственность за
// некоторые его шаги на подклассы. Паттерн позволяет подклассам переопределять
// шаги алгоритма, не меняя его общей структуры.

using System;

namespace TemplateMethod
{
    // Абстрактный Класс определяет шаблонный метод, содержащий скелет
    // некоторого алгоритма, состоящего из вызовов (обычно) абстрактных
    // примитивных операций.
    //
    // Конкретные подклассы должны реализовать эти операции, но оставить сам
    // шаблонный метод без изменений.
    abstract class DataMiner
    {
        // Шаблонный метод определяет скелет алгоритма.
        public void Mine()
        {
            this.OpenFile();
            this.ParseData();
            this.ExtractData();
            this.AnalyzeData();
            this.SendReport();
            this.CloseFile();
        }

        // Эти операции уже имеют реализации.
        protected void AnalyzeData()
        {
            Console.WriteLine("DataMiner says: I am analyzing data");
        }

        protected void SendReport()
        {
            Console.WriteLine("DataMiner says: I'm sending report");
        }

        // А эти операции должны быть реализованы в подклассах.
        protected abstract void ParseData();

        protected abstract void ExtractData();

        // Это «хуки». Подклассы могут переопределять их, но это не
        // обязательно, поскольку у хуков уже есть стандартная (но пустая)
        // реализация. Хуки предоставляют дополнительные точки расширения в
        // некоторых критических местах алгоритма.
        protected virtual void OpenFile() {
            Console.WriteLine("DataMiner says: I'm opening file");
        }

        protected virtual void CloseFile() {
            Console.WriteLine("DataMiner says: I'm closing file");
        }
    }

    // Конкретные классы должны реализовать все абстрактные операции
    // базового класса. Они также могут переопределить некоторые операции с
    // реализацией по умолчанию.
    class XMLDataMiner : DataMiner
    {
        protected override void ParseData()
        {
            Console.WriteLine("XMLDataMiner says: I'm parsing data");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("XMLDataMiner says: I'm extracting data");
        }
    }

    // Обычно конкретные классы переопределяют только часть операций
    // базового класса.
    class PDFDataMiner : DataMiner
    {
        protected override void ParseData()
        {
            Console.WriteLine("PDFDataMiner says: I'm parsing data");
        }

        protected override void ExtractData()
        {
            Console.WriteLine("PDFDataMiner says: I'm extracting data");
        }

        protected override void OpenFile()
        {
            Console.WriteLine("PDFDataMiner says: Overridden Opening file");
        }
    }

    class Client
    {
        // Клиентский код вызывает шаблонный метод для выполнения алгоритма.
        // Клиентский код не должен знать конкретный класс объекта, с которым
        // работает, при условии, что он работает с объектами через интерфейс их
        // базового класса.
        public static void ClientCode(DataMiner abstractClass)
        {
            // ...
            abstractClass.Mine();
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Same client code can work with different subclasses:");

            Client.ClientCode(new XMLDataMiner());

            Console.Write("\n");

            Console.WriteLine("Same client code can work with different subclasses:");
            Client.ClientCode(new PDFDataMiner());
        }
    }
}
