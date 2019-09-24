using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    // TODO cross over (single-point, two-point, random)
    public class MultipleInheritanceReproducer<T> : Reproducer<T> where T : Phenotype
    {
        int m_numParents;
        int m_generationCount;
        System.Random m_rand;
        public MultipleInheritanceReproducer(int generationCount, int numParents = 2)
        {
            m_generationCount = generationCount;
            m_numParents = numParents;
            m_rand = new System.Random();
        }

        public IEnumerable<T> Reproduce(IEnumerable<T> parents)
        {
            LinkedList<T> offsprings = new LinkedList<T>();
            while (offsprings.Count() < m_generationCount)
            {
                // find m_numParents parents

                // find path
                // IEnumerable<TravellingSalesman> offspring = await Task.WhenAll(
                // parents.Select(parent => Task.Run(() =>
                //  new TravellingSalesman(parent, T, findShortestPath))));
                // while (offspring.Count() < populationCount)
                // {
                //     offspring = offspring.Concat(                             // 4. Concatenate newly created children with previous children
                //         await Task.WhenAll(                                   // 3. Wait until all constructions of children is finished
                //             parents.Select(
                //                 parent => Task.Run(() =>                      // 2. Run the constructor asynchronously 
                //                 new TravellingSalesman(parent, T, findShortestPath))).ToArray())); // 1. From each parent, create a new TravellingSalesman
                // }
            }
            return offsprings;
        }
    }
}