using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralClicker
{
    public class Neuron
    {
        private const double LEARNING_RATE = 0.1;

        private double[] _weights;

        public Neuron(double[] weights)
        {
            _weights = weights;
        }

        public Neuron(int count)
        {
            var rng = new Random();

            var weights = new double[count];

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rng.NextDouble() * 5 - 1;
            }

            _weights = weights;
        }

        public int Feedforward(double[] inputs)
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * _weights[i];
            }

            return (int)Math.Round(sum);
        }

        public void Train(double[] inputs, double desired)
        {
            int guess = Feedforward(inputs);
            double error = desired - guess;

            for (int i = 0; i < _weights.Length; i++)
            {
                _weights[i] += LEARNING_RATE * error * inputs[i];
            }
        }
    }
}
