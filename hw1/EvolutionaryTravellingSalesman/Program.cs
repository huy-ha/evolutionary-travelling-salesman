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
            int populationCount = 1000;
            var population = new LinkedList<TravellingSalesman>();
            for(int i = 0; i < populationCount; i++)
            {
                population.AddLast(new TravellingSalesman(cities));
            }
            // Start evolution
            int generationCount = 1000000;
            bool findShortestPath = true;
            float elitistPercentage = 0.01f;
            var selector = new SimulatedAnnealingSelected();
            float mutationFactor = 0.99f;
            float mutationFactorDecay = 0.99999f;

            // Timer for optimization purposes
            selector.Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for(int generation = 0; generation < generationCount; generation++){
                Console.WriteLine("\nGeneration " + generation);
                //select
                // (var elites,var parents) = selector.Select(population,elitistPercentage,findShortestPath);
                var parents = selector.Select(population,elitistPercentage,findShortestPath);
                float parentsAvgDist = parents.Average(salesman => salesman.Cost);
                float parentsMaxDist = parents.Max(salesman => salesman.Cost);
                float parentsMinDist = parents.Min(salesman => salesman.Cost);
                 Console.WriteLine("Average: " + parentsAvgDist + ", Max: " + parentsMaxDist + ", Min: "+ parentsMinDist);
                //mutate
                var offspring = parents.Select(parent => new TravellingSalesman(parent,mutationFactor));
                while(offspring.Count() < populationCount){
                    offspring = offspring.Concat(                             // 4. Concatenate newly created children with previous children
                        await Task.WhenAll(                                   // 3. Wait until all constructions of children is finished
                            parents.Select(                                                       
                                parent => Task.Run(() =>                      // 2. Run the constructor asynchronously 
                                new TravellingSalesman(parent))).ToArray())); // 1. From each parent, create a new TravellingSalesman
                }
                population = new LinkedList<TravellingSalesman>(offspring.Take(populationCount));
                mutationFactor *= mutationFactorDecay;

                //get longest, shortest, and average
                float avgDist = population.Average(salesman => salesman.Cost);
                float maxDist = population.Max(salesman => salesman.Cost);
                float minDist = population.Min(salesman => salesman.Cost);
                Console.WriteLine("Average: " + avgDist + ", Max: " + maxDist + ", Min: "+ minDist);
                Console.WriteLine("Population:" + population.Count + ", MutationFactor:" + mutationFactor);
                if(mutationFactor == 0) break;
                
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.TotalSeconds);
        }
    }
}
