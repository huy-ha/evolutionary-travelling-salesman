using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public static class PrioritiesMutator
    {
        public static void Mutate(TravellingSalesman salesman, float mutationFactor, float T)
        {
            float mutatedFitness = MutatePriorities(salesman.priorities, mutationFactor, T);
            salesman.UpdatePriorities(salesman.priorities, mutatedFitness);
        }

        public static float MutatePriorities(LinkedList<Tuple<TravellingSalesman.City, float>> priorities, float mutationFactor, float T)
        {
            int count = priorities.Count();
            Random rand = new Random();
            float oldFitness = TravellingSalesman.Fitness(TravellingSalesman.CalculateCost(priorities));
            var mutatedPriorities = new LinkedList<Tuple<TravellingSalesman.City, float>>(priorities);
            int mutationCount = rand.Next() % (int)(mutationFactor * count);
            //TODO code both multiple and single mutation
            for (int mutation = 0; mutation < mutationCount; mutation++)
            {
                int idx = rand.Next() % count;
                var node = mutatedPriorities.Find(mutatedPriorities.ElementAt(idx));
                var city = node.Value.Item1;
                float priority = node.Value.Item2;
                float newPriority =
                MathF.Min(
                    MathF.Max(
                        priority + ((float)rand.NextDouble() % 1 - 0.5f) * 0.1f, 0), 1);
                node.Value = new Tuple<TravellingSalesman.City, float>(city, newPriority);
            }
            float newFitness = TravellingSalesman.Fitness(TravellingSalesman.CalculateCost(mutatedPriorities));
            if (newFitness > oldFitness || rand.NextDouble() % 1 < T)
            {
                priorities = mutatedPriorities;
                return newFitness;
            }
            return oldFitness;
        }
    }

}