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
    // TODO implement find shortest path by changing fitness calculation of TSP
    // TODO config file
    // TODO approximate time left
    // TODO stopping condition when no improvement in half the time
    // TODO Hierarchical tree based on spatial
    // TODO How to do tight linkage??
    // TODO How to do diversity????
    // TODO change to how many evaluations rather than generations
    // Plot path
    // Try to minimize evaluations
    class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = "tsp.txt";
            var cities = File.ReadAllLines(filePath)
                .Select(line =>
                {
                    var coors = line.Split("\t", 2);
                    var x = float.Parse(coors[0]);
                    var y = float.Parse(coors[1]);
                    return new TravellingSalesman.City(x, y);
                });

            // Initialize population
            int populationCount = 100;
            var population = new LinkedList<TravellingSalesman>();
            for (int i = 0; i < populationCount; i++)
            {
                population.AddLast(new TravellingSalesman(cities));
            }
            // Start evolution
            int generationCount = 50000;
            bool findShortestPath = true;
            float elitistPercentage = 0.02f;
            var selector = new SimulatedAnnealingSelected();
            float initMutationFactor = 0.5f;
            float mutationFactor = initMutationFactor;
            float mutationFactorDecay = 1f;

            // Simulated Annealing
            float init_T = 1f;
            float T = init_T; //Probability of choosing a worse gene anyways
            float T_decay = 0.9999f;

            int logCount = 50;
            int logFrequency = generationCount / logCount;

            // Timer for optimization purposes
            selector.Reset();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Output variables
            string bestSalesmanOutput = "";
            string worstSalesmanOutput = "";
            List<float> minCosts = new List<float>(generationCount);
            List<float> maxCosts = new List<float>(generationCount);
            List<float> avgCosts = new List<float>(generationCount);
            for (int generation = 0; generation < generationCount; generation++)
            {
#if DEBUG
                Console.WriteLine("\nGeneration " + generation);
#endif
                // Select individuals that will be reproducing
                var parents = selector.Select(population, elitistPercentage, findShortestPath);
#if DEBUG
                float parentsAvgDist = parents.Average(salesman => salesman.Cost);
                float parentsMaxDist = parents.Max(salesman => salesman.Cost);
                float parentsMinDist = parents.Min(salesman => salesman.Cost);
                Console.WriteLine("Average Parent: " + parentsAvgDist + ", Max: " + parentsMaxDist + ", Min: " + parentsMinDist);
#endif
                // Create offsprings
                IEnumerable<TravellingSalesman> offspring = await Task.WhenAll(
                    parents.Select(parent => Task.Run(() =>
                     new TravellingSalesman(parent, T, findShortestPath))));
                while (offspring.Count() < populationCount)
                {
                    offspring = offspring.Concat(                             // 4. Concatenate newly created children with previous children
                        await Task.WhenAll(                                   // 3. Wait until all constructions of children is finished
                            parents.Select(
                                parent => Task.Run(() =>                      // 2. Run the constructor asynchronously 
                                new TravellingSalesman(parent, T, findShortestPath))).ToArray())); // 1. From each parent, create a new TravellingSalesman
                }
                if (findShortestPath)
                {
                    //elites
                    offspring = population.OrderBy(salesman => salesman.Cost).Take((int)(elitistPercentage * populationCount)).Concat(offspring);
                    population = new LinkedList<TravellingSalesman>(offspring.OrderBy(salesman => salesman.Cost).Take(populationCount));
                }
                else
                {
                    offspring = population.OrderByDescending(salesman => salesman.Cost).Take((int)(elitistPercentage * populationCount)).Concat(offspring);
                    population = new LinkedList<TravellingSalesman>(offspring.OrderByDescending(salesman => salesman.Cost).Take(populationCount));
                }
                T *= T_decay;
                mutationFactor *= mutationFactorDecay;

                // Collect Stats
                float avgDist = population.Average(salesman => salesman.Cost);
                var maxSalesman = population.Aggregate((salesman1, salesman2) => salesman1.Cost > salesman2.Cost ? salesman1 : salesman2);
                var minSalesman = population.Aggregate((salesman1, salesman2) => salesman1.Cost < salesman2.Cost ? salesman1 : salesman2);
                float maxDist = maxSalesman.Cost;
                float minDist = minSalesman.Cost;

                minCosts.Add(minDist);
                maxCosts.Add(maxDist);
                avgCosts.Add(avgDist);
#if DEBUG
                Console.WriteLine("Average: " + avgDist + ", Max: " + maxDist + ", Min: " + minDist);
                Console.WriteLine("Population:" + population.Count + ", MutationFactor:" + mutationFactor);
#endif
                if (mutationFactor == 0) break;
                if (generation % logFrequency == 0 || generation == generationCount - 1)
                {
                    Console.WriteLine("Saving Generation " + generation);
                    bestSalesmanOutput += "Generation " + generation + "\n";
                    bestSalesmanOutput += minSalesman.PrintPath() + "\n";
                    worstSalesmanOutput += "Generation " + generation + "\n";
                    worstSalesmanOutput += minSalesman.PrintPath() + "\n";
                }
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.TotalSeconds);
            // Save Output
            string config = "";
            config += "Cities File: " + filePath + "\n";
            config += "FindShortestPath: " + findShortestPath + "\n";
            config += "Generation Count: " + generationCount + "\n";
            config += "\n";
            config += "Population Count: " + populationCount + "\n";
            config += "Elitist Percentage: " + elitistPercentage + "\n";
            config += "\n";
            config += "Initial Mutation Factor: " + initMutationFactor + "\n";
            config += "Mutation Factor Decay: " + mutationFactorDecay + "\n";
            config += "\n";
            config += "Init T: " + init_T + "\n";
            config += "T Decay: " + T_decay + "\n";
            System.IO.File.WriteAllText("output/Config.txt", config);
            System.IO.File.WriteAllText("output/BestSalesMan.txt", bestSalesmanOutput);
            System.IO.File.WriteAllText("output/WorstSalesMan.txt", bestSalesmanOutput);
            System.IO.File.WriteAllText("output/MaxCosts.txt", string.Join("\n", maxCosts));
            System.IO.File.WriteAllText("output/MinCosts.txt", string.Join("\n", minCosts));
            System.IO.File.WriteAllText("output/AvgCosts.txt", string.Join("\n", avgCosts));
        }
    }
}
