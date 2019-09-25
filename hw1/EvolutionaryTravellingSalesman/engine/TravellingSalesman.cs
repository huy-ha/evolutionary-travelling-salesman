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

        public LinkedList<Tuple<City, float>> priorities = new LinkedList<Tuple<City, float>>();
        private City[] m_path = null;

        public static Config config = null;
        #endregion

        public static int evaluations = 0;

        public TravellingSalesman(IEnumerable<City> cities)
        {
            if (config == null)
                throw new System.Exception("Travelling Salesman not configured");
            m_rand = new Random();
            priorities = new LinkedList<Tuple<City, float>>(
                NormalizePriorities(
                    cities
                    .Select(city => new Tuple<City, float>(city, (float)(m_rand.NextDouble() % 1)))));
            m_path = ToPath(priorities);
            CalculateCost();
        }

        public TravellingSalesman(IEnumerable<Tuple<City, float>> initPriorities, float initFitness = -1)
        {
            if (config == null)
                throw new System.Exception("Travelling Salesman not configured");
            priorities = new LinkedList<Tuple<City, float>>(NormalizePriorities(initPriorities));
            m_rand = new Random();
            UpdatePriorities(priorities, initFitness);
        }

        public IEnumerable<Tuple<City, float>> NormalizePriorities(IEnumerable<Tuple<City, float>> unnormalizedPriorities)
        {
            // Normalize all priorities s.t. this city is has highest priority
            var cityWithPriority = unnormalizedPriorities.First(pair => pair.Item1.id == 0);
            return unnormalizedPriorities.Select(pair => new Tuple<City, float>(pair.Item1, 1 - (pair.Item2 % cityWithPriority.Item2)));
        }

        public void UpdatePriorities(LinkedList<Tuple<City, float>> priorities, float initFitness = -1)
        {
            this.priorities = priorities;
            if (m_path == null)
                m_path = ToPath(priorities);
            if (initFitness == -1)
                CalculateCost();
            else
                Cost = FitnessToCost(Cost);
        }

        public float Fitness()
        {
            return Fitness(Cost);
        }

        public static float Fitness(float cost)
        {
            return config.Get(Config.Bool.Optimize) ? 1000 / cost : cost;
        }

        public static float FitnessToCost(float fitness)
        {
            return config.Get(Config.Bool.Optimize) ? 1000 / fitness : fitness;
        }

        public static float CalculateCost(City[] path)
        {
            float cost = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                cost += City.Distance(path[i], path[i + 1]);
            }
            evaluations++;
            return cost;
        }

        private float CalculateCost()
        {
            if (m_path == null)
                m_path = ToPath(priorities);
            Cost = CalculateCost(m_path);
            return Cost;
        }

        public static float CalculateCost(IEnumerable<Tuple<City, float>> priorities)
        {
            return CalculateCost(ToPath(priorities));
        }

        public static City[] ToPath(IEnumerable<Tuple<City, float>> priorities)
        {
            return priorities
            .OrderByDescending(pair => pair.Item2)
            .Select(pair => pair.Item1).ToArray();
        }

        public override string ToString()
        {
            return "" + Fitness();
        }

        public string PrioritiesToString()
        {
            return PrioritiesToString(priorities);
        }

        public static string PrioritiesToString(IEnumerable<Tuple<City, float>> priorities)
        {
            return string.Join("|", priorities.OrderBy(pair => pair.Item1.id).Select(pair => "(" + pair.Item1.ToString() + ":" + pair.Item2 + ")"));
        }

        public string PrintPath()
        {
            if (m_path == null)
                m_path = ToPath(priorities);
            return string.Join("|", (object[])m_path);
        }

        public class City
        {
            private float m_x = 0;
            private float m_y = 0;

            static List<float> _ids = new List<float>();

            public float idKey
            {
                get => (m_x * 10) + (m_y * 3) * (m_y * 3);
            }

            public int id
            {
                get => _ids.IndexOf(idKey);
            }

            public City(float x, float y)
            {
                m_x = x;
                m_y = y;
                if (!_ids.Contains(idKey))
                {
                    _ids.Add(idKey);
                }
            }

            public static float Distance(City city1, City city2)
            {
                float dx = city1.m_x - city2.m_x;
                float dy = city1.m_y - city2.m_y;
                return MathF.Sqrt(dx * dx + dy * dy);
            }

            public override string ToString()
            {
                return "" + id;
            }
        }
    }
}