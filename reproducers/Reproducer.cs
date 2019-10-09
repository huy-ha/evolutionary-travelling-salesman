using System.Collections.Generic;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    public interface Reproducer<T> where T : Phenotype
    {
        Task<IEnumerable<T>> Reproduce(IEnumerable<T> reproducingPopulation, float mutationFactor, float T);
    }
}