using System;
using System.Collections.Generic;
using System.Linq;

public class TravellingSalesman
{
    public float Cost
    {
        get;
        protected set;
    }

    private List<City> m_path;

	public TravellingSalesman(IEnumerable<City> cities)
	{
        m_path = FindRandomPath(cities);
        CalculateCost();
	}

    private List<City> FindRandomPath(IEnumerable<City> cities)
    {
        var rand = new Random();
        return new List<City>(cities
            .Zip(
            cities.Select(city => rand.NextDouble()), //create random order
            (city, order) => new {City=city,Order=order}) //create object with order and city
            .OrderBy(obj => obj.Order) //sort by order
            .Select(obj => obj.City)); //get just the city
    }

    private void CalculateCost()
    {
        Cost = 0;
        for(int i = 0; i < m_path.Count - 1; i++)
        {
            Cost += City.Distance(m_path[i], m_path[i + 1]);
        }
    }

    public override string ToString(){
        return ""+Cost;
    }

    public class City
    {
        private float m_x = 0;
        private float m_y = 0;

        public City(float x, float y) {
            m_x = x;
            m_y = y;
        }

        public static float Distance(City city1,City city2)
        {
            float dx = city1.m_x - city2.m_x;
            float dy = city1.m_y - city2.m_y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString(){
            return m_x + " " + m_y;
        }
    }
}
