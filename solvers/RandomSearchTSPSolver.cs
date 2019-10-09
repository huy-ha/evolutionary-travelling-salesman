using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    class RandomSearchTSPSolver : TSPSolver
    {
        public RandomSearchTSPSolver(Config initConfig) : base(initConfig)
        {
            SolverName = "Random Search TSPSolver";
            TravellingSalesman.config = config;
        }
        public override async Task Evolve()
        {
            var elite = population.OrderByDescending(salesman => salesman.Fitness()).Take(1);
            List<Task<TravellingSalesman>> asyncPopulation = new List<Task<TravellingSalesman>>();
            for (int i = 0; i < config.Get(Config.Int.PopulationCount) - 1; i++)
            {
                asyncPopulation.Add(Task.Run(() => new TravellingSalesman(cities)));
            }
            population = new LinkedList<TravellingSalesman>(elite.Concat(await Task.WhenAll(asyncPopulation)));
        }
    }
}