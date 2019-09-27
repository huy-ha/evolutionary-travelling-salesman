using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EvolutionaryTravellingSalesman
{
    public class MultipleInheritanceReproducer : Reproducer<TravellingSalesman>
    {
        int m_numParents;
        int m_populationCount;
        System.Random m_rand;

        public MultipleInheritanceReproducer(int populationCount)
        {
            m_populationCount = populationCount;
        }

        public async Task<IEnumerable<TravellingSalesman>> Reproduce(IEnumerable<TravellingSalesman> reproducingPopulation, float mutationFactor, float T)
        {
            List<Task<IEnumerable<TravellingSalesman>>> offsprings = new List<Task<IEnumerable<TravellingSalesman>>>();
            int reproducingPopulationCount = reproducingPopulation.Count();
            while (offsprings.Count() / 2 < m_populationCount)
            {
                offsprings.Add(Task.Run(() =>
                {
                    Random rand = new Random(offsprings.Count());
                    var g1 = reproducingPopulation.ElementAt(rand.Next() % reproducingPopulationCount).genotype;
                    var g2 = reproducingPopulation.ElementAt(rand.Next() % reproducingPopulationCount).genotype;
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