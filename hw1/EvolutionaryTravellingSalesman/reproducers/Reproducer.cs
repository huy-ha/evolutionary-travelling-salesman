using System.Collections.Generic;
namespace EvolutionaryTravellingSalesman
{
    public interface Reproducer<T> where T : Phenotype
    {
        IEnumerable<T> Reproduce(IEnumerable<T> parents);
    }
}