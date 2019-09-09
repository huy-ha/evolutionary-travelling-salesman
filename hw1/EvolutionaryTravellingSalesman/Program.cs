using System;
using System.IO;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO do file read
            var filePath = "";
            var cities = File.ReadAllLines(filePath)
                .Select(line => new TravellingSalesman.City(0, 0));

            int populationCount = 10000;
            var salesman = new TravellingSalesman(cities);
            Console.WriteLine("Hello World!");
        }
    }
}
