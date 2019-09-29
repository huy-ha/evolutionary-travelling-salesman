# Travelling Salesman Problem using Genetic Algorithms

## Evolutionary Computation and Automated Design (MECS E4510) with professor Hod Lipson

Name: Huy Ha

UNI: hqh2101

Date Submitted: September 29th 2019

No Grace Hour Used (96 hours left)

---

# Grading Helper:

## General

1.  [Cover page](<#Travelling Salesman Problem using Genetic Algorithms>)
2.  [Summary result table showing all information requested](<#Summary Results Table>)
3.  `Dot plot for any one of the methods (not all required)`
4.  `Convergence plot for any one of some methods (not all)`
5.  `Code included (8pt courier single spacing)`
6.  `Theoretical shortest path using Christofides' algorithm`
7.  `Movie of optimizing path (one frame every time path improves)`
8.  `learning curves clearly labeled, have error bars, labeled axes`
9.  `Shortest path Overall performance (based on distance, evaluations)`
10. `Longest path Overall performance (based on distance, evaluations)`

## Methods

1. [Description of Representation Used](#Representations)
2. [Description of Random Search](<#Random Search>)
3. `Description of Hill Climber`
4. `Description of EA variation and selection methods used`
5. `Analysis of Performance (What worked and what didn't)`
6. `Two methods compared (bar chart)`

## Performance Curves

1. [Shortest Path Learning Curve of random search](<#Shortest Path Learning Curve of Random Search>) `TODO: error bars`
2. `Shortest Path Learning Curve of hill climber`
3. `Shortest Path Learning Curve of EA and some variation of EA`
4. [Longest Path Learning Curve of random search](<#Longest Path Learning Curve of Random Search>)
5. `Longest Path Learning Curve of hill climber`
6. `Longest Path Learning Curve of EA and some variation of EA`

## Results

#### Summary Results Table

| Run # | Reproduction  | Mutation    | Annealing      | Population | Genotype | Optimize | Longest Path |
| ----: | :------------ | :---------- | :------------- | ---------: | -------: | -------: | :----------- |
|    18 | Asexual       | Single Swap | No             |        100 |     List | Shortest | 74.63085     |
|    19 | Asexual       | Single Swap | No             |        100 |     List |  Longest | 760.8126     |
|    20 | Asexual       | Single Swap | Yes (init 0.5) |        100 |     List | Shortest | 74.58129     |
|    21 | Asexual       | Insert      | Yes (init 0.5) |         50 |     List | Shortest | 47.86598     |
|    22 | Asexual       | Insert      | Yes (init 0.5) |        100 |     List | Shortest | 41.14663     |
|    23 | Asexual       | Insert      | Yes (init 0.5) |        200 |     List | Shortest | 36.29556     |
|    24 | Random Search |             |                |        100 |     List |  Longest | 555.0189     |
|    25 | Random Search |             |                |        100 |     List | Shortest | 483.8219     |
|    26 | Hill Climber  | Insert      |                |          1 |     List | Shortest | 30.68847     |
|    27 | Hill Climber  | Insert      |                |          1 |     List |  Longest | 762.99493    |
|    28 | Hill Climber  | Single Swap |                |          1 |     List | Shortest | 78.05326     |
|    29 | Hill Climber  | Single Swap |                |          1 |     List |  Longest | 760.7559     |

As can be seen from the table above, my most sucessful implementation was run #TODO named TODO. The plot of the paths can be seen below.

## Methods

### Random Search

In `RandomSearchTSPSolver.cs`, I've implemented a random search travelling saleman problem solver that uniformly samples the solution space, and keeps track of the best solution it has found so far. In other words, I'm creating a random path for every single individual in the population in every single generation, evaluting the fitness, then, if the individual has the highest fitness in the population, I allow the individual to survive to the next generation (the `elite`).

#### Shortest Path Learning Curve of Random Search

![alt text](output\run25-random-shortest\Cost.png "Shortest Path Learning Curve of Random Search")

#### Longest Path Learning Curve of Random Search

![alt text](output\run24-random-longest\Cost.png "Longest Path Learning Curve of Random Search")

### Representations and their corresponding Crossover and Mutation Operators

In my assignment I tried two different genotype representations, with their corresponding crossover and mutation operators, which I will describe below:

1.  `PriorityGenotype`: Every single city has a normalized float associated with it, and the path the TSP will take is a priority queue on the list of tuple of city and its corresponding priority.

    - `PriorityCrossover`: this crossover operator does a Two Point crossover on the two parent's priorities creating the child's priority. Since the priority lists are ordered by the city's id, there is no invalid priority list (duplicate or missing cities) to resolve, therefore, no invalid path to resolve.
    - `PrioritySingleMutator`: this mutator operator adds some random noise to the priority of one city, then clamps it between 0 and 1.
    - `PriorityMutator`: does the same thing the `PrioritySingleMutator` does but to a variable number of cities, depending on the mutation factor.

2.  `ListGenotype`: the genotype is just an ordered list of the cities, and the path is exactly the genotype.
    - `SingleSwapMutator`: This mutator operator performs a single swap between two random cities.
    - `MultiSwapMutator`: same as `SingleSwapMutator`, but performs a variable number of swaps based on the mutation factor.
    - `InsertMutator`: chooses a random sequence of cities, removes the sequence from the path, then inserts it somewhere else in the path.
    - `Crossover`: TODO

## Performance Plots

## Appendix
