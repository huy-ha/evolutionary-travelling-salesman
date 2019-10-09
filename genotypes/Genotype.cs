using System.Collections.Generic;

namespace EvolutionaryTravellingSalesman
{
    public interface Genotype
    {
        City[] ToPath();
    }
}
