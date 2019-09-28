# Travelling Salesman Problem using Genetic Algorithms

| Evolutionary Computation and Automated Design (MECS E4510) | Hod Lipson |
| ---------------------------------------------------------- | ---------- |


| Name: Huy Ha | UNI: hqh2101 | Date Submitted: September 29th 2019 | No Grace Hour Used (96 hours left) |
| ------------ | ------------ | ----------------------------------- | ---------------------------------- |


## Results

|   Run # | Approach Name                                | Shortest Path | Longest Path | Notes |
| ------: | :------------------------------------------- | ------------: | -----------: | :---- |
|      11 | Multiple Inheritance (Normalized Priorities) |           N/a |          N/a | N/a   |
| 18 & 19 | Asexual, Swap, No Annealing                  |      74.63085 |          N/a |       |

As can be seen from the table above, my most sucessful implementation was run #TODO named TODO. The plot of the path can be seen below

## Methods

In run #11, I tried using the `MultipleInheritanceReproducer` again, this time, assuring that the city priorities are normalized (the intuition was that if the entire population had the same starting points, then crossing over priorities would be more meaningful). This allowed me to achieve the exponential decay that I wanted (see graph below). This run used the ElitesAnnealingTSPSolver, and you can see clearly in the graph how the worst TSPs were approaching the average TSP of the population as T decayed. Moving forward from here, I think I want to explore running this for more generations, but with a smaller population (for speed concerns). In order to battle with the lower of diversity due to a decrease in population count, I will allow the temperature to decay more slowly.

TODO: Insert image from run 11

In run #14, I tried running my algorithm on the circle test ussing the AsexualReproducer, which takes one parent, performs one or more swaps on the priorities. This method converged too quickly, and this was when I realized I needed some diversity operators.

## Performance Plots

## Appendix
