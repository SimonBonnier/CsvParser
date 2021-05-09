namespace GenericCsvParse.Console
{
    using GenericCsvParser.Core;
    using GenericCsvParser.Core.Entities;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/CarsCSV.txt");

            //var employees = await CSVReader.ReadCsv<Employee>(path);

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine($"{employee.Id} {employee.FirstName} {employee.LastName} {employee.IdentificationNumber}");
            //}

            var cars = await CSVReader.ReadCsv<Car>(path);

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Id} {car.Brand} {car.Model} {car.CO2}");
            }

            Console.WriteLine("");
        }
    }
}
