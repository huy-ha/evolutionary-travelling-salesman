using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
namespace EvolutionaryTravellingSalesman
{
    public static class PriorityCrossOver
    {
        public static IEnumerable<PriorityGenotype> CrossOver(PriorityGenotype p1, PriorityGenotype p2)
        {
            Random rand = new Random();
            int citiesCount = p1.Priorities.Count();
            Debug.Assert(citiesCount == p2.Priorities.Count());

            int crossOverIdx1 = rand.Next() % citiesCount;
            int crossOverIdx2 = rand.Next() % citiesCount;
            while (crossOverIdx1 >= crossOverIdx2)
            {
                crossOverIdx1 = rand.Next() % citiesCount;
                crossOverIdx2 = rand.Next() % citiesCount;
            }
            var child1 =
                new PriorityGenotype(
                    PriorityGenotype.NormalizePriorities(
                        p1.Priorities.Take(crossOverIdx1)
                        .Concat(p2.Priorities.Take(crossOverIdx2).TakeLast(crossOverIdx2 - crossOverIdx1))
                        .Concat(p1.Priorities.TakeLast(citiesCount - crossOverIdx2))));
            var child2 =
                new PriorityGenotype(
                    PriorityGenotype.NormalizePriorities(
                        p2.Priorities.Take(crossOverIdx1)
                        .Concat(p1.Priorities.Take(crossOverIdx2).TakeLast(crossOverIdx2 - crossOverIdx1))
                        .Concat(p2.Priorities.TakeLast(citiesCount - crossOverIdx2))));
            return new List<PriorityGenotype>() { child1, child2 };
        }
    }
}
