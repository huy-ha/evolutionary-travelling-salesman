using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = "cities.txt";
            var cities = File.ReadAllLines(filePath)
                .Select(line => {
                    var coors = line.Split(" ",2);
                    var x = float.Parse(coors[0]);
                    var y = float.Parse(coors[1]);
                    return new TravellingSalesman.City(x, y);
                });

            // Initialize population
            int populationCount = 10000;
            var population = new LinkedList<TravellingSalesman>();
            for(int i = 0; i < populationCount; i++)
            {
                population.AddLast(new TravellingSalesman(cities));
            }
            // Start evolution
            int generationCount = 100;
            bool findShortestPath = true;
            float elitistPercentage = 0.01f;
            var selector = new SimulatedAnnealingSelected();
            selector.Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for(int generation = 0; generation < generationCount; generation++){
                Console.WriteLine("\nGeneration " + generation);
                //select
                // (var elites,var parents) = selector.Select(population,elitistPercentage,findShortestPath);
                var parents = selector.Select(population,elitistPercentage,findShortestPath);
                //mutate
                var offspring = parents.Select(parent => new TravellingSalesman(parent));
                while(offspring.Count() < populationCount){
                    offspring = offspring.Concat(                             // 4. Concatenate newly created children with previous children
                        await Task.WhenAll(                                   // 3. Wait until all constructions of children is finished
                            parents.Select(                                                       
                                parent => Task.Run(() =>                      // 2. Run the constructor asynchronously 
                                new TravellingSalesman(parent))).ToArray())); // 1. From each parent, create a new TravellingSalesman
                }
                population = new LinkedList<TravellingSalesman>(offspring.Take(populationCount));
                // population = new LinkedList<TravellingSalesman>(elites.Concat(offspring));

                //get longest, shortest, and average
                float avgDist = population.Average(salesman => salesman.Cost);
                float maxDist = population.Max(salesman => salesman.Cost);
                float minDist = population.Min(salesman => salesman.Cost);
                Console.WriteLine("Average: " + avgDist + ", Max: " + maxDist + ", Min: "+ minDist);
                Console.WriteLine("Population:" + population.Count);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.TotalSeconds);
        }
    }
}
