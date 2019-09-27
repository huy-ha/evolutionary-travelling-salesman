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
                    var parentGenotype = reproducingPopulation.ElementAt(rand.Next() % reproducingPopulationCount).genotype;
                    string genotypeRepresentation = TravellingSalesman.config.Get(Config.String.Genotype);
                    switch (genotypeRepresentation)
                    {
                        case "List":
                            var childGenotype = SingleSwapMutator.Mutate(parentGenotype as ListGenotype, mutationFactor, T);
                            System.Diagnostics.Debug.Assert(childGenotype.Path.Count() == (parentGenotype as ListGenotype).Path.Count());
                            return new TravellingSalesman(childGenotype);
                        default:
                        case "Priority":
                            throw new Exception("Expected List Genotype");
                    }
                }));
            }

            return await Task.WhenAll(offsprings);
        }
    }
}