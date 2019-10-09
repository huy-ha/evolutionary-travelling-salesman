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

                    Selector = new TruncateSelector<TravellingSalesman>(reproductionPercentage);
                    break;
                default:
                    throw new Exception("Invalid Selector");
            }
            //configure reproducer
            switch (config.Get(Config.String.Reproducer))
            {
                case "MultipleInheritance":
                    Reproducer = new MultipleInheritanceReproducer(populationCount);
                    break;
                case "Asexual":
                    Debug.Assert(config.Get(Config.String.Genotype) == "List");
                    Reproducer = new AsexualReproducer(populationCount);
                    break;
                default:
                    throw new Exception("Invalid Reproducer type");
            }
            OnLog += () =>
            {
                Console.WriteLine("mutationFactor:{0} | temperature:{1} | populationCount {2}", mutationFactor, temperature, populationCount);
            };
        }

        public override async Task Evolve()
        {
            //select
            var parents = Selector.Select(population);
            //reproduce (cross-over and mutate)
            var offsprings = await Reproducer.Reproduce(parents, mutationFactor, temperature);
            //pick out elites
            var elites = population.OrderByDescending(salesman => salesman.Fitness()).Take((int)(elitistPercentage * populationCount));
            //create new generation from elites and offsprings, but take the only the best ones
            population = new LinkedList<TravellingSalesman>(offsprings.Concat(elites).OrderByDescending(salesman => salesman.Fitness()).Take(populationCount));
            mutationFactor *= mutationFactorDecay;
            temperature *= temperatureDecay;
        }
    }
}
