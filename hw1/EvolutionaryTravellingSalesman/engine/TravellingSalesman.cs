
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

        public Genotype genotype;

        private City[] m_path = null;

        public static Config config = null;
        public static int cityCount = -1;
        #endregion

        public static int evaluations = 0;

        public TravellingSalesman(IEnumerable<City> cities)
        {
            if (config == null)
                throw new System.Exception("Travelling Salesman not configured");
            m_rand = new Random();
            switch (config.Get(Config.String.Genotype))
            {
                case "Priority":
                    genotype = new PriorityGenotype(cities);
                    break;
                case "List":
                default:
                    genotype = new ListGenotype(cities);
                    break;
            }
            m_path = genotype.ToPath();
            CalculateCost();
            cityCount = cities.Count();
        }

        public TravellingSalesman(Genotype genotype)
        {
            this.genotype = genotype;
            m_path = this.genotype.ToPath();
            CalculateCost();
        }

        public float Fitness()
        {
            return FitnessToCost(Cost);
        }

        public static float FitnessToCost(float fitness)
        {
            return config.Get(Config.Bool.Optimize) ? 1 / fitness : fitness;
        }

        public static float CostToFitness(float cost)
        {
            return config.Get(Config.Bool.Optimize) ? 1 / cost : cost;
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

        public static float CalculateFitness(City[] path)
        {
            return CostToFitness(CalculateCost(path));
        }

        public static float CalculateFitness(Genotype genotype)
        {
            return CostToFitness(CalculateCost(genotype));
        }

        private float CalculateCost()
        {
            if (m_path == null)
                m_path = genotype.ToPath();
            Cost = CalculateCost(m_path);
            return Cost;
        }

        public static float CalculateCost(Genotype genotype)
        {
            return CalculateCost(genotype.ToPath());
        }

        public override string ToString()
        {
            return "" + Fitness();
        }

        public string PrintPath()
        {
            if (m_path == null)
                m_path = genotype.ToPath();
            return string.Join("|", (object[])m_path);
        }
    }
}