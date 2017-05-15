# ALGntc - A simple genetic algorithm

ALGntc is an open source genetic algorithm built in C# licensed under MIT. The intent is to provide a simple, yet complete, way to use and learn about the genetic algorithm, being possible to the user change the parameters in a easy way and preview the evolution of process by a chart in real time.

Feel free to use and change the soure code the way you want.

### Getting Started

What is a genetic algorithm? In a simple way, is a method to solve complex mathematical problems using an evolutionary approach, doing crossovers of possible solutions until the best fitting result appears. You can read more about genetic algorithms in [here](https://www.mathworks.com/discovery/genetic-algorithm.html) or [here](https://www.doc.ic.ac.uk/~nd/surprise_96/journal/vol1/hmw/article1.html) or just google it.

To use this code just download and run it with a C# compatible IDE, like [Visual Studio Communnity](https://www.visualstudio.com/pt-br/vs/community/) which is free.

### Parameters

To execute the genetic algorithm just type all parameters in the respectives text boxes.
* Population: Number of possible solutions [INT+]
* Chromossomes: Number of genes of each solution in population [INT+]
* Min. Chromossome Value: Minimum value of each chromossome [DOUBLE]
* Max. Chromossome Value: Maximum value of each chromossome [DOUBLE]
* Mutation Rate: Percentual chance to chromossome's solution to randomly changes [DOUBLE]
* Selection Type: Selection type used in the algorithm [ENUM]
* Fitness Function: Fitness function used in the algorithm [ENUM]
* Number of Iterations: Number of iterations [INT]

### Custom Fitness Function and/or Selection Type

You can add your own fitness function and/or selection type changing the source code.
* Fitness Function: It's possible to add a fitness function in the 'Custom' method located on 'Functions.cs'
* Selection Type: It's possible to add a selection type changing the 'GenerateNewPopulationCustom' method located on 'GeneticAlgorithm.cs'

### License

This source code is licensed under MIT License. Any question see LICENSE file.

