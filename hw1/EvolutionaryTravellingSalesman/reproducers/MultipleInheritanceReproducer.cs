using System.Collections.Generic;
using System.Linq;

// TODO cross over (single-point, two-point, random)
public class MultipleInheritanceReproducer{
    int m_numParents;
    int m_generationCount;
    System.Random m_rand;
    public MultipleInheritanceReproducer(int generationCount,int numParents = 2){
        m_generationCount = generationCount;
        m_numParents = numParents;
        m_rand = new System.Random();
    }

    public IEnumerable<TravellingSalesman> Reproduce(IEnumerable<TravellingSalesman> parents){
        LinkedList<TravellingSalesman> offsprings = new LinkedList<TravellingSalesman>();
        while(offsprings.Count() < m_generationCount)
        {
            // find m_numParents parents
            
            // find path
        }
        return offsprings;
    }
}