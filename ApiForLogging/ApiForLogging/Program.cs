using System;
using System.Threading;
using System.Threading.Channels;

namespace ApiForLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            DataController dataController = new DataController(new apiforloggingdbContext("localhost", "5432", "apiforloggingdb", "postgres", "philipushka"));

            Console.WriteLine(dataController.AddRecord(new DateTime(2017, 7, 6), "1record", "low", "pc"));
            Console.WriteLine(dataController.AddRecord(new DateTime(2018, 7, 6), "2record", "medium", "server"));
            Console.WriteLine(dataController.AddRecord(new DateTime(2019, 7, 6), "3record", "high", "pc"));
            Console.WriteLine(dataController.AddRecord(new DateTime(2020, 7, 6), "4record", "medium", "pc"));

            Console.WriteLine("Количество записей: " + dataController.RecordsCount);
            Console.WriteLine("Количество записей для уровня логгирования medium: " + dataController.RecordsByLogCount("medium"));
            Console.WriteLine("Количество записей для источника pc: " + dataController.RecordsBySourceCount("pc"));

            Console.WriteLine("Все записи между 01.01.2019 и 01.01.2025:");
            dataController.FindByData(new DateTime(2019, 1, 1), new DateTime(2025, 1, 1)).ForEach(Console.WriteLine);

            Console.WriteLine("Удалено записей: " + dataController.DeleteRecords(1, 4));
            
            Console.WriteLine("Все записи с подстрокой record в тексте:");
            dataController.FindByTextOrSource("record").ForEach(Console.WriteLine);
            
            Console.WriteLine("Все записи с уровнем логгирования medium");
            dataController.FindByLog("medium").ForEach(Console.WriteLine);
            
            //CREATE TABLE Records (id INTEGER SERIAL primary key, date DATE, text VARCHAR, logLevel VARCHAR, source VARCHAR);
            //
        }
    }
}