using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EvolutionaryTravellingSalesman
{
    public class AsexualSwapReproducer : Reproducer<TravellingSalesman>
    {
        int m_populationCount;

        public AsexualSwapReproducer(int populationCount, int numParents = 2)
        {
            m_populationCount = populationCount;
        }

        public async Task<IEnumerable<TravellingSalesman>> Reproduce(IEnumerable<TravellingSalesman> reproducingPopulation, float mutationFactor, float T)
        {
            List<Task<TravellingSalesman>> offsprings = new List<Task<TravellingSalesman>>();
            int reproducingPopulationCount = reproducingPopulation.Count();
            while (offsprings.Count() < m_populationCount)
            {
                offsprings.Add(Task.Run(() =>
                {
                    //sample a parent
                    Random rand = new Random();
                    var parent = reproducingPopulation.ElementAt(rand.Next() % reproducingPopulationCount);
                    float childFitness = -1;
                    LinkedList<Tuple<TravellingSalesman.City, float>> childPriorities;
                    (childPriorities, childFitness) = SingleSwapMutator.MutatePriorities( //2. Mutate parent priorities with mutation factor and T
                                                            parent.priorities, // 1. Get random parent
                                                             mutationFactor, T);
                    for (int attempt = 0; attempt < 10; attempt++)
                    {
                        if (childFitness < parent.Fitness() || ((rand.NextDouble() % 1) >= T && attempt > 0))
                        {
                            System.Diagnostics.Debug.Assert(childPriorities.Count() == parent.priorities.Count());
                            return new TravellingSalesman(childPriorities);
                        }
                        (childPriorities, childFitness) = SingleSwapMutator.MutatePriorities( //2. Mutate parent priorities with mutation factor and T
                                                            parent.priorities, // 1. Get random parent
                                                             mutationFactor, T);
                    }
                    System.Diagnostics.Debug.Assert(childPriorities.Count() == parent.priorities.Count());
                    // Give up after 10 attempts
                    return new TravellingSalesman(childPriorities);
                    // try new offspring
                }));
            }

            return await Task.WhenAll(offsprings);
        }
    }
}