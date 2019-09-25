using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    class ElitesAnnealingTSPSolver : TSPSolver
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
        public ElitesAnnealingTSPSolver(Config initConfig) : base(initConfig)
        {

            SolverName = "Elites Annealing TSPSolver";
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
                    Reproducer = new MultipleInheritanceReproducer(populationCount);
                    break;
                case "AsexualSwapReproducer":
                default:
                    Reproducer = new AsexualSwapReproducer(populationCount);
                    break;
            }
#if DEBUG
            OnLog += () =>
            {
                Console.WriteLine("mutationFactor:{0} | temperature:{1} | populationCount {2}", mutationFactor, temperature, populationCount);
            };
#endif
        }

        public override async Task Evolve()
        {
            //select
            var parents = Selector.Select(population);
#if DEBUG
            float parentsAvgDist = parents.Average(salesman => salesman.Cost);
            float parentsMaxDist = parents.Max(salesman => salesman.Cost);
            float parentsMinDist = parents.Min(salesman => salesman.Cost);
            Console.WriteLine("Average Parent: " + parentsAvgDist + ", Max: " + parentsMaxDist + ", Min: " + parentsMinDist);
#endif
            //mutate
            var offsprings = await Reproducer.Reproduce(parents, mutationFactor, temperature);
#if DEBUG
            Console.WriteLine("Got {0} offsprings", offsprings.Count());
#endif
            var elites = population.OrderByDescending(salesman => salesman.Fitness()).Take((int)(elitistPercentage * populationCount));
#if DEBUG
            Console.WriteLine("Got {0} elites with average cost of {1}", elites.Count(), elites.Average(elite => elite.Cost));
#endif
            population = new LinkedList<TravellingSalesman>(offsprings.Concat(elites).OrderByDescending(salesman => salesman.Fitness()).Take(populationCount));
            mutationFactor *= mutationFactorDecay;
            temperature *= temperatureDecay;
        }
    }
}
