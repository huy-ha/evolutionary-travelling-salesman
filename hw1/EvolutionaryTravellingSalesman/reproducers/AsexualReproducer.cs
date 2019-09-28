using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EvolutionaryTravellingSalesman
{
    public class AsexualReproducer : Reproducer<TravellingSalesman>
    {
        int m_populationCount;

        public AsexualReproducer(int populationCount, int numParents = 2)
        {
            m_populationCount = populationCount;
        }

        public async Task<IEnumerable<TravellingSalesman>> Reproduce(IEnumerable<TravellingSalesman> reproducingPopulation, float mutationFactor, float T)
        {
            List<Task<TravellingSalesman>> offsprings = new List<Task<TravellingSalesman>>();
            int reproducingPopulationCount = reproducingPopulation.Count();
            Random rand = new Random();
            while (offsprings.Count() < m_populationCount)
            {
                int x = rand.Next();
                offsprings.Add(Task.Run(() =>
                {
                    //sample a parent
                    var parent = reproducingPopulation.ElementAt(x % reproducingPopulationCount);
                    var parentGenotype = parent.genotype;
                    string genotypeConfig = TravellingSalesman.config.Get(Config.String.Genotype);
                    string mutatorConfig = TravellingSalesman.config.Get(Config.String.Mutator);
                    switch (genotypeConfig)
                    {
                        case "List":
                            switch (mutatorConfig)
                            {
                                case "SingleSwap":
                                    var childGenotype = SingleSwapMutator.Mutate(parentGenotype as ListGenotype, mutationFactor, T);
                                    System.Diagnostics.Debug.Assert(childGenotype.Path.Count() == (parentGenotype as ListGenotype).Path.Count());
                                    return new TravellingSalesman(childGenotype);
                                default:
                                    throw new Exception("Invalid Mutator");
                            }
                        default:
                        case "Priority":
                            throw new Exception("Invalid Genotype");
                    }
                }));
            }

            return await Task.WhenAll(offsprings);
        }
    }
}