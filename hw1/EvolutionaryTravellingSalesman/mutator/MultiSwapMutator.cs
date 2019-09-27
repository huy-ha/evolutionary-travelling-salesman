using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public static class MultiSwapMutator
    {
        public static ListGenotype Mutate(ListGenotype genotype, float mutationFactor, float T)
        {
            int count = genotype.Path.Count();
            Random rand = new Random();
            float oldFitness = TravellingSalesman.CalculateFitness(genotype);
            var testPath = new List<City>(genotype.Path);
            int mutationCount = Math.Max(rand.Next() % Math.Max((int)(mutationFactor * count), 3), 1);

            for (int mutation = 0; mutation < mutationCount; mutation++)
            {
                int idx1 = rand.Next() % count;
                int idx2 = rand.Next() % count;
                while (idx2 == idx1)
                {
                    idx1 = rand.Next() % count;
                    idx2 = rand.Next() % count;
                }
                var tmp = testPath[idx1];
                testPath[idx1] = testPath[idx2];
                testPath[idx2] = tmp;
            }
            float newFitness = TravellingSalesman.CalculateFitness(testPath.ToArray());
            if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
            {
                // keep mutation
                return new ListGenotype(testPath);
            }
            else
            {
                //swap back
                return genotype;
            }
        }
    }
}