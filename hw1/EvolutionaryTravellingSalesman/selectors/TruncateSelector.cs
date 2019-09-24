using System.Collections.Generic;
using System.Linq;
namespace EvolutionaryTravellingSalesman
{
    public class TruncateSelector<T> : Selector<T> where T : Phenotype
    {
        float m_reproductionPercentage = -1;
        public TruncateSelector(float reproductionPercentage)
        {
            m_reproductionPercentage = reproductionPercentage;
        }
        public IEnumerable<T> Select(IEnumerable<T> population)
        {
            return population
            .OrderBy(individual => individual.Fitness())
            .Take((int)(population.Count() * m_reproductionPercentage));

        }
    }
}