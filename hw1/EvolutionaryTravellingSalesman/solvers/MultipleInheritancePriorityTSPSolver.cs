using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    class MultipleInheritancePriorityTSPSolver : TSPSolver
    {
        protected float
        mutationFactor,
        mutationFactorDecay,
        temperature,
        temperatureDecay,
        elitistPercentage,
        reproductionPercentage = -1;

        Selector<TravellingSalesman> Selector;
        Reproducer<TravellingSalesman> Reproducer;
        public MultipleInheritancePriorityTSPSolver(Config initConfig) : base(initConfig)
        {

            SolverName = "Multiple Inheritance Priority TSP Solver";
            TravellingSalesman.config = config;
            mutationFactor = config.Get(Config.Float.InitMutationFactor);
            mutationFactorDecay = config.Get(Config.Float.MutationFactorDecay);
            temperature = config.Get(Config.Float.Temperature);
            temperatureDecay = config.Get(Config.Float.TemperatureDecay);
            elitistPercentage = config.Get(Config.Float.ElitistPercentage);
            reproductionPercentage = config.Get(Config.Float.ReproductionPercentage);
            //configure selector
            switch (config.Get(Config.String.Selector))
            {
                case "TruncateSelector":
                default:
                    Selector = new TruncateSelector<TravellingSalesman>(reproductionPercentage);
                    break;
            }
            //configure reproducer
            switch (config.Get(Config.String.Reproducer))
            {
                case "MultipleInheritanceReproducer":
                default:
                    Reproducer = new MultipleInheritanceReproducer(populationCount);
                    break;
            }
        }

        public override async Task Evolve()
        {//select
            var parents = Selector.Select(population);
#if DEBUG
            float parentsAvgDist = parents.Average(salesman => salesman.Cost);
            float parentsMaxDist = parents.Max(salesman => salesman.Cost);
            float parentsMinDist = parents.Min(salesman => salesman.Cost);
            Console.WriteLine("Average Parent: " + parentsAvgDist + ", Max: " + parentsMaxDist + ", Min: " + parentsMinDist);
#endif
            //mutate
            var offsprings = await Reproducer.Reproduce(parents, mutationFactor, temperature);
            var elites = population.OrderBy(salesman => salesman.Fitness()).Take((int)(elitistPercentage * populationCount));
            population = new LinkedList<TravellingSalesman>(offsprings.Concat(elites).OrderBy(salesman => salesman.Fitness()).Take(populationCount));
            mutationFactor *= mutationFactorDecay;
            temperature = temperatureDecay;
        }

    }
}