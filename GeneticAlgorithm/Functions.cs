using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneticAlgorithm.GeneticAlgorithm;

namespace GeneticAlgorithm
{
    public static class Functions
    {
        public static double Fitness(FitnessFunction ff, double[] values)
        {
            switch (ff)
            {
                case FitnessFunction.Rastrigin:
                    return Rastrigin(values);
                case FitnessFunction.Custom:
                    return Custom(values);
                default:
                    return 0.0;
            }
        }

        public static double Rastrigin(double[] values)
        {
            //y = A*n + SUM[xi² - A*cos(2*pi*xi)]
            //A = 10
            const double A = 10;

            double y = A * values.Length; //y = A*n

            for (int i = 0; i < values.Length; i++)
            {
                y += Math.Pow(values[i], 2) - A * Math.Cos(2 * Math.PI * values[i]);
            }

            return y;
        }

        public static double Custom(double[] value)
        {
            /*
                Create your own function here!
            */
            return 0.0;
        }
    }
}
