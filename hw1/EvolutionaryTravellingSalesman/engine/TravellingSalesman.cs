using System;
using System.Collections.Generic;
using System.Linq;
namespace EvolutionaryTravellingSalesman
{
    public class TravellingSalesman : Phenotype
    {
        #region Variables
        private Random m_rand;
        public float Cost
        {
            get;
            protected set;
        }
        private bool m_findShortestPath;

        public List<City> path;
        #endregion

        public TravellingSalesman(bool findShortestPath, IEnumerable<City> cities)
        {
            m_findShortestPath = findShortestPath;
            m_rand = new Random();
            path = FindRandomPath(cities);
            CalculateCost();
        }

        public TravellingSalesman(List<City> initPath, float initCost = -1)
        {
            path = initPath;
            m_rand = new Random();
            if (initCost == -1)
                CalculateCost();
        }

        public float Fitness()
        {
            return m_findShortestPath ? 1000 / Cost : Cost;
        }

        private List<City> FindRandomPath(IEnumerable<City> cities)
        {
            return new List<City>(cities
                .Zip(
                cities.Select(city => m_rand.NextDouble()), //create random order
                (city, order) => new { City = city, Order = order }) //create object with order and city
                .OrderBy(obj => obj.Order) //sort by order
                .Select(obj => obj.City)); //get just the city
        }

        private float CalculateCost(List<City> path)
        {
            float cost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                cost += City.Distance(path[i], path[i + 1]);
            }
            return cost;
        }

        private float CalculateCost()
        {
            Cost = CalculateCost(path);
            return Cost;
        }

        public override string ToString()
        {
            return "" + Fitness();
        }

        public string PrintPath()
        {
            return string.Join("|", path);
        }

        public class City
        {
            private float m_x = 0;
            private float m_y = 0;

            public City(float x, float y)
            {
                m_x = x;
                m_y = y;
            }

            public static float Distance(City city1, City city2)
            {
                float dx = city1.m_x - city2.m_x;
                float dy = city1.m_y - city2.m_y;
                return MathF.Sqrt(dx * dx + dy * dy);
            }

            public override string ToString()
            {
                return m_x + " " + m_y;
            }
        }
    }
}