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
                    Random threadRand = new Random(offsprings.Count());
                    // find m_numParents parents
                    var parentIndices = Enumerable.Range(0, 2).Select(x => threadRand.Next() % reproducingPopulationCount);
                    var parents = parentIndices.Select(idx => reproducingPopulation.ElementAt(idx));


                    Debug.Assert(parents.Count() == 2);
                    var firstParentPriorities = parents.ElementAt(0).priorities.OrderBy(pair => pair.Item1.id);
                    var secondParentPriorities = parents.ElementAt(1).priorities.OrderBy(pair => pair.Item1.id);
#if DEBUG
                    Console.WriteLine("\tgot parent priorities");
#endif
                    // Perform cross over
                    int citiesCount = firstParentPriorities.Count();
                    int crossOverIdx1 = threadRand.Next() % citiesCount;
                    int crossOverIdx2 = threadRand.Next() % citiesCount;
                    while (crossOverIdx1 >= crossOverIdx2)
                    {
#if DEBUG
                        Console.WriteLine("\t\t clashing idx {0} and {1}", crossOverIdx1, crossOverIdx2);
#endif                        
                        crossOverIdx1 = threadRand.Next() % citiesCount;
                        crossOverIdx2 = threadRand.Next() % citiesCount;
                    }
#if DEBUG
                    Console.WriteLine("\tgot crossover ids {0} and {1}", crossOverIdx1, crossOverIdx2);
#endif
                    var childPriority =
                    new LinkedList<Tuple<TravellingSalesman.City, float>>(
                    firstParentPriorities.Take(crossOverIdx1)
                    .Concat(secondParentPriorities.Take(crossOverIdx2).TakeLast(crossOverIdx2 - crossOverIdx1))
                    .Concat(firstParentPriorities.TakeLast(citiesCount - crossOverIdx2)));
#if DEBUG
                    Console.WriteLine("\tgot child priorities");
#endif
                    //Mutation
                    var childPriorityList = PrioritiesMutator.MutatePriorities(childPriority, mutationFactor, T);
#if DEBUG
                    Console.WriteLine("\tdone mutating");
#endif
                    // Streamline mutation so less evaluations
                    return new TravellingSalesman(childPriorityList);
                }));
            }

            return await Task.WhenAll(offsprings);
        }
    }
}