using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System;

namespace EvolutionaryTravellingSalesman
{
    public static class InsertMutator
    {
        public static ListGenotype Mutate(ListGenotype genotype, float mutationFactor, float T)
        {
            int count = genotype.Path.Count();
            Random rand = new Random();
            float oldFitness = TravellingSalesman.CalculateFitness(genotype);
            var testPath = new List<City>();
            int mutationCount = Math.Max(rand.Next() % Math.Max((int)(mutationFactor * count), 3), 1);
            int[] randomNums = { rand.Next() % count, rand.Next() % count, rand.Next() % count };
            while (randomNums.Distinct().Count() != randomNums.Length)
            {
                randomNums = new int[] { rand.Next() % count, rand.Next() % count, rand.Next() % count };
            }
            randomNums = randomNums.OrderBy(x => x).ToArray();
            int idx1, idx2, insertIdx = -1;
            bool insertBefore = rand.NextDouble() % 1 > 0.5 ? true : false;
            if (insertBefore)
            {
                insertIdx = randomNums[0];
                idx1 = randomNums[1];
                idx2 = randomNums[2];
                for (int i = 0; i < insertIdx; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = idx1; i < idx2; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = insertIdx; i < idx1; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = idx2; i < genotype.Path.Count(); i++)
                    testPath.Add(genotype.Path[i]);
                Debug.Assert(testPath.Count() == genotype.Path.Count());
                Debug.Assert(testPath.Distinct().Count() == genotype.Path.Count());
            }
            else
            {
                idx1 = randomNums[0];
                idx2 = randomNums[1];
                insertIdx = randomNums[2];
                for (int i = 0; i < idx1; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = idx2; i < insertIdx; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = idx1; i < idx2; i++)
                    testPath.Add(genotype.Path[i]);
                for (int i = insertIdx; i < genotype.Path.Count(); i++)
                    testPath.Add(genotype.Path[i]);
                Debug.Assert(testPath.Count() == genotype.Path.Count());
                Debug.Assert(testPath.Distinct().Count() == genotype.Path.Count());
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