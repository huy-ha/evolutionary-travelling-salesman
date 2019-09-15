using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace EvolutionaryTravellingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO do file read
            var filePath = "cities.txt";
            var cities = File.ReadAllLines(filePath)
                .Select(line => {
                    var coors = line.Split(" ",2);
                    var x = float.Parse(coors[0]);
                    var y = float.Parse(coors[1]);
                    return new TravellingSalesman.City(x, y);
                });

            //Initialize population
            int populationCount = 10000;
            var population = new LinkedList<TravellingSalesman>();
            for(int i = 0; i < populationCount; i++)
            {
                var salesman = new TravellingSalesman(cities);
                Console.WriteLine(salesman);
                population.AddLast(salesman);
            }
            //start evolution
            int generationCount = 100;
            bool findShortestPath = true;
            var selector = new SimulatedAnnealingSelected();
            selector.Reset();
            for(int generation = 0; generation < generationCount; generation++){
                //select
                var toReproduce = selector.Select(population);
                //mutate
                
            }
        }
    }
}
