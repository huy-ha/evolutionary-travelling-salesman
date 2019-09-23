using System.Collections.Generic;
using System.Linq;
using System;

public class SimulatedAnnealingSelected
{
    private float m_beta;
    private float m_initT;
    private float m_T;
    private float m_reproductionPercentage;
    public SimulatedAnnealingSelected(float beta = 0.001f, float initT = 100, float reproductionPercentage = 0.4f)
    {
        m_beta = beta;
        m_initT = initT;
        m_T = m_initT;
        m_reproductionPercentage = reproductionPercentage;
    }

    public void Reset()
    {
        m_T = m_initT;
    }

    //Stochastic Universal Sampling based on pseudocode from https://en.wikipedia.org/wiki/Stochastic_universal_sampling
    public
    // (IEnumerable<TravellingSalesman>,IEnumerable<TravellingSalesman>)
    IEnumerable<TravellingSalesman>
     Select(IEnumerable<TravellingSalesman> salesmen, float elitistPercentage = 0.1f, bool findShortestPath = true)
    {
        if (findShortestPath)
        {
            return salesmen.OrderBy(salesman => salesman.Cost).Take((int)(salesmen.Count() * m_reproductionPercentage));
        }
        else
        {
            return salesmen.OrderByDescending(salesman => salesman.Cost).Take((int)(salesmen.Count() * m_reproductionPercentage));
        }

        // // Calculate Population Variables
        // var fitnessSum = salesmen.Sum(salesman => salesman.Fitness);
        // var count = salesmen.Count();
        // int eliteCount = (int) (count * elitistPercentage);
        // int reproductionCount = count - eliteCount;

        // // Calculate variables for SUS
        // float distBetweenPtr = fitnessSum/reproductionCount;
        // var rand = new Random();
        // float start = rand.Next() % distBetweenPtr;

        // salesmen.OrderBy(salesman => salesman.Fitness);
        // if(!ascending) salesmen.Reverse();

        // LinkedList<TravellingSalesman> parents = new LinkedList<TravellingSalesman>();
        // for(int i = 0; i < reproductionCount; i++) {
        //     float selectionFitness = start + distBetweenPtr * i;
        //     float currentFitnessSum = 0;
        //     foreach(var salesman in salesmen){
        //         currentFitnessSum += salesman.Fitness;
        //         if(currentFitnessSum > selectionFitness){
        //             parents.AddLast(salesman);
        //             break;
        //         }
        //     }
        // }
        // return ( 
        //     ascending ? salesmen.TakeLast(eliteCount) : salesmen.Take(eliteCount),
        //     parents
        // );
    }
}