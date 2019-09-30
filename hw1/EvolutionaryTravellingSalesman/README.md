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
5.  [Theoretical shortest path using Christofides' algorithm](<# Christofides>) `Plot path comparison between insert hill climber and christofides`
6.  [Movie of optimizing path (one frame every time path improves)](<#Video Animation of Insert Hill Climber Solving for Shortest Path>)
7.  `Shortest path Overall performance (based on distance, evaluations)`
8.  `Longest path Overall performance (based on distance, evaluations)`

## Methods

1. [Description of Representation Used](#Representations)
2. [Description of Random Search](<#Random Search>)
3. [Description of Hill Climber](<#Hill Climber>)
4. Description of [EA variation](<#Representations and their corresponding Crossover and Mutation Operators>) and [selection methods](#Selector) used
5. [Analysis of Performance](<# 4. Analysis of Performance>)

## Performance Curves

1. [Learning Curve of Random Search(Shortest and Longest)](<#Shortest Path (left) and Longest Path (right) Learning Curve of Random Search>)
2. [Learning Curve of Hill Climber (Shortest and Longest)](<# Shortest Path (left) and Longest Path (right) Learning Curve of Hill Climbers>)
3. `Learning Curve of EA`
4. `Learning Curves of all algorithms tried`

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
<img src="output\assets\shortest-rs.png" alt="Shortest Path Learning Curve of Random Search"
	title="Shortest Path Learning Curve of Random Search" width="45%" height="auto" />
<img src="output\assets\longest-rs.png" alt="Longest Path Learning Curve of Random Search"
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

### 3.3.1 Representations and their corresponding Crossover and Mutation Operators

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

### 3.3.2 Selector

Earlier on in the development process, I experimented with Stochastic Universal Sampling, as described in the recommended textbook for the class "An Introduction to Genetic Algorithms", but it did not work as well, and I didn't see it as the limiting factor of my program. Therefore, I removed that part of the code, and just replaced it with a simple truncate selector, which selects the top X%, where X is a hyperparameter I called ReproductionFactor.

# 4. Analysis of Performance

## Swap Mutator v.s. Insert Mutator

The biggest breakthough I had in terms of performance was when I implemented the `InsertMutator`. This mutator is much more suited for the problem than the SwapMutator was. I think the intuition is that grabbing a sequence of cities then chucking it somewhere else doesn't break the solution that previous generations has already came up with (This was the inspiration for my `SelectionCrossOver`).

Another curious thing to note is that Insert did much better than Swap when it came to shortest path, but only did marginally better than Swap in longest path. I think this is because Swap breaks paths that are close together, so it is more suited for longest path than shortest path. However, the advantage of Insert is still that it preserves groups (sub-solutions) that are already optimum, and is able to explore without giving up those optimum sub-solutions.

## Asexual v.s. Multiple Inheritance

I think the reason why Asexual consistently outperforms Multiple Inheritance is not because in general it does, but because Mutliple Inheritance relies on have appropriate cross over operators, which I don't think I spent enough time exploring. If given more time, I would like to try a number of other cross over operators for ordered lists, and hopefully that might prove Multiple Inheritance to be superior.

Another note is that my development cycle is biased. Because Asexual reproduction cycles are faster to run and get results, I'm able to go through many more development cycles with Asexual, therefore have more time exploring all the different mutators for Asexual and optimizing it.

## Simulated Annealing vs Greedy

Something I did not expect was for Simulated Annealing to not consistently outperform the greedy variations (just initializing the temperature to 0). I ran lots of tests on the circle test file I created, and to my surprise, most of the time, Greedy converges to a higher fitness. I could not come with an explantion to this. This does not make sense to me because I run the simulation for long enough to observe real convergence, and so any exploration should benefit the program.

On the same note, I expected a larger population to maintain more diversity, therefore, converge to a higher fitness, but this was not the case. I ran tests on the circle city test file I created, and the fitness would consistently converge to a higher value for a population of 50 than for 100, and higher for 100 than for 200, and so on.

These two points, I'm not able to explain why this is the case.

## Christofides

According to a Piazza post answer by the professor, he said we could use a package to calculate the distance using Christofides. I cloned guillaumeportails' fork of sth144's implementation of Christofides, and made some changes so that the final distance calculation is accurate for floating point numbers. The output path length from this implementation of Christofides was `32.1098`. To my surprise, my Insert Hill Climber outperformed this Christofides implementation! Of course, since I did not implement this program myself and did not spend time looking through the code, I don't know of its correctness, but the result is encouraging none the less. [See 5.2 for the output I got when running Christofides.](<# 5.2 Christofides Algorithm output>)

# 5. Appendix

## 5.1 Source Code

As per the TA's approval on Piazza, the source code for this project has been zipped up with this README document.

## 5.2 Christofides Algorithm output

```
cities created
tsp created
Fillmatrix started
Filled Matrix
MST created
Matching completed
BestPath completed 0
Final length: 0

0 to 660 cost 0.21371
660 to 495 cost 0.0239094
495 to 518 cost 0.0125831
518 to 814 cost 0.0246069
814 to 193 cost 0.0245268
193 to 136 cost 0.00757985
136 to 16 cost 0.00917958
16 to 769 cost 0.0152843
769 to 437 cost 0.017203
437 to 179 cost 0.0142129
179 to 448 cost 0.0347946
448 to 823 cost 0.0531549
823 to 326 cost 0.0640528
326 to 298 cost 0.0326298
298 to 816 cost 0.0262839
816 to 612 cost 0.0647916
612 to 52 cost 0.014918
52 to 726 cost 0.0108301
726 to 649 cost 0.0132654
649 to 615 cost 0.0164624
615 to 756 cost 0.101699
756 to 701 cost 0.0228115
701 to 616 cost 0.035752
616 to 905 cost 0.0367072
905 to 190 cost 0.0641047
190 to 520 cost 0.0285338
520 to 698 cost 0.00978459
698 to 124 cost 0.0278024
124 to 247 cost 0.00587152
247 to 571 cost 0.011884
571 to 373 cost 0.0238598
373 to 207 cost 0.0279071
207 to 74 cost 0.0125553
74 to 997 cost 0.0522104
997 to 773 cost 0.0903594
773 to 364 cost 0.00736196
364 to 553 cost 0.0168162
553 to 78 cost 0.0489602
78 to 483 cost 0.00540035
483 to 330 cost 0.0466122
330 to 441 cost 0.0115499
441 to 886 cost 0.0325366
886 to 306 cost 0.0269063
306 to 588 cost 0.00709601
588 to 83 cost 0.0174942
83 to 73 cost 0.01194
73 to 39 cost 0.00832279
39 to 380 cost 0.0260679
380 to 981 cost 0.0344646
981 to 49 cost 0.0680409
49 to 285 cost 0.513602
285 to 400 cost 0.0173182
400 to 674 cost 0.0119171
674 to 639 cost 0.0285908
639 to 753 cost 0.00975537
753 to 925 cost 0.0152828
925 to 604 cost 0.0341047
604 to 951 cost 0.0323075
951 to 161 cost 0.0769202
161 to 273 cost 0.0278354
273 to 551 cost 0.0334932
551 to 64 cost 0.0265449
64 to 945 cost 0.0204352
945 to 611 cost 0.0231089
611 to 10 cost 0.0352039
10 to 684 cost 0.000786294
684 to 90 cost 0.0167437
90 to 137 cost 0.0103956
137 to 386 cost 0.00713698
386 to 428 cost 0.026557
428 to 914 cost 0.0296031
914 to 255 cost 0.019876
255 to 183 cost 0.0276791
183 to 465 cost 0.0160369
465 to 289 cost 0.00787982
289 to 591 cost 0.0310632
591 to 259 cost 0.0296089
259 to 549 cost 0.0478123
549 to 817 cost 0.0519112
817 to 685 cost 0.075211
685 to 152 cost 0.025205
152 to 164 cost 0.00653919
164 to 561 cost 0.00644229
561 to 556 cost 0.0247667
556 to 596 cost 0.353409
596 to 474 cost 0.0296644
474 to 498 cost 0.031897
498 to 195 cost 0.00701924
195 to 197 cost 0.0411198
197 to 923 cost 0.013739
923 to 451 cost 0.0396314
451 to 543 cost 0.0278667
543 to 434 cost 0.0428151
434 to 2 cost 0.0309621
2 to 442 cost 0.0362003
442 to 638 cost 0.0430322
638 to 17 cost 0.0375801
17 to 962 cost 0.0196525
962 to 367 cost 0.019261
367 to 204 cost 0.0206711
204 to 640 cost 0.0216326
640 to 253 cost 0.0212719
253 to 145 cost 0.0346648
145 to 942 cost 0.00659342
942 to 48 cost 0.0121097
48 to 432 cost 0.0120689
432 to 567 cost 0.00809572
567 to 322 cost 0.0115338
322 to 300 cost 0.557766
300 to 61 cost 0.0135764
61 to 186 cost 0.0158048
186 to 586 cost 0.0419501
586 to 231 cost 0.210611
231 to 163 cost 0.0350798
163 to 194 cost 0.0228272
194 to 627 cost 0.00257221
627 to 53 cost 0.0225139
53 to 114 cost 0.011286
114 to 416 cost 0.023878
416 to 439 cost 0.0271092
439 to 9 cost 0.0246152
9 to 644 cost 0.0189427
644 to 978 cost 0.0879323
978 to 775 cost 0.0169516
775 to 652 cost 0.0372238
652 to 824 cost 0.0275857
824 to 447 cost 0.0329583
447 to 855 cost 0.0167509
855 to 944 cost 0.232634
944 to 184 cost 0.0202103
184 to 403 cost 0.0205334
403 to 717 cost 0.0380935
717 to 359 cost 0.0471369
359 to 490 cost 0.0299567
490 to 43 cost 0.0171773
43 to 69 cost 0.0397822
69 to 332 cost 0.0231346
332 to 77 cost 0.0176787
77 to 159 cost 0.0190842
159 to 142 cost 0.0109789
142 to 702 cost 0.0307785
702 to 196 cost 0.0230001
196 to 187 cost 0.0228819
187 to 856 cost 0.0171241
856 to 85 cost 0.0177137
85 to 919 cost 0.0515325
919 to 244 cost 0.0411331
244 to 203 cost 0.00692392
203 to 165 cost 0.0201842
165 to 455 cost 0.0604717
455 to 779 cost 0.036321
779 to 654 cost 0.0557184
654 to 118 cost 0.0260606
118 to 457 cost 0.0366244
457 to 754 cost 0.0161038
754 to 849 cost 0.0241733
849 to 864 cost 0.0133796
864 to 585 cost 0.00842032
585 to 8 cost 0.0153709
8 to 692 cost 0.00630739
692 to 417 cost 0.0274757
417 to 502 cost 0.0288585
502 to 365 cost 0.0278987
365 to 460 cost 0.0410068
460 to 778 cost 0.00277166
778 to 715 cost 0.0214691
715 to 343 cost 0.0433409
343 to 237 cost 0.030016
237 to 390 cost 0.0299686
390 to 818 cost 0.0221086
818 to 62 cost 0.0234862
62 to 722 cost 0.00650043
722 to 286 cost 0.0112334
286 to 646 cost 0.014123
646 to 822 cost 0.0234438
822 to 360 cost 0.0270937
360 to 484 cost 0.022522
484 to 50 cost 0.0216079
50 to 379 cost 0.0340154
379 to 935 cost 0.0684651
935 to 222 cost 0.00566715
222 to 497 cost 0.0433599
497 to 503 cost 0.0161364
503 to 830 cost 0.0350884
830 to 796 cost 0.0448725
796 to 393 cost 0.0435907
393 to 376 cost 0.0219171
376 to 777 cost 0.00968035
777 to 389 cost 0.0186484
389 to 578 cost 0.0199597
578 to 397 cost 0.0456764
397 to 287 cost 0.0290679
287 to 450 cost 0.0335566
450 to 106 cost 0.0167515
106 to 214 cost 0.0722411
214 to 954 cost 0.0274605
954 to 525 cost 0.00802991
525 to 215 cost 0.0307272
215 to 563 cost 0.0210001
563 to 941 cost 0.0290857
941 to 928 cost 0.0229126
928 to 629 cost 0.0110335
629 to 665 cost 0.0368423
665 to 597 cost 0.0363316
597 to 262 cost 0.0367823
262 to 280 cost 0.0261195
280 to 245 cost 0.0289013
245 to 786 cost 0.0328444
786 to 307 cost 0.0071122
307 to 858 cost 0.0408222
858 to 987 cost 0.112384
987 to 733 cost 0.177453
733 to 471 cost 0.00739858
471 to 562 cost 0.0132776
562 to 423 cost 0.0117618
423 to 993 cost 0.0154433
993 to 463 cost 0.0207163
463 to 839 cost 0.0249953
839 to 276 cost 0.0189318
276 to 672 cost 0.00822401
672 to 916 cost 0.0218253
916 to 34 cost 0.0267534
34 to 659 cost 0.0270321
659 to 950 cost 0.0386992
950 to 335 cost 0.024239
335 to 776 cost 0.00502648
776 to 771 cost 0.0338993
771 to 906 cost 0.00918362
906 to 688 cost 0.0196191
688 to 67 cost 0.0518195
67 to 88 cost 0.0233252
88 to 427 cost 0.0137866
427 to 94 cost 0.0372668
94 to 810 cost 0.0311538
810 to 811 cost 0.0124724
811 to 648 cost 0.0225276
648 to 95 cost 0.0554314
95 to 375 cost 0.0109445
375 to 96 cost 0.0345389
96 to 162 cost 0.0363441
162 to 540 cost 0.00177524
540 to 101 cost 0.0220399
101 to 217 cost 0.0261055
217 to 876 cost 0.0520698
876 to 173 cost 0.0171313
173 to 727 cost 0.0135626
727 to 931 cost 0.021505
931 to 28 cost 0.114501
28 to 903 cost 0.0413096
903 to 565 cost 0.0510338
565 to 116 cost 0.55994
116 to 216 cost 0.0268891
216 to 827 cost 0.00752227
827 to 890 cost 0.0292299
890 to 82 cost 0.0174391
82 to 256 cost 0.0244663
256 to 257 cost 0.0225649
257 to 79 cost 0.0335649
79 to 233 cost 0.11186
233 to 230 cost 0.0345757
230 to 347 cost 0.0109346
347 to 676 cost 0.070808
676 to 315 cost 0.0247268
315 to 798 cost 0.0319537
798 to 51 cost 0.00338188
51 to 559 cost 0.260581
559 to 299 cost 0.0248335
299 to 632 cost 0.0179091
632 to 120 cost 0.027531
120 to 699 cost 0.0127862
699 to 372 cost 0.0378534
372 to 780 cost 0.0125137
780 to 705 cost 0.0630225
705 to 132 cost 0.038005
132 to 843 cost 0.0303463
843 to 506 cost 0.00968207
506 to 527 cost 0.014156
527 to 550 cost 0.0140307
550 to 762 cost 0.0222203
762 to 418 cost 0.0021359
418 to 331 cost 0.0107696
331 to 894 cost 0.0325153
894 to 932 cost 0.00386552
932 to 430 cost 0.0326781
430 to 45 cost 0.00830631
45 to 3 cost 0.0129619
3 to 979 cost 0.0395434
979 to 801 cost 0.0177685
801 to 402 cost 0.0202039
402 to 735 cost 0.0268203
735 to 785 cost 0.0255903
785 to 297 cost 0.0179039
297 to 235 cost 0.0138674
235 to 835 cost 0.0422606
835 to 862 cost 0.0038911
862 to 41 cost 0.0148335
41 to 346 cost 0.0142246
346 to 265 cost 0.0121765
265 to 20 cost 0.0342308
20 to 802 cost 0.0105677
802 to 998 cost 0.0279673
998 to 410 cost 0.0199212
410 to 790 cost 0.0202301
790 to 904 cost 0.0393467
904 to 541 cost 0.04181
541 to 218 cost 0.0257833
218 to 677 cost 0.0319907
677 to 301 cost 0.0153968
301 to 647 cost 0.0315325
647 to 675 cost 0.0252785
675 to 392 cost 0.021831
392 to 531 cost 0.0549796
531 to 523 cost 0.0322495
523 to 219 cost 0.0713455
219 to 249 cost 0.0211102
249 to 158 cost 0.0188703
158 to 205 cost 0.0501121
205 to 592 cost 0.00433242
592 to 154 cost 0.0264257
154 to 968 cost 0.017723
968 to 126 cost 0.0133257
126 to 789 cost 0.0140827
789 to 363 cost 0.0122688
363 to 504 cost 0.0489576
504 to 552 cost 0.00672424
552 to 982 cost 0.012353
982 to 22 cost 0.0131869
22 to 587 cost 0.0100796
587 to 736 cost 0.0167209
736 to 135 cost 0.039092
135 to 994 cost 0.00616803
994 to 458 cost 0.00485189
458 to 546 cost 0.0214031
546 to 84 cost 0.047419
84 to 469 cost 0.00957903
469 to 749 cost 0.0308479
749 to 974 cost 0.0125372
974 to 576 cost 0.0160625
576 to 168 cost 0.00843651
168 to 800 cost 0.0255103
800 to 344 cost 0.0249909
344 to 920 cost 0.0454712
920 to 348 cost 0.00507997
348 to 499 cost 0.0166192
499 to 988 cost 0.028938
988 to 650 cost 0.0254197
650 to 323 cost 0.020478
323 to 958 cost 0.00983839
958 to 155 cost 0.0136216
155 to 826 cost 0.0221653
826 to 719 cost 0.0232974
719 to 396 cost 0.0151257
396 to 877 cost 0.0764066
877 to 759 cost 0.0503459
759 to 240 cost 0.0246141
240 to 412 cost 0.0333246
412 to 516 cost 0.0246199
516 to 4 cost 0.00742079
4 to 200 cost 0.00566777
200 to 282 cost 0.0101115
282 to 270 cost 0.0197963
270 to 626 cost 0.0152707
626 to 324 cost 0.0600003
324 to 738 cost 0.0260821
738 to 24 cost 0.00903727
24 to 794 cost 0.0298188
794 to 263 cost 0.0286705
263 to 791 cost 0.0320985
791 to 14 cost 0.058556
14 to 874 cost 0.0272604
874 to 472 cost 0.0115632
472 to 618 cost 0.000806228
618 to 44 cost 0.036755
44 to 234 cost 0.00513707
234 to 707 cost 0.0246335
707 to 605 cost 0.0235202
605 to 314 cost 0.0149046
314 to 398 cost 0.0253611
398 to 362 cost 0.0162494
362 to 283 cost 0.0132693
283 to 513 cost 0.0371563
513 to 295 cost 0.0210521
295 to 572 cost 0.0130951
572 to 977 cost 0.0972953
977 to 887 cost 0.0328842
887 to 793 cost 0.0361289
793 to 568 cost 0.00727258
568 to 883 cost 0.0130391
883 to 353 cost 0.018856
353 to 697 cost 0.0216807
697 to 493 cost 0.0322927
493 to 97 cost 0.01547
97 to 574 cost 0.026141
574 to 695 cost 0.00948336
695 to 664 cost 0.0194587
664 to 918 cost 0.0349954
918 to 949 cost 0.0155227
949 to 251 cost 0.00383065
251 to 292 cost 0.0189406
292 to 937 cost 0.0340524
937 to 281 cost 0.0149782
281 to 131 cost 0.00848114
131 to 619 cost 0.0208214
619 to 5 cost 0.0399258
5 to 996 cost 0.0515035
996 to 454 cost 0.0107445
454 to 590 cost 0.0225013
590 to 975 cost 0.0337937
975 to 795 cost 0.0279383
795 to 897 cost 0.0323543
897 to 25 cost 0.0138902
25 to 92 cost 0.0290498
92 to 716 cost 0.0416052
716 to 308 cost 0.00626296
308 to 160 cost 0.0114763
160 to 303 cost 0.0445086
303 to 431 cost 0.0223624
431 to 420 cost 0.0287974
420 to 444 cost 0.0148733
444 to 970 cost 0.0369228
970 to 440 cost 0.0891894
440 to 350 cost 0.0193007
350 to 984 cost 0.0106198
984 to 879 cost 0.0335855
879 to 964 cost 0.0362208
964 to 133 cost 0.00534938
133 to 828 cost 0.00370058
828 to 18 cost 0.0102164
18 to 312 cost 0.0271904
312 to 690 cost 0.0201402
690 to 630 cost 0.0364353
630 to 415 cost 0.0279222
415 to 115 cost 0.0132656
115 to 462 cost 0.0237068
462 to 815 cost 0.0240149
815 to 927 cost 0.166596
927 to 842 cost 0.124204
842 to 940 cost 0.00911947
940 to 961 cost 0.0138937
961 to 33 cost 0.0173728
33 to 602 cost 0.0478686
602 to 57 cost 0.0227767
57 to 60 cost 0.00264937
60 to 530 cost 0.00405363
530 to 339 cost 0.0176034
339 to 995 cost 0.0244479
995 to 342 cost 0.0209204
342 to 357 cost 0.0247349
357 to 470 cost 0.00910501
470 to 634 cost 0.0588316
634 to 863 cost 0.022657
863 to 662 cost 0.0239716
662 to 885 cost 0.0442444
885 to 901 cost 0.0049016
901 to 150 cost 0.0215023
150 to 91 cost 0.0317107
91 to 623 cost 0.0180056
623 to 87 cost 0.0209731
87 to 70 cost 0.0170623
70 to 813 cost 0.0316603
813 to 734 cost 0.0890134
734 to 622 cost 0.082951
622 to 792 cost 0.0442259
792 to 989 cost 0.133173
989 to 686 cost 0.0206997
686 to 65 cost 0.0213737
65 to 872 cost 0.0109554
872 to 840 cost 0.0156052
840 to 708 cost 0.0172991
708 to 391 cost 0.0825286
391 to 831 cost 0.0160039
831 to 933 cost 0.00837376
933 to 210 cost 0.019161
210 to 191 cost 0.0125127
191 to 609 cost 0.0428226
609 to 405 cost 0.0234266
405 to 750 cost 0.0257312
750 to 302 cost 0.0568047
302 to 211 cost 0.0278366
211 to 528 cost 0.0195879
528 to 767 cost 0.0570255
767 to 729 cost 0.0303176
729 to 180 cost 0.014433
180 to 668 cost 0.0204613
668 to 32 cost 0.017131
32 to 600 cost 0.0310823
600 to 764 cost 0.0208645
764 to 104 cost 0.0187216
104 to 661 cost 0.0383211
661 to 755 cost 0.0142518
755 to 696 cost 0.0509508
696 to 464 cost 0.0236911
464 to 496 cost 0.0180801
496 to 404 cost 0.00759987
404 to 198 cost 0.0122181
198 to 72 cost 0.0708664
72 to 491 cost 0.0227361
491 to 117 cost 0.0311724
117 to 610 cost 0.0292729
610 to 212 cost 0.0274906
212 to 880 cost 0.0321933
880 to 514 cost 0.00835104
514 to 341 cost 0.0198714
341 to 784 cost 0.0280469
784 to 358 cost 0.03736
358 to 38 cost 0.00331428
38 to 799 cost 0.0164464
799 to 909 cost 0.0387566
909 to 819 cost 0.0497572
819 to 399 cost 0.0156606
399 to 220 cost 0.0282958
220 to 7 cost 0.0110645
7 to 765 cost 0.0259522
765 to 537 cost 0.0251904
537 to 411 cost 0.0175543
411 to 712 cost 0.0578152
712 to 821 cost 0.0247522
821 to 881 cost 0.0203029
881 to 112 cost 0.297752
112 to 35 cost 0.0208017
35 to 983 cost 0.0250715
983 to 29 cost 0.454065
29 to 475 cost 0.0203418
475 to 508 cost 0.0130532
508 to 290 cost 0.0591926
290 to 658 cost 0.0425924
658 to 607 cost 0.0294151
607 to 93 cost 0.0362549
93 to 806 cost 0.0173777
806 to 479 cost 0.00707998
479 to 313 cost 0.00711486
313 to 534 cost 0.0115289
534 to 922 cost 0.00840728
922 to 965 cost 0.326313
965 to 153 cost 0.011642
153 to 81 cost 0.018121
81 to 952 cost 0.0156786
952 to 409 cost 0.0288988
409 to 836 cost 0.0758529
836 to 99 cost 0.0249388
99 to 13 cost 0.0155347
13 to 89 cost 0.0450144
89 to 621 cost 0.035477
621 to 261 cost 0.0234976
261 to 748 cost 0.0299917
748 to 694 cost 0.00834027
694 to 226 cost 0.0340355
226 to 809 cost 0.0154101
809 to 966 cost 0.0116924
966 to 846 cost 0.0273274
846 to 741 cost 0.0307736
741 to 888 cost 0.022765
888 to 593 cost 0.035976
593 to 509 cost 0.0524702
509 to 724 cost 0.0191621
724 to 570 cost 0.00986669
570 to 670 cost 0.0174208
670 to 384 cost 0.0358095
384 to 599 cost 0.0100168
599 to 737 cost 0.0188108
737 to 912 cost 0.0396931
912 to 103 cost 0.0862982
103 to 238 cost 0.0469579
238 to 991 cost 0.0478242
991 to 743 cost 0.029883
743 to 723 cost 0.0105573
723 to 720 cost 0.00553704
720 to 867 cost 0.102032
867 to 320 cost 0.0273526
320 to 63 cost 0.0246242
63 to 268 cost 0.00667356
268 to 102 cost 0.0249148
102 to 494 cost 0.00676912
494 to 606 cost 0.0324263
606 to 679 cost 0.247142
679 to 512 cost 0.0375296
512 to 926 cost 0.0304454
926 to 645 cost 0.0122827
645 to 524 cost 0.0440345
524 to 482 cost 0.0409352
482 to 171 cost 0.0121663
171 to 227 cost 0.0301042
227 to 383 cost 0.0122067
383 to 938 cost 0.0276649
938 to 757 cost 0.0318804
757 to 141 cost 0.0141569
141 to 929 cost 0.0212948
929 to 201 cost 0.0090395
201 to 889 cost 0.00674689
889 to 406 cost 0.0254096
406 to 236 cost 0.0353869
236 to 895 cost 0.0077283
895 to 535 cost 0.0274893
535 to 554 cost 0.0251842
554 to 473 cost 0.0228304
473 to 850 cost 0.0205189
850 to 501 cost 0.0026808
501 to 584 cost 0.0223285
584 to 745 cost 0.00704317
745 to 349 cost 0.0141385
349 to 246 cost 0.0244461
246 to 157 cost 0.0240568
157 to 548 cost 0.0274416
548 to 374 cost 0.0255123
374 to 167 cost 0.0258751
167 to 224 cost 0.0239998
224 to 310 cost 0.0297856
310 to 709 cost 0.0354538
709 to 681 cost 0.0233217
681 to 56 cost 0.0358105
56 to 208 cost 0.0327813
208 to 731 cost 0.0230665
731 to 569 cost 0.019707
569 to 580 cost 0.0197423
580 to 466 cost 0.0402435
466 to 113 cost 0.0140812
113 to 59 cost 0.0223279
59 to 969 cost 0.0144015
969 to 105 cost 0.04334
105 to 555 cost 0.0301188
555 to 407 cost 0.013215
407 to 354 cost 0.0407352
354 to 882 cost 0.0300998
882 to 908 cost 0.00550301
908 to 837 cost 0.00910352
837 to 321 cost 0.00676166
321 to 667 cost 0.0212019
667 to 123 cost 0.0161362
123 to 68 cost 0.0330933
68 to 946 cost 0.025625
946 to 721 cost 0.0249382
721 to 973 cost 0.018071
973 to 539 cost 0.0180335
539 to 999 cost 0.262526
999 to 633 cost 0.0271181
633 to 583 cost 0.0335473
583 to 248 cost 0.0140644
248 to 119 cost 0.0226988
119 to 271 cost 0.0124097
271 to 728 cost 0.0438691
728 to 21 cost 0.0288799
21 to 267 cost 0.0161869
267 to 476 cost 0.0649005
476 to 866 cost 0.0214337
866 to 956 cost 0.0227831
956 to 808 cost 0.0285026
808 to 269 cost 0.0425478
269 to 631 cost 0.0283951
631 to 433 cost 0.00789557
433 to 377 cost 0.0250824
377 to 449 cost 0.00976355
449 to 258 cost 0.0105185
258 to 382 cost 0.0184261
382 to 853 cost 0.0179149
853 to 492 cost 0.011165
492 to 744 cost 0.00530129
744 to 66 cost 0.0194288
66 to 689 cost 0.043341
689 to 635 cost 0.0319491
635 to 653 cost 0.0169854
653 to 134 cost 0.0110612
134 to 209 cost 0.0449396
209 to 71 cost 0.0102164
71 to 31 cost 0.0114306
31 to 913 cost 0.0299499
913 to 939 cost 0.0352622
939 to 36 cost 0.01615
36 to 772 cost 0.0220594
772 to 489 cost 0.0156996
489 to 760 cost 0.0114343
760 to 177 cost 0.022869
177 to 907 cost 0.030737
907 to 608 cost 0.0380906
608 to 272 cost 0.0790665
272 to 352 cost 0.00507636
352 to 595 cost 0.0194836
595 to 781 cost 0.0195555
781 to 624 cost 0.0109914
624 to 532 cost 0.0188379
532 to 58 cost 0.021924
58 to 812 cost 0.00987103
812 to 371 cost 0.0339087
371 to 921 cost 0.00887092
921 to 917 cost 0.0265265
917 to 221 cost 0.0277171
221 to 869 cost 0.0361567
869 to 125 cost 0.0276388
125 to 188 cost 0.00310311
188 to 250 cost 0.0234098
250 to 486 cost 0.0415812
486 to 589 cost 0.0335497
589 to 26 cost 0.0207466
26 to 254 cost 0.0293895
254 to 788 cost 0.0151334
788 to 628 cost 0.118804
628 to 871 cost 0.0065904
871 to 23 cost 0.0237614
23 to 845 cost 0.027434
845 to 832 cost 0.0129817
832 to 149 cost 0.00843935
149 to 538 cost 0.0446052
538 to 915 cost 0.0303777
915 to 438 cost 0.0235056
438 to 277 cost 0.0221065
277 to 847 cost 0.0196556
847 to 804 cost 0.0381933
804 to 891 cost 0.0227649
891 to 140 cost 0.0185711
140 to 446 cost 0.02273
446 to 911 cost 0.0298071
911 to 642 cost 0.011197
642 to 581 cost 0.00540266
581 to 369 cost 0.00275835
369 to 169 cost 0.0399843
169 to 683 cost 0.0383477
683 to 763 cost 0.0245785
763 to 388 cost 0.0264683
388 to 30 cost 0.0098758
30 to 833 cost 0.047061
833 to 703 cost 0.0112732
703 to 725 cost 0.0329377
725 to 413 cost 0.0732948
413 to 488 cost 0.00486825
488 to 947 cost 0.0190136
947 to 98 cost 0.0366709
98 to 266 cost 0.0163124
266 to 582 cost 0.0116137
582 to 19 cost 0.0184995
19 to 478 cost 0.0103831
478 to 844 cost 0.00280159
844 to 899 cost 0.0133141
899 to 213 cost 0.190395
213 to 752 cost 0.0126258
752 to 657 cost 0.0320264
657 to 536 cost 0.136615
536 to 319 cost 0.0107889
319 to 829 cost 0.0173358
829 to 868 cost 0.00973371
868 to 875 cost 0.0207367
875 to 127 cost 0.0419774
127 to 181 cost 0.0629125
181 to 691 cost 0.012311
691 to 80 cost 0.00651637
80 to 521 cost 0.00518818
521 to 76 cost 0.0208972
76 to 487 cost 0.0482555
487 to 787 cost 0.024623
787 to 680 cost 0.00164448
680 to 655 cost 0.0194601
655 to 740 cost 0.0116091
740 to 666 cost 0.0307472
666 to 278 cost 0.00776527
278 to 232 cost 0.0180413
232 to 865 cost 0.0212339
865 to 199 cost 0.0563238
199 to 327 cost 0.0243332
327 to 614 cost 0.0256557
614 to 924 cost 0.0251279
924 to 128 cost 0.00969137
128 to 436 cost 0.0374161
436 to 425 cost 0.0409583
425 to 166 cost 0.0180854
166 to 54 cost 0.0301228
54 to 148 cost 0.02764
148 to 55 cost 0.0243352
55 to 841 cost 0.0109429
841 to 768 cost 0.0587353
768 to 986 cost 0.105591
986 to 960 cost 0.0822832
960 to 656 cost 0.0389372
656 to 1 cost 0.0238059
1 to 884 cost 0.0308391
884 to 345 cost 0.0207019
345 to 426 cost 0.0392549
426 to 663 cost 0.0489412
663 to 636 cost 0.0131127
636 to 742 cost 0.0108032
742 to 387 cost 0.0202996
387 to 304 cost 0.0282173
304 to 422 cost 0.00366375
422 to 366 cost 0.0195726
366 to 980 cost 0.0214386
980 to 990 cost 0.0417253
990 to 545 cost 0.0158831
545 to 467 cost 0.0615437
467 to 625 cost 0.0266308
625 to 732 cost 0.114635
732 to 641 cost 0.223594
641 to 452 cost 0.0108895
452 to 860 cost 0.0180674
860 to 873 cost 0.0371166
873 to 340 cost 0.0123664
340 to 243 cost 0.04361
243 to 336 cost 0.0175385
336 to 453 cost 0.0273058
453 to 930 cost 0.0213519
930 to 309 cost 0.0258623
309 to 485 cost 0.00787432
485 to 47 cost 0.0121196
47 to 15 cost 0.0191677
15 to 46 cost 0.0247487
46 to 693 cost 0.0496449
693 to 176 cost 0.00794786
176 to 547 cost 0.0439799
547 to 223 cost 0.0211664
223 to 704 cost 0.0593702
704 to 613 cost 0.0271737
613 to 11 cost 0.0129931
11 to 356 cost 0.01467
356 to 445 cost 0.0599104
445 to 40 cost 0.00109816
40 to 481 cost 0.0245167
481 to 529 cost 0.0269456
529 to 333 cost 0.0620344
333 to 900 cost 0.0555025
900 to 239 cost 0.0645956
239 to 291 cost 0.0304914
291 to 517 cost 0.0307307
517 to 838 cost 0.0138264
838 to 834 cost 0.048644
834 to 892 cost 0.031419
892 to 511 cost 0.0129436
511 to 378 cost 0.00926815
378 to 189 cost 0.0151337
189 to 637 cost 0.0359739
637 to 328 cost 0.0715469
328 to 296 cost 0.019443
296 to 878 cost 0.00970971
878 to 805 cost 0.021461
805 to 394 cost 0.0307303
394 to 564 cost 0.00530747
564 to 803 cost 0.0149948
803 to 459 cost 0.0118338
459 to 671 cost 0.0179393
671 to 766 cost 0.0423
766 to 192 cost 0.0437059
192 to 594 cost 0.035095
594 to 544 cost 0.0181045
544 to 573 cost 0.0554636
573 to 739 cost 0.0366535
739 to 718 cost 0.0361258
718 to 414 cost 0.0268235
414 to 419 cost 0.0101585
419 to 151 cost 0.0151262
151 to 435 cost 0.00434265
435 to 370 cost 0.0137899
370 to 967 cost 0.0208741
967 to 37 cost 0.580025
37 to 747 cost 0.0172065
747 to 957 cost 0.0387937
957 to 86 cost 0.00147598
86 to 334 cost 0.00179319
334 to 130 cost 0.0298
130 to 783 cost 0.0153179
783 to 156 cost 0.00732956
156 to 577 cost 0.0225397
577 to 170 cost 0.0194003
170 to 305 cost 0.0364453
305 to 100 cost 0.0108407
100 to 182 cost 0.0348189
182 to 859 cost 0.0569403
859 to 318 cost 0.0557356
318 to 678 cost 0.0414716
678 to 558 cost 0.074287
558 to 673 cost 0.0359935
673 to 948 cost 0.143782
948 to 368 cost 0.070355
368 to 730 cost 0.0466876
730 to 902 cost 0.118734
902 to 381 cost 0.0131738
381 to 107 cost 0.0111106
107 to 401 cost 0.0170163
401 to 122 cost 0.00761114
122 to 351 cost 0.00832385
351 to 294 cost 0.0244257
294 to 146 cost 0.0190312
146 to 385 cost 0.0250808
385 to 519 cost 0.0287459
519 to 774 cost 0.0247139
774 to 598 cost 0.0127086
598 to 851 cost 0.0173336
851 to 985 cost 0.0121389
985 to 603 cost 0.0115872
603 to 522 cost 0.003022
522 to 713 cost 0.0551784
713 to 770 cost 0.0284728
770 to 274 cost 0.00474674
274 to 275 cost 0.0212231
275 to 651 cost 0.00256037
651 to 620 cost 0.0176946
620 to 972 cost 0.025921
972 to 515 cost 0.0299417
515 to 959 cost 0.01396
959 to 316 cost 0.0166134
316 to 242 cost 0.0204297
242 to 42 cost 0.0205438
42 to 408 cost 0.0281052
408 to 560 cost 0.0208354
560 to 700 cost 0.0473005
700 to 566 cost 0.117052
566 to 325 cost 0.0026834
325 to 480 cost 0.0274507
480 to 293 cost 0.0305132
293 to 355 cost 0.0294044
355 to 714 cost 0.00857664
714 to 669 cost 0.0302576
669 to 687 cost 0.00651973
687 to 139 cost 0.0124884
139 to 706 cost 0.0135006
706 to 311 cost 0.00951421
311 to 870 cost 0.011042
870 to 934 cost 0.0368898
934 to 971 cost 0.0112981
971 to 288 cost 0.0202007
288 to 228 cost 0.0631848
228 to 943 cost 0.00328895
943 to 264 cost 0.0341176
264 to 936 cost 0.0168706
936 to 443 cost 0.025964
443 to 461 cost 0.0394367
461 to 109 cost 0.0231276
109 to 526 cost 0.0500614
526 to 27 cost 0.0372786
27 to 617 cost 0.00762351
617 to 854 cost 0.0312083
854 to 963 cost 0.0190292
963 to 6 cost 0.017436
6 to 108 cost 0.0539425
108 to 361 cost 0.00653385
361 to 252 cost 0.0274571
252 to 746 cost 0.0159771
746 to 75 cost 0.0502468
75 to 110 cost 0.0225481
110 to 507 cost 0.0270653
507 to 229 cost 0.0281862
229 to 710 cost 0.0269118
710 to 976 cost 0.0267366
976 to 643 cost 0.0467258
643 to 711 cost 0.00722471
711 to 751 cost 0.0340203
751 to 143 cost 0.0283069
143 to 338 cost 0.0130318
338 to 241 cost 0.0360087
241 to 857 cost 0.0413709
857 to 468 cost 0.0156115
468 to 317 cost 0.00329193
317 to 910 cost 0.00883876
910 to 542 cost 0.0519964
542 to 861 cost 0.0396552
861 to 174 cost 0.00718563
174 to 144 cost 0.000784225
144 to 172 cost 0.0509487
172 to 533 cost 0.0256212
533 to 206 cost 0.0118647
206 to 893 cost 0.0163205
893 to 848 cost 0.00248829
848 to 896 cost 0.0256071
896 to 898 cost 0.0416677
898 to 953 cost 0.015109
953 to 505 cost 0.0143954
505 to 175 cost 0.038706
175 to 260 cost 0.0138599
260 to 329 cost 0.0177222
329 to 992 cost 0.0135948
992 to 761 cost 0.0872005
761 to 758 cost 0.0231554
758 to 202 cost 0.0262642
202 to 807 cost 0.0181522
807 to 825 cost 0.0251164
825 to 129 cost 0.017808
129 to 421 cost 0.00991937
421 to 510 cost 0.0138102
510 to 185 cost 0.0526822
185 to 682 cost 0.00739443
682 to 395 cost 0.0237926
395 to 557 cost 0.0321207
557 to 500 cost 0.0229971
500 to 147 cost 0.023561
147 to 121 cost 0.0194289
121 to 284 cost 0.0113112
284 to 225 cost 0.0234815
225 to 477 cost 0.0203259
477 to 955 cost 0.0830787
955 to 575 cost 0.0261231
575 to 279 cost 0.0183104
279 to 601 cost 0.0132554
601 to 12 cost 0.0266154
12 to 579 cost 0.0133706
579 to 178 cost 0.0335177
178 to 782 cost 0.0259921
782 to 456 cost 0.0107596
456 to 424 cost 0.068812
424 to 820 cost 0.0347353
820 to 429 cost 0.0348345
429 to 111 cost 0.0312333
111 to 337 cost 0.0440372
337 to 797 cost 0.0330298
797 to 138 cost 0.027059
138 to 852 cost 0.133357
852 to 0
Length: 0


Length: 32.1098
```
