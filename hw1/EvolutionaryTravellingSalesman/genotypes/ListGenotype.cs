using System.Collections.Generic;
using System;
using System.Linq;
namespace EvolutionaryTravellingSalesman
{
    public class ListGenotype : Genotype
    {
        public List<City> Path = new List<City>();

        // Initialize random genotype
        public ListGenotype(IEnumerable<City> cities)
        {
            var rand = new Random();
            Path = new List<City>(cities
                .Zip(
                cities.Select(city => rand.NextDouble()), //create random order
                (city, order) => new { City = city, Order = order }) //create object with order and city
                .OrderBy(obj => obj.Order) //sort by order
                .Select(obj => obj.City)); //get just the city
        }

        public City[] ToPath()
        {
            return Path.ToArray();
        }

    }
}
