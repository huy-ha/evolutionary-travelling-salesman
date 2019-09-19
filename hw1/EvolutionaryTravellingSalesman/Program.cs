using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    // TODO do circle cities test file
    // TODO implement Dijkstra's to know limit
    // TODO implement random search to know baseline
    // Plot path
    // Try to minimize evaluations
    class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = "tsp.txt";
            var cities = File.ReadAllLines(filePath)
                .Select(line => {
                    var coors = line.Split("\t",2);
                    var x = float.Parse(coors[0]);
                    var y = float.Parse(coors[1]);
                    return new TravellingSalesman.City(x, y);
                });

            // Initialize population
            int populationCount = 500;
            var population = new LinkedList<TravellingSalesman>();
            for(int i = 0; i < populationCount; i++)
            {
                population.AddLast(new TravellingSalesman(cities));
            }
            // Start evolution
            int generationCount = 100000;
            bool findShortestPath = true;
            float elitistPercentage = 0.01f;
            var selector = new SimulatedAnnealingSelected();
            float mutationFactor = 0.6f;
            float mutationFactorDecay = 0.9999f;

            int saveInterval = generationCount/50;

            // Timer for optimization purposes
            selector.Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Output variables
            string bestSalesmanOutput = "";
            List<float> minCosts = new List<float>(generationCount);
            List<float> maxCosts = new List<float>(generationCount);
            List<float> avgCosts = new List<float>(generationCount);
            for(int generation = 0; generation < generationCount; generation++){
                #if DEBUG
                    Console.WriteLine("\nGeneration " + generation);
                #endif
                //select
                // (var elites,var parents) = selector.Select(population,elitistPercentage,findShortestPath);
                var parents = selector.Select(population,elitistPercentage,findShortestPath);
                float parentsAvgDist = parents.Average(salesman => salesman.Cost);
                float parentsMaxDist = parents.Max(salesman => salesman.Cost);
                float parentsMinDist = parents.Min(salesman => salesman.Cost);
                #if DEBUG
                    Console.WriteLine("Average Parent: " + parentsAvgDist + ", Max: " + parentsMaxDist + ", Min: "+ parentsMinDist);
                #endif
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

                // Collect Stats
                float avgDist = population.Average(salesman => salesman.Cost);
                var maxSalesman = population.Aggregate((salesman1,salesman2)=> salesman1.Cost > salesman2.Cost ? salesman1 : salesman2);
                var minSalesman = population.Aggregate((salesman1,salesman2)=> salesman1.Cost < salesman2.Cost ? salesman1 : salesman2);
                float maxDist = maxSalesman.Cost;
                float minDist = minSalesman.Cost;
                minCosts.Add(minDist);
                maxCosts.Add(maxDist);
                avgCosts.Add(avgDist);
                #if DEBUG
                    Console.WriteLine("Average: " + avgDist + ", Max: " + maxDist + ", Min: "+ minDist);
                    Console.WriteLine("Population:" + population.Count + ", MutationFactor:" + mutationFactor);
                #endif
                if(mutationFactor == 0) break;
                if(generation%saveInterval == 0){
                    Console.WriteLine("Saving Generation " + generation);
                    bestSalesmanOutput += "Generation " + generation + "\n";
                    bestSalesmanOutput += minSalesman.PrintPath() + "\n";
                }
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.TotalSeconds);
            // Save Output
            System.IO.File.WriteAllText("BestSalesMan.txt", bestSalesmanOutput);
            System.IO.File.WriteAllText("MaxCosts.txt", string.Join("\n",maxCosts));
            System.IO.File.WriteAllText("MinCosts.txt", string.Join("\n",minCosts));
            System.IO.File.WriteAllText("AvgCosts.txt", string.Join("\n",avgCosts));
        }
    }
}
