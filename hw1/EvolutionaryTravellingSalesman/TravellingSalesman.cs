using System;
using System.Collections.Generic;
using System.Linq;

public class TravellingSalesman
{
    #region Variables
    public float Cost
    {
        get;
        protected set;
    }

    public float Fitness {
        get {
            return 1000/Cost;
        }
    }

    private List<City> m_path;
    private Random m_rand;
    #endregion

	public TravellingSalesman(IEnumerable<City> cities)
	{
        m_path = FindRandomPath(cities);
        CalculateCost();
        m_rand = new Random();
	}

    public TravellingSalesman(TravellingSalesman parent,float T,float mutationFactor=0.3f){
        m_path = new List<City>(parent.m_path);
        m_rand = new Random();
        Cost = parent.Cost;
        int count = m_path.Count(); 
        int mutationTrajectoryLength = m_rand.Next() % (int)(count * 0.1f);
        Mutate(10,Math.Max((int) (count*mutationFactor),1),T);
    }

    private void Mutate(int mutations,int attempts, float T){
        var rand = new Random();
        int count = m_path.Count(); 
        for(int attempt = 0; attempt < attempts; attempt++){
            var testPath = new List<City>(m_path);
            for(int i = 0; i < mutations;i++){
                int idx1 = rand.Next()%count;
                int idx2 = rand.Next()%count;
                Swap(testPath,idx1,idx2);
            }
            float testPathCost = CalculateCost(testPath);
            if(testPathCost < Cost || rand.NextDouble() < T){
                m_path = testPath;
                Cost = testPathCost;
            }
        }
    }

    //Swap two cities in path at idx i1 and i2
    private void Swap(List<City> path, int i1, int i2){
        var tmp = m_path[i1];
        m_path[i1] = m_path[i2];
        m_path[i2] = tmp;
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

    private float CalculateCost(List<City> path){
        float cost = 0;
        for(int i = 0; i < path.Count - 1; i++)
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

    public override string ToString(){
        return ""+Fitness;
    }

    public string PrintPath(){
        return string.Join("|",m_path);
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
