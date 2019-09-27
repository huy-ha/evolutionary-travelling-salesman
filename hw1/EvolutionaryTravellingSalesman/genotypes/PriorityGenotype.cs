using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public class PriorityGenotype : Genotype
    {
        public LinkedList<Tuple<City, float>> Priorities =
        new LinkedList<Tuple<City, float>>();
        // Initialize random genotype
        public PriorityGenotype(IEnumerable<City> cities)
        {
            Random rand = new Random();
            Priorities = new LinkedList<Tuple<City, float>>(
                                NormalizePriorities(
                                cities
                                .Select(city => new Tuple<City, float>(city, (float)(rand.NextDouble() % 1)))));
        }

        public PriorityGenotype(IEnumerable<Tuple<City, float>> Priorities)
        {
            this.Priorities = new LinkedList<Tuple<City, float>>(Priorities);
        }

        public static IEnumerable<Tuple<City, float>> NormalizePriorities(IEnumerable<Tuple<City, float>> unnormalizedPriorities)
        {
            // Normalize all priorities s.t. this city is has highest priority
            var cityWithPriority = unnormalizedPriorities.First(pair => pair.Item1.id == 0);
            return unnormalizedPriorities.Select(
                pair =>
                new Tuple<City, float>(
                    pair.Item1,
                    1 - (pair.Item2 % cityWithPriority.Item2)));
        }

        public City[] ToPath()
        {
            return Priorities
                        .OrderByDescending(pair => pair.Item2)
                        .Select(pair => pair.Item1).ToArray();
        }
    }
}
