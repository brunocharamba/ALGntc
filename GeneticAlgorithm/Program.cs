using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //parameters
            int countPopulation = 200;
            int countChromossomes = 10;
            int countIterations = 100000;
            double mutationRate = 0.05;
            double minValue = -5.12;
            double maxValue = 5.12;

            //run algorithm
            Console.WriteLine(String.Format("Parameters:\nPopulation: {0}\nChromossomes: {1}\nGenerations: {2}\nMutation Rate: {3}\nMinimum Value: {4}\nMaximum Value: {5}\n", 
                countPopulation, countChromossomes, countIterations, mutationRate, minValue, maxValue));
            Console.WriteLine("Run Genetic Algorithm with Rastrigin function? (Y/n)");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                GeneticAlgorithm.RunGeneticAlgorithm(countPopulation, countChromossomes, countIterations, mutationRate, minValue, maxValue,
                GeneticAlgorithm.SelectionType.BestOf, GeneticAlgorithm.FitnessFunction.Rastrigin);
            }
           
            Console.Read();

        }
    }
}
