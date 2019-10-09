using System.Collections.Generic;
namespace EvolutionaryTravellingSalesman
{
    public interface Selector<T> where T : Phenotype
    {
        IEnumerable<T> Select(IEnumerable<T> population);
    }
}