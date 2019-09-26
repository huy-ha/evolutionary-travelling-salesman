using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public static class PrioritiesSingleMutator
    {
        public static void Mutate(TravellingSalesman salesman, float mutationFactor, float T)
        {
            float fitness = -1;
            (salesman.priorities, fitness) = MutatePriorities(salesman.priorities, mutationFactor, T);
            salesman.UpdatePriorities(salesman.priorities);
        }

        public static (LinkedList<Tuple<TravellingSalesman.City, float>>, float) MutatePriorities(LinkedList<Tuple<TravellingSalesman.City, float>> priorities, float mutationFactor, float T)
        {
            int count = priorities.Count();
            Random rand = new Random();
            float oldFitness = TravellingSalesman.Fitness(TravellingSalesman.CalculateCost(priorities));
            var mutatedPriorities = new LinkedList<Tuple<TravellingSalesman.City, float>>(priorities);

            float mutationMagnitude = MathF.Max(T, 0.05f);

            // Find random city to mutate its priorities
            int idx = rand.Next() % count;
            var node = mutatedPriorities.Find(mutatedPriorities.ElementAt(idx));
            var city = node.Value.Item1;
            float priority = node.Value.Item2;  
            float newPriority =
            MathF.Min(
                MathF.Max(
                    priority + ((float)(rand.NextDouble() % 1) - 0.5f) * 0.4f * mutationMagnitude, 0), 1);
            node.Value = new Tuple<TravellingSalesman.City, float>(city, newPriority);
            float newFitness = TravellingSalesman.Fitness(TravellingSalesman.CalculateCost(mutatedPriorities));
            if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
            {
#if DEBUG
                Console.WriteLine("mutated city #{0} from {1} to {2}, resulting in fitness from {3} to {4}", city.id, priority, newPriority, newFitness, oldFitness);
#endif
                return (mutatedPriorities, newFitness);
            }
            return (priorities, oldFitness);
        }
    }

}