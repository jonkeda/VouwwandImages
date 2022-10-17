using System;
using System.Linq;
using System.Text;
using GeneticSharp;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels.Calculations
{
    public class FunctionBuilderRunner : PropertyNotifier
    {
        private readonly FunctionBuilder _functionBuilder;
        private GeneticAlgorithm? _ga;
        private string _outcome;

        public string Outcome
        {
            get { return _outcome; }
            set { SetProperty(ref _outcome, value); }
        }

        public FunctionBuilderRunner(FunctionBuilder functionBuilder)
        {
            _functionBuilder = functionBuilder;
        }

        public void Run()
        {
            _functionBuilder.Initialize();

            var selection = _functionBuilder.CreateSelection();
            var crossover = _functionBuilder.CreateCrossover();
            var mutation = _functionBuilder.CreateMutation();
            var fitness = _functionBuilder.CreateFitness();
            var population = new Population(100, 200, _functionBuilder.CreateChromosome())
            {
                GenerationStrategy = new PerformanceGenerationStrategy()
            };

            _ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = _functionBuilder.CreateTermination()
            };

            _ga.GenerationRan += ShowProgress;
            _functionBuilder.ConfigGA(_ga);
            _ga.Start();
        }

        private void ShowProgress(object? sender, EventArgs e)
        {
            if (_ga == null)
            {
                return;
            }

/*            if (_ga.Population.GenerationsNumber % 100 != 0)
            {
                return;
            }*/
            //var terminationName = _ga.Termination.GetType().Name;
            var bestChromosome = _ga.Population.BestChromosome;
            var sb = new StringBuilder();
            //sb.AppendLine($"Termination: {terminationName}");
            sb.AppendLine($"Generations: {_ga.Population.GenerationsNumber}");
            sb.AppendLine($"Fitness: {bestChromosome.Fitness:F0}");
            sb.AppendLine($"Time: {_ga.TimeEvolving}");
            sb.AppendLine($"Speed (gen/sec): {(_ga.Population.GenerationsNumber / _ga.TimeEvolving.TotalSeconds):0}");

            foreach (var chromosome in _ga.Population.CurrentGeneration.Chromosomes.Take(4))
            {
                sb.AppendLine(_functionBuilder.Draw(chromosome));
            }

            Outcome = sb.ToString();
        }

        public void Stop()
        {
            _ga?.Stop();
        }

        public void Resume()
        {
            _ga?.Resume();
        }

    }


}