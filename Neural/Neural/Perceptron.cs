using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural
{
    public class Perceptron
    {
        private const double LEARNING_RATE = 0.01;

        private double[] _weights;

        public Perceptron(double[] weights)
        {
            _weights = weights;
        }

        public int Feedforward(double[] inputs)
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * _weights[i];
            }

            return Activate(sum);
        }

        private int Activate(double sum)
        {
            return sum > 0 ? 1 : -1;
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
