using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public static class GeneticAlgorithm
    {
        public static double fitness = double.MaxValue;
        public static double[] bestIndividue;

        #region Run Genetic Algorithm

        /// <summary>
        /// Run the genetic algorithm and write in the console the best individue at every generation of the population 
        /// </summary>
        /// <param name="countPopulation">Number of individues in the population</param>
        /// <param name="countChromossomes">Number of chromossomes of each individue</param>
        /// <param name="numberIteration">Number of generations of algorithm</param>
        /// <param name="mutationRate">Possibility of mutation in one chromossome, the value range between 0.0 and 1.0</param>
        /// <param name="minValue">Minimum value of chromossome</param>
        /// <param name="maxValue">Maximum value of chromossome</param>
        /// <param name="st">Type of selection</param>
        /// <param name="ff">Type of fitness function</param>
        public static void RunGeneticAlgorithm(int countPopulation, int countChromossomes, int numberIteration, double mutationRate, double minValue, double maxValue,
            SelectionType st, FitnessFunction ff)
        {
            double[][] pop = InitializatePopulation(countPopulation, countChromossomes, minValue, maxValue);
            double[][] off;
            double[][] nxt;

            for (int i = 0; i < numberIteration; i++)
            {
                off = GenerateOffspring(pop, mutationRate);
                nxt = GenerateNewPopulation(pop, off, st, ff);

                pop = nxt;

                Console.WriteLine(String.Format("{0} : {1}", i, ToString(nxt[0])));
            }
        }

        /// <summary>
        /// Randomly generates values for the entire population and their chromossomes
        /// </summary>
        /// <param name="countPopulation">Number of individues of population</param>
        /// <param name="countChromossomes">Number of chromossomes of each individue</param>
        /// <returns>Initial population</returns>
        public static double[][] InitializatePopulation(int countPopulation, int countChromossomes, double minValue, double maxValue)
        {
            double[][] pop = new double[countPopulation][];

            for (int i = 0; i < countPopulation; i++)
            {
                pop[i] = new double[countChromossomes];

                for (int j = 0; j < countChromossomes; j++)
                {
                    Random r = GenerateRandom();
                    pop[i][j] = GenerateRandomNumber(r, minValue, maxValue);
                }
            }

            return pop;
        }

        /// <summary>
        /// Generate the offspring based on their parents.
        /// This method do a crossover between 2 randomly choosen parents, and generates 2 children, each one with
        /// half of their parents chromossomes. Every time that a chromossome is copied has a chance to mutate, which change randomly this chromossome
        /// </summary>
        /// <param name="parents">Parents matrix</param>
        /// <param name="mutationRate">Possibility of mutation (values between 0.0 and 1.0)</param>
        /// <returns>The children (offspring) of the parents</returns>
        public static double[][] GenerateOffspring(double[][] parents, double mutationRate)
        {
            Random r = GenerateRandom();

            //randomize and separate parents (50/50)
            ShuffleArray(GenerateRandom(), parents);
            
            //do crossover based on central pivot
            int pivot = (parents[0].GetLength(0) / 2); //central pivot position
            double[][] offspring = new double[parents.GetLength(0)][]; //create empty array with same size of parents

            for (int i = 0, ii = parents.GetLength(0) - 1; i < offspring.GetLength(0); i++, ii--)
            {
                offspring[i] = new double[parents[0].GetLength(0)];

                for (int j = 0; j < parents[0].GetLength(0); j++)
                {
                    //if mutation occours, change randomly the chromossome instead of get from the parents
                    //if not, get from the parents
                    if (r.NextDouble() <= mutationRate)
                    {
                        offspring[i][j] = GenerateRandomNumber(r, -5.12, 5.12);
                    }
                    else if (j < pivot)
                    {
                        offspring[i][j] = parents[i][j];
                    }
                    else
                    {
                        offspring[i][j] = parents[ii][j];
                    }
                }
            }

            //return new population (offspring)
            return offspring;
        }

        /// <summary>
        /// Creates the next generation of the population based on the best fitting indiviudes between parents and offspring
        /// </summary>
        /// <param name="parents">Parent's matrix</param>
        /// <param name="offspring">Offspring's matrix</param>
        /// <param name="st">Type of selection</param>
        /// <param name="ff">Type of fitness function</param>
        /// <returns>Mixed population with the best fitting individues between parents and offspring</returns>
        public static double[][] GenerateNewPopulation(double[][] parents, double[][] offspring, SelectionType st, FitnessFunction ff)
        {
            List<Tuple<double, double[]>> newPop = new List<Tuple<double, double[]>>();
            double[][] nextGen = new double[parents.GetLength(0)][];

            //if only individues with best fitness progress
            if (st == SelectionType.BestOf)
            {
                //organizing new population
                if(ff == FitnessFunction.Rastrigin)
                {
                    //adding parents and offspring to one list
                    for (int i = 0; i < parents.GetLength(0); i++) newPop.Add(new Tuple<double, double[]>(Functions.Rastrigin(parents[i]), parents[i]));
                    for (int i = 0; i < offspring.GetLength(0); i++) newPop.Add(new Tuple<double, double[]>(Functions.Rastrigin(offspring[i]), offspring[i]));
                }

                //sorting the list from min to max
                newPop.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                //removing half of the population to match the count parameter
                for (int i = 0; i < parents.GetLength(0); i++)
                {
                    nextGen[i] = new double[parents[0].GetLength(0)];
                    nextGen[i] = newPop[i].Item2;
                }

            }

            fitness = newPop[0].Item1;

            return nextGen;
        }

        #endregion

        #region Enums

        /// <summary>
        /// The selection types to choose the individues how pass to the next generation
        /// </summary>
        public enum SelectionType{
            BestOf, Null
        }

        /// <summary>
        /// The fitness functions
        /// </summary>
        public enum FitnessFunction
        {
            Rastrigin, Sphere, Custom
        }

        #endregion

        #region Helper

        private static void ShuffleArray(Random r, double[][] pop)
        {
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < pop.GetLength(0); t++)
            {
                double[] tmp = pop[t];
                
                int x = r.Next(t, pop.GetLength(0));
                pop[t] = pop[x];
                pop[x] = tmp;
            }
        }

        /// <summary>
        /// Generate a true Random number based on a seed
        /// </summary>
        /// <returns>Random object</returns>
        private static Random GenerateRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }

        /// <summary>
        /// Generate a random double between two numbers
        /// </summary>
        /// <param name="r">Random object, try to use a GenerateRandom()</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns></returns>
        private static double GenerateRandomNumber(Random r, double min, double max)
        {
            return r.NextDouble() * (max - min) + min;
        }

        public static string ToString(double[][] pop)
        {
            string otp = String.Empty;

            for (int i = 0; i < pop.GetLength(0); i++)
            {

                for (int j = 0; j < pop[0].GetLength(0); j++)
                {
                    otp += Math.Round(pop[i][j], 8, MidpointRounding.AwayFromZero) + "\t";
                }

                otp += "\t | " + Functions.Rastrigin(pop[i]);
                otp += "\n";
            }

            return otp;
        }

        public static string ToString(double[] pop)
        {
            string otp = String.Empty;

            for (int i = 0; i < pop.GetLength(0); i++)
            {
                otp += Math.Round(pop[i], 8, MidpointRounding.AwayFromZero) + "\t";
            }

            otp += "\t | " + Functions.Rastrigin(pop);

            return otp;
        }

        #endregion
    }
}
