# Travelling Salesman Problem using Genetic Algorithms

## Approach

In run #11, I tried using the MuttipleInheritanceReproducer again, this time, assuring that the city priorities are normalized (the intuition was that if the entire population had the same starting points, then crossing over priorities would be more meaningful). This allowed me to achieve the exponential decay that I wanted (see graph below). This run used the ElitesAnnealingTSPSolver, and you can see clearly in the graph how the worst TSPs were approaching the average TSP of the population as T decayed. Moving forward from here, I think I want to explore running this for more generations, but with a smaller population (for speed concerns). In order to battle with the lower of diversity due to a decrease in population count, I will allow the temperature to decay more slowly.

TODO: Insert image from run 11
