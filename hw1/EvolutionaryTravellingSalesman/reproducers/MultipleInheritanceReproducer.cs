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

        public MultipleInheritanceReproducer(int populationCount, int numParents = 2)
        {
            m_populationCount = populationCount;
            m_numParents = numParents;
            m_rand = new System.Random();
        }

        public async Task<IEnumerable<TravellingSalesman>> Reproduce(IEnumerable<TravellingSalesman> reproducingPopulation, float mutationFactor, float T)
        {
            List<Task<TravellingSalesman>> offsprings = new List<Task<TravellingSalesman>>();
            int reproducingPopulationCount = reproducingPopulation.Count();
            while (offsprings.Count() < m_populationCount)
            {
                offsprings.Add(Task.Run(() =>
                {
                    // find m_numParents parents
                    var parentIndices = Enumerable.Range(0, 2).Select(x => m_rand.Next() % reproducingPopulationCount);
                    var parents = parentIndices.Select(idx => reproducingPopulation.ElementAt(idx));


                    Debug.Assert(parents.Count() == 2);
                    var firstParentPriorities = parents.ElementAt(0).priorities.OrderBy(pair => pair.Item1.id);
                    var secondParentPriorities = parents.ElementAt(1).priorities.OrderBy(pair => pair.Item1.id);

                    // Perform cross over
                    int citiesCount = firstParentPriorities.Count();
                    int crossOverIdx1 = m_rand.Next() % citiesCount;
                    int crossOverIdx2 = m_rand.Next() % citiesCount;
                    while (crossOverIdx1 >= crossOverIdx2)
                    {
                        if (crossOverIdx1 > crossOverIdx2)
                        {
                            int tmp = crossOverIdx2;
                            crossOverIdx1 = crossOverIdx2;
                            crossOverIdx2 = tmp;
                        }
                        else
                        {
                            crossOverIdx2 = m_rand.Next() % citiesCount;
                        }
                    }

                    var childPriority =
                    new LinkedList<Tuple<TravellingSalesman.City, float>>(
                    firstParentPriorities.Take(crossOverIdx1)
                    .Concat(secondParentPriorities.Take(crossOverIdx2).TakeLast(crossOverIdx2 - crossOverIdx1))
                    .Concat(firstParentPriorities.TakeLast(citiesCount - crossOverIdx2)));

                    //Mutation
                    float childFitness = PrioritiesMutator.MutatePriorities(childPriority, mutationFactor, T);
                    // Streamline mutation so less evaluations
                    return new TravellingSalesman(childPriority, childFitness);
                }));
            }

            return await Task.WhenAll(offsprings);
        }
    }
}