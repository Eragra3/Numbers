using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NeuralClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private char[] _letters = { 'a','b','c','d','e','f','g','h',
            'i','j','k','l','m','n','o','p', 'q','r','s',
            't','u','v','w','x','y','z'};

        private Random _rng = new Random();
        private string SECRET_TEXT = "daniel";
        private int _currentLetter = 0;

        private Neuron _neuron = new Neuron();

        public MainWindow()
        {
            InitializeComponent();

            NameTB.Text = string.Empty;
            RunNN();

        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private void RunNN()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var letter = GetNeuralGuess();

            AddLetter(letter);

            if (NameTB.Text.Equals(SECRET_TEXT))
            {
                NameTB.Background = Brushes.Green;
                dispatcherTimer.Stop();
            }

            TeachThisIdiot();

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void TeachThisIdiot()
        {
            var correctLetter = SECRET_TEXT[_currentLetter];
            var correctLetterIndex = Array.FindIndex(_letters, e => e == correctLetter);

            var inputs = new double[7] { 0, 0, 0, 0, 0, 0, -1 };
            inputs[_currentLetter] = 1;

            _neuron.Train(inputs, correctLetterIndex);
        }

        private char GetNeuralGuess()
        {
            var inputs = new double[7] { 0, 0, 0, 0, 0, 0, -1 };
            inputs[_currentLetter] = 1;

            var guess = _neuron.Feedforward(inputs);

            var letter = '@';
            if (guess >= 0 && guess < _letters.Length)
            {
                letter = _letters[guess];
            }

            return letter;
        }

        public void AddLetter(char letter)
        {
            if (_currentLetter == 0)
            {
                ConsoleTB.AppendText(" | " + NameTB.Text);
                NameTB.Text = string.Empty;
            }

            NameTB.Text += letter;

            _currentLetter++;
            _currentLetter %= SECRET_TEXT.Length;

            ConsoleTB.ScrollToEnd();
        }
    }
}
