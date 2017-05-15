using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GeneticAlgorithm.GeneticAlgorithm;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*
            //parameters
            int countPopulation = 200;
            int countChromossomes = 10;
            int countIterations = 100;
            double mutationRate = 0.05;
            double minValue = -5.12;
            double maxValue = 5.12;
            SelectionType st = SelectionType.BestOf;
            FitnessFunction ff = FitnessFunction.Rastrigin;

            //run algorithm
            Console.WriteLine(String.Format("Parameters:\nPopulation: {0}\nChromossomes: {1}\nGenerations: {2}\nMutation Rate: {3}\nMinimum Value: {4}\nMaximum Value: {5}\n", 
                countPopulation, countChromossomes, countIterations, mutationRate, minValue, maxValue));
            Console.WriteLine("Run Genetic Algorithm with Rastrigin function? (Y/n)");

            System.Windows.Forms.DataVisualization.Charting.Chart ch = new System.Windows.Forms.DataVisualization.Charting.Chart();

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                GeneticAlgorithm.RunGeneticAlgorithm(countPopulation, countChromossomes, countIterations, mutationRate, minValue, maxValue, st, ff, ch);
            }
           
            Console.Read();
            */

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Generator());
            
        }
    }
}
