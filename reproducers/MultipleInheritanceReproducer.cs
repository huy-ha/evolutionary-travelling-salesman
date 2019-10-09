using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EvolutionaryTravellingSalesman
{
    public class MultipleInheritanceReproducer : Reproducer<TravellingSalesman>
    {
        int m_populationCount = -1;
        string mutatorConfig;
        string crossoverConfig;
        Config config;

        public MultipleInheritanceReproducer(int populationCount)
        {
            m_populationCount = populationCount;
            config = TravellingSalesman.config;
            mutatorConfig = config.Get(Config.String.Mutator);
            crossoverConfig = config.Get(Config.String.CrossOver);
        }

        public async Task<IEnumerable<TravellingSalesman>> Reproduce(IEnumerable<TravellingSalesman> reproducingPopulation, float mutationFactor, float T)
        {
            int reproducingPopulationCount = reproducingPopulation.Count();
            Random rand = new Random();
            if (config.Get(Config.String.Genotype) == "List")
            {
                List<Task<TravellingSalesman>> offsprings = new List<Task<TravellingSalesman>>();
                while (offsprings.Count() < m_populationCount)
                {
                    int idx1 = rand.Next() % reproducingPopulationCount;
                    int idx2 = rand.Next() % reproducingPopulationCount;
                    double mutationChance = rand.NextDouble() % 1;
                    offsprings.Add(Task.Run(() =>
                    {
                        var g1 = reproducingPopulation.ElementAt(idx1).genotype;
                        var g2 = reproducingPopulation.ElementAt(idx2).genotype;
                        ListGenotype childGenotype;
                        // Cross Over
                        switch (crossoverConfig)
                        {
                            case "Selection":
                                childGenotype = SelectionCrossover.CrossOver(g1 as ListGenotype, g2 as ListGenotype);
                                break;
                            default:
                                throw new Exception("Invalid CrossOver!");
                        }

                        //Mutate
                        switch (mutatorConfig)
                        {
                            case "SingleSwap":
                                return new TravellingSalesman(
                                    SingleSwapMutator.Mutate(childGenotype as ListGenotype, mutationFactor, T));
                            case "Insert":
                                return new TravellingSalesman(
                                    InsertMutator.Mutate(childGenotype as ListGenotype, mutationFactor, T));
                            default:
                                throw new Exception("Invalid Mutator!");
                        }
                    }));
                }
                return await Task.WhenAll(offsprings);
            }
            else
            {
                List<Task<IEnumerable<TravellingSalesman>>> offsprings = new List<Task<IEnumerable<TravellingSalesman>>>();
                while (offsprings.Count() / 2 < m_populationCount)
                {
                    int idx1 = rand.Next() % reproducingPopulationCount;
                    int idx2 = rand.Next() % reproducingPopulationCount;
                    offsprings.Add(Task.Run(() =>
                    {
                        var g1 = reproducingPopulation.ElementAt(idx1).genotype;
                        var g2 = reproducingPopulation.ElementAt(idx2).genotype;
                        // Only one of the options
                        return PriorityCrossOver.CrossOver(g1 as PriorityGenotype, g2 as PriorityGenotype) //1. Cross over parents genotypes
                            .Select(childGenotype => new TravellingSalesman( //3. create travelling salesman phenotypes from genotypes
                                PrioritySingleMutator.Mutate(childGenotype, mutationFactor, T))); //2. Mutate the offspring genotypes
                    }));
                }
                var siblingsGroups = await Task.WhenAll(offsprings);
                return siblingsGroups.Aggregate(new List<TravellingSalesman>().AsEnumerable(), (allOffsprings, siblings) => allOffsprings.Concat(siblings));
            }
        }
    }
}