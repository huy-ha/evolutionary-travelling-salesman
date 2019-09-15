using System.Collections.Generic;
using System.Linq;
using System;

public class SimulatedAnnealingSelected
{
    private float m_beta; 
    private float m_initT;
    private float m_T;
    private float m_elitistPercentage;
    private float m_reproductionPercentage; 
    public SimulatedAnnealingSelected(float beta = 0.001f,float initT=100,float elitistPercentage = 0.01f, float reproductionPercentage = 0.6f){
        m_beta = beta;
        m_initT = initT;
        m_T = m_initT;
        m_elitistPercentage = elitistPercentage;
        m_reproductionPercentage = reproductionPercentage;
    }

    public void Reset(){
        m_T = m_initT;
    }

    //Stochastic Universal Sampling based on pseudocode from https://en.wikipedia.org/wiki/Stochastic_universal_sampling
    public IEnumerable<TravellingSalesman> Select(IEnumerable<TravellingSalesman> salesmen,bool ascending = true){
        // Calculate variables for SUS
        var populationCostSum = salesmen.Sum(salesman => salesman.Cost);
        var count = salesmen.Count();
        int elitistCount = (int) (count * m_elitistPercentage);
        int reproductionCount = (int) (count * m_reproductionPercentage);
        float distBetweenPtr = populationCostSum/reproductionCount;
        var rand = new Random();
        float start = rand.Next() % distBetweenPtr;

        // Sort Enumerable in ascending order
        salesmen.OrderBy(salesman => salesman.Cost);
        if(!ascending) salesmen.Reverse();
        
        LinkedList<float> ptrs = new LinkedList<float>();
        for(int i = 0; i < reproductionCount; i++) ptrs.AddLast(start + distBetweenPtr * i);
        
        var currPtr = ptrs.First;
        float sum = 0;
        // Return salesmen chosen to reproduce for next generation
        return salesmen.Where(salesman => {
            sum += salesman.Cost;
            bool toSelect = sum > currPtr.Value;
            if(toSelect){
                currPtr = currPtr.Next;
                return true;
            }
            return false;
        });
    }
}