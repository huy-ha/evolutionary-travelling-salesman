using System;
using System.Collections.Generic;
using System.Linq;

public class TravellingSalesman
{
    #region Variables
    private Random m_rand;
    public float Cost
    {
        get;
        protected set;
    }

    public float Fitness
    {
        get
        {
            return 1000 / Cost;
        }
    }

    private List<City> m_path;
    #endregion

    public TravellingSalesman(IEnumerable<City> cities)
    {
        m_path = FindRandomPath(cities);
        CalculateCost();
        m_rand = new Random();
    }

    public TravellingSalesman(TravellingSalesman parent, float T, bool findShortestPath, float maxMutationFactor = 0.3f)
    {
        m_path = new List<City>(parent.m_path);
        m_rand = new Random();
        Cost = parent.Cost;
        int count = m_path.Count();
        int maxMutations = Math.Max((int)(count * maxMutationFactor), 1);
        int mutations = m_rand.Next() % maxMutations;
        for (int i = 0; i < mutations; i++)
        {
            Mutate(T, count, findShortestPath);
        }
        CalculateCost();
    }

    private void Mutate(float T, int count, bool findShortestPath)
    {
        var testPath = new List<City>(m_path);
        Swap(testPath, m_rand.Next() % count, m_rand.Next() % count);
        float testPathCost = CalculateCost(testPath);
        if ((findShortestPath ? testPathCost < Cost : testPathCost > Cost) || m_rand.NextDouble() < T)
        {
            m_path = testPath;
            Cost = testPathCost;
        }
    }

    private void Swap(List<City> path, int i1, int i2)
    {
        var tmp = path[i1];
        path[i1] = path[i2];
        path[i2] = tmp;
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
        Cost = CalculateCost(m_path);
        return Cost;
    }

    public override string ToString()
    {
        return "" + Fitness;
    }

    public string PrintPath()
    {
        return string.Join("|", m_path);
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
