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
            var outputPath = new List<City>(genotype.Path);
            if (TravellingSalesman.config.Get(Config.String.Reproducer) == "Asexual")
            {
                // Because in asexual reproduction, mutation is only variation
                int mutationCount = Math.Max(rand.Next() % Math.Max((int)(mutationFactor * count), 3), 1);
                for (int i = 0; i < mutationCount; i++)
                {
                    var testPath = InsertMutate(outputPath, rand, count);
                    float newFitness = TravellingSalesman.CalculateFitness(testPath.ToArray());
                    if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
                    {
                        //take new mutation
                        outputPath = new List<City>(testPath);
                        oldFitness = newFitness;
                    }
                }
            }
            else
            {
                if ((rand.NextDouble() % 1) < mutationFactor)
                {
                    var testPath = InsertMutate(outputPath, rand, count);
                    float newFitness = TravellingSalesman.CalculateFitness(testPath.ToArray());
                    if (newFitness > oldFitness || (rand.NextDouble() % 1) < T)
                    {
                        //take new mutation
                        outputPath = new List<City>(testPath);
                        oldFitness = newFitness;
                    }
                }
            }
            return new ListGenotype(outputPath);
        }

        private static List<City> InsertMutate(List<City> input, Random rand, int count)
        {
            var testPath = new List<City>();
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
                    testPath.Add(input[i]);
                for (int i = idx1; i < idx2; i++)
                    testPath.Add(input[i]);
                for (int i = insertIdx; i < idx1; i++)
                    testPath.Add(input[i]);
                for (int i = idx2; i < input.Count(); i++)
                    testPath.Add(input[i]);
                Debug.Assert(testPath.Count() == input.Count());
                Debug.Assert(testPath.Distinct().Count() == input.Count());
            }
            else
            {
                idx1 = randomNums[0];
                idx2 = randomNums[1];
                insertIdx = randomNums[2];
                for (int i = 0; i < idx1; i++)
                    testPath.Add(input[i]);
                for (int i = idx2; i < insertIdx; i++)
                    testPath.Add(input[i]);
                for (int i = idx1; i < idx2; i++)
                    testPath.Add(input[i]);
                for (int i = insertIdx; i < input.Count(); i++)
                    testPath.Add(input[i]);
                Debug.Assert(testPath.Count() == input.Count());
                Debug.Assert(testPath.Distinct().Count() == input.Count());
            }
            return testPath;
        }
    }
}