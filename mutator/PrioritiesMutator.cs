using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public static class PrioritiesMutator
    {
        public static PriorityGenotype MutatePriorities(PriorityGenotype genotype, float mutationFactor, float T)
        {
            int count = genotype.Priorities.Count();
            Random rand = new Random();
            float oldFitness = TravellingSalesman.CalculateFitness(genotype);
            var mutatedGenotype = new PriorityGenotype(genotype.Priorities);
            int mutationCount = rand.Next() % (int)(mutationFactor * count);
            float mutationMagnitude = MathF.Max(T, 0.05f);

            for (int mutation = 0; mutation < mutationCount; mutation++)
            {
                int idx = rand.Next() % count;
                var node = mutatedGenotype.Priorities.Find(mutatedGenotype.Priorities.ElementAt(idx));
                var city = node.Value.Item1;
                float priority = node.Value.Item2;
                float newPriority =
                MathF.Min(
                    MathF.Max(
                        priority + ((float)(rand.NextDouble() % 1) - 0.5f) * 0.2f * mutationMagnitude, 0), 1);
                node.Value = new Tuple<City, float>(city, newPriority);
            }
            float newFitness = TravellingSalesman.CalculateFitness(mutatedGenotype);
            if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
            {
                return mutatedGenotype;
            }
            return genotype;
        }
    }
}