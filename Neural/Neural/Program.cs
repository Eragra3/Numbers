using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 10000;

            const int training_iterations = 1000;

            int rightAnswers = 0;

            var neuron = new Perceptron(new double[2] { 1.0, 1 });

            var rng = new Random();

            ////train
            //for (int i = 0; i < training_iterations; i++)
            //{
            //    var inputs = new double[1] { rng.Next(0, 101) };

            //    neuron.Train(inputs, SecretFunction(inputs[0]));
            //}

            ////u wot m8
            //for (int i = 0; i < iterations; i++)
            //{
            //    var inputs = new double[1] { rng.Next(0, 101) };

            //    var decision = neuron.Feedforward(inputs);

            //    if (SecretFunction(inputs[0]) == decision) rightAnswers++;
            //}


            for (int i = 0; i < 100; i++)
            {
                var correctAnswers = 0;
                var kek = 0;
                for (int j = 0; j < 1000; j++)
                {
                    var inputs = new double[2] { rng.Next(0, 101), -1 };

                    var decision = neuron.Feedforward(inputs);

                    if (decision == 1) kek++;

                    if (SecretFunction(inputs[0]) == decision) correctAnswers++;
                }

                Console.WriteLine($"Iteration {i}");
                Console.WriteLine($"{correctAnswers}/{10}");
                if (kek == 0) kek = 10;
                Console.WriteLine($"{kek}");
                Console.WriteLine();

                for (int j = 0; j < 10; j++)
                {
                    var inputs = new double[2] { rng.Next(0, 101), -1 };

                    neuron.Train(inputs, SecretFunction(inputs[0]));
                }
            }

            Console.ReadKey();
        }

        private static int SecretFunction(double x)
        {
            return x > 30 ? 1 : -1;
        }
    }
}
