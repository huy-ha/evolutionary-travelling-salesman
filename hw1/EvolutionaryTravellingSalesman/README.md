# Travelling Salesman Problem using Genetic Algorithms

Name: Huy Ha

UNI: hqh2101

Course Name: Evolutionary Computation and Automated Design

Course Number: MECS E4510

Instructor: professor Hod Lipson

Date Submitted: September 29th 2019

Grace Hours used: 0 hours

Grace Hours left: 96 hours

<div style="page-break-after: always;"></div>

---

# 1. Grading Helper:

## General

1.  [Cover page](<#Travelling Salesman Problem using Genetic Algorithms>)
2.  [Results Summary table](<#Summary Results Table>)
3.  [Dot plot for a population of Asexual Insert Hill Climbers](<#Dot Plot for a Population of Hill Climbers>)
4.  [Convergence plot for any one of some methods (not all)](<#Shortest Path (left) and Longest Path (right) Learning Curve of Hill Climbers>)
5.  `Code included (8pt courier single spacing)`
6.  `Theoretical shortest path using Christofides' algorithm`
7.  [Movie of optimizing path (one frame every time path improves)](<#Video Animation of Insert Hill Climber Solving for Shortest Path>)
8.  `learning curves clearly labeled, have error bars, labeled axes`
9.  `Shortest path Overall performance (based on distance, evaluations)`
10. `Longest path Overall performance (based on distance, evaluations)`

## Methods

1. [Description of Representation Used](#Representations)
2. [Description of Random Search](<#Random Search>)
3. [Description of Hill Climber](<#Hill Climber>)
4. `Description of EA variation and selection methods used`
5. `Analysis of Performance (What worked and what didn't)`
6. `Two methods compared (bar chart)`

## Performance Curves

1. [Shortest Path Learning Curve of random search](<#Shortest Path (left) and Longest Path (right) Learning Curve of Random Search>) `TODO: error bars`
2. `Shortest Path Learning Curve of hill climber`
3. `Shortest Path Learning Curve of EA and some variation of EA`
4. [Longest Path Learning Curve of random search](<#Shortest Path (left) and Longest Path (right)
5. `Longest Path Learning Curve of hill climber`
6. `Longest Path Learning Curve of EA and some variation of EA`

# 2. Results

## 2.1 Summary Results Table

Below is my results summary table, and explanation of what each column means (where it might not be clear)

- The second column (Reproduction) tells which reproducer I used, or the name of the configuration (Hill climber, random search,...).
- The third column (Mutation) outlines which of the mutator operator (if any) I used in the run
- The fourth column (Annealing) states whether or not I used simulated annealing, and if so, what was the initial temperature
- The fifth column (Population) gives the number of individuals in my population for the run
- The sixth column (Genotype) tells which genotype was used for the run
- The seventh column (Optimize) tells whether the run was optimized for shortest or longest path

| Run # | Reproduction  | Mutation    | Annealing      | Population | Genotype | Optimize | Path Length |
| ----: | :------------ | :---------- | :------------- | ---------: | -------: | -------: | :---------- |
|    18 | Asexual       | Single Swap | No             |        100 |     List | Shortest | 74.63085    |
|    19 | Asexual       | Single Swap | No             |        100 |     List |  Longest | 760.8126    |
|    20 | Asexual       | Single Swap | Yes (init 0.5) |        100 |     List | Shortest | 74.58129    |
|    21 | Asexual       | Insert      | Yes (init 0.5) |         50 |     List | Shortest | 47.86598    |
|    22 | Asexual       | Insert      | Yes (init 0.5) |        100 |     List | Shortest | 41.14663    |
|    23 | Asexual       | Insert      | Yes (init 0.5) |        200 |     List | Shortest | 36.29556    |
|    24 | Random Search |             |                |        100 |     List |  Longest | 555.0189    |
|    25 | Random Search |             |                |        100 |     List | Shortest | 483.8219    |
|    26 | Hill Climber  | Insert      |                |          1 |     List | Shortest | 30.68847    |
|    27 | Hill Climber  | Insert      |                |          1 |     List |  Longest | 762.99493   |
|    28 | Hill Climber  | Single Swap |                |          1 |     List | Shortest | 78.05326    |
|    29 | Hill Climber  | Single Swap |                |          1 |     List |  Longest | 760.7559    |

As can be seen from the table above, my most sucessful implementation was run #TODO named TODO. The plot of the paths can be seen below.

## 2.2 Brief Analysis of Results Summary Table

# 3. Methods

## 3.1 Random Search

In `RandomSearchTSPSolver.cs`, I've implemented a random search travelling saleman problem solver that uniformly samples the solution space, and keeps track of the best solution it has found so far. In other words, I'm creating a random path for every single individual in the population in every single generation, evaluting the fitness, then, if the individual has the highest fitness in the population, I allow the individual to survive to the next generation (the `elite`).

#### Shortest Path (left) and Longest Path (right) Learning Curve of Random Search

<div style="clear:both;">
<img src="output\run25-random-shortest\Cost.png" alt="Shortest Path Learning Curve of Random Search"
	title="Shortest Path Learning Curve of Random Search" width="45%" height="auto" />
<img src="output\run24-random-longest\Cost.png" alt="Longest Path Learning Curve of Random Search"
	title="Longest Path Learning Curve of Random Search" width="45%" height="auto" />
</div>

## 3.2 Hill Climber

A Hill climber is just a genetic algorithm with population 1, simulated annealing with initial temperature set to 0, with a 100% reproduction rate (the individual in the previous "generation" is always the starting point for creating the new individual in the next population), with an asexual reproduction operator (no crossing over). Since my implementation of the GA was generic enough, I was able to just define a configuration of the GA as a hill climber, with the configurations as described above. The only degree of freedom left was the mutation operator, which I experimented with the insert mutator and swap mutator (described more detailedly below in my EA section). The learning curves for hill climbers with both types of mutator operators can be seen below.

#### Shortest Path (left) and Longest Path (right) Learning Curve of Hill Climbers

<div style="clear:both;">
<img src="output\assets\longest-hc.png" alt="Shortest Path Learning Curve of Hill Climbers"
	title="Shortest Path Learning Curve of Hill Climbers" width="45%" height="auto" />
<img src="output\assets\shortest-hc.png" alt="Shortest Path Learning Curve of Hill Climbers"
	title="Longest Path Learning Curve of Hill Climbers" width="45%" height="auto" />
</div>

#### Video Animation of Insert Hill Climber Solving for Shortest Path

<a href="https://drive.google.com/file/d/1o2MVIlHO2COeTcewPxxkhMqrY-ufpkky/view?usp=sharing">
<img src="output\assets\animation-preview.png" alt="Preview of Insert Hill Climber animation"
	title="Preview of Insert Hill Climber animation"/>
</a>

[Here is a video of the insert hill climber solving for the shortest path. (You can also click on the preview of the animation above)](https://drive.google.com/file/d/1o2MVIlHO2COeTcewPxxkhMqrY-ufpkky/view?usp=sharing)

#### Dot Plot for a Population of Hill Climbers

Because Hill Climbers were so successful, I wanted to explore what a population of hill climbers would do. This is exactly as described above, only, this time, there are 40 hill climbers, and each generation, they all mutate a bit to give the next generation's offspring. Below is a dot plot of that (on the left). As you can see, it just looks like a thick irregular line, so I've zoomed into a part of the plot so you can see the actual scatter points (on the right)

<div style="clear:both;">
<img src="output\assets\insert-hc-dotplot.png" alt="Dot Plot for a Population of Hill Climbers"
	title="Dot Plot for a Population of Hill Climbers"  width="45%" height="auto" />
<img src="output\assets\insert-hc-dotplot-zoomed.png" alt="Zoomed In Dot Plot for a Population of Hill Climbers"
	title="Zoomed In Dot Plot for a Population of Hill Climbers"  width="45%" height="auto" />
</div>

## 3.3 Evolutionary Algorithm

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

# 4. Appendix

As per the TA's approval on Piazza, the source code for this project has been zipped up with this README document.
