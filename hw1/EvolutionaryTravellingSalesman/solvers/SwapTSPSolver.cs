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
        public MultipleInheritancePriorityTSPSolver()
        {
            SolverName = "Multiple Inheritance Priority TSP Solver";
            OnConfigure += config =>
            {
                mutationFactor = config.Get(Config.Float.InitMutationFactor);
                mutationFactorDecay = config.Get(Config.Float.MutationFactorDecay);
                temperature = config.Get(Config.Float.Temperature);
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
                        Reproducer = new MultipleInheritanceReproducer<TravellingSalesman>(generationCount, 3);
                        break;
                }
            };
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
            var offspring = Reproducer.Reproduce(parents);
            var elites = population.OrderBy(salesman => salesman.Fitness()).Take((int)(elitistPercentage * populationCount));
            population = new LinkedList<TravellingSalesman>(offspring.Concat(elites).OrderBy(salesman => salesman.Fitness()).Take(populationCount));
            mutationFactor *= mutationFactorDecay;
            temperature = temperatureDecay;
        }

    }
}
