using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public static class SingleSwapMutator
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
            var testPriorities = new LinkedList<Tuple<TravellingSalesman.City, float>>(priorities);
            int mutationCount = rand.Next() % (int)(mutationFactor * count);
            //TODO code both multiple and single mutation
            for (int mutation = 0; mutation < mutationCount; mutation++)
            {
                int idx1 = rand.Next() % count;
                int idx2 = rand.Next() % count;
                while (idx2 == idx1)
                {
                    idx1 = rand.Next() % count;
                    idx2 = rand.Next() % count;
                }
                var node1 = testPriorities.Find(testPriorities.ElementAt(idx1));
                var node2 = testPriorities.Find(testPriorities.ElementAt(idx2));
                var val1 = node1.Value;
                var val2 = node2.Value;
                node1.Value = new Tuple<TravellingSalesman.City, float>(val1.Item1, val2.Item2);
                node2.Value = new Tuple<TravellingSalesman.City, float>(val2.Item1, val1.Item2);
                float newFitness = TravellingSalesman.Fitness(TravellingSalesman.CalculateCost(testPriorities));
                if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
                {
                    // keep mutation
                    mutatedPriorities = testPriorities;
                    oldFitness = newFitness;
                }
                else
                {
                    //swap back
                    testPriorities = new LinkedList<Tuple<TravellingSalesman.City, float>>(mutatedPriorities);
                }
            }
            return (mutatedPriorities, oldFitness);
        }
    }

}