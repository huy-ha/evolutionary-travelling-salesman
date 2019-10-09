using System;
using System.Collections.Generic;

namespace EvolutionaryTravellingSalesman
{

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

        // DO NOT CHANGE BECAUSE OUTPUT USES THIS
        public override string ToString()
        {
            return m_x + " " + m_y;
        }
    }
}