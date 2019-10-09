using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
namespace EvolutionaryTravellingSalesman
{
    public static class SelectionCrossover
    {
        public static ListGenotype CrossOver(ListGenotype g1, ListGenotype g2)
        {
            Random rand = new Random();
            int count = g1.Path.Count();
            Debug.Assert(count == g2.Path.Count());

            int[] randomNums = {
                rand.Next() % count,
                rand.Next() % count,
                };
            while (randomNums.Distinct().Count() != randomNums.Length)
            {
                randomNums = new int[] { rand.Next() % count, rand.Next() % count };
            }
            randomNums = randomNums.OrderBy(x => x).ToArray();

            int i1 = randomNums[0];
            int i2 = randomNums[1];

            var selected = g1.Path.Take(i2).TakeLast(i2 - i1).ToArray();
            // get all the cities in the path that isn't in g1's selection
            var others = g2.Path.Where(city => !selected.Any(selectedCity => selectedCity.id == city.id)).ToArray();

            List<City> outputPath = new List<City>();
            for (int i = 0; i < i1; i++)
                outputPath.Add(others[i]);

            for (int i = 0; i < selected.Count(); i++)
                outputPath.Add(selected[i]);

            for (int i = i1; i < others.Count(); i++)
                outputPath.Add(others[i]);
            return new ListGenotype(outputPath);
        }
    }
}
