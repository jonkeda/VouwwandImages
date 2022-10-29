using System.Collections.Generic;
using System.ComponentModel;
using GeneticSharp;
using GeneticSharp.Extensions;

namespace VouwwandImages.ViewModels.Calculations
{
    [DisplayName("Function Builder")]
    public class FunctionBuilder : GeneticAlgorithmControllerBase
    {
        #region Fields
        private readonly FunctionBuilderFitness _fitness;
        private readonly List<FunctionBuilderInput> _inputs;
        private readonly int _maxOperations;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the available operations.
        /// </summary>
        /// <value>The available operations.</value>
        private readonly string[] _availableOperations;
        private readonly string[] _parameterNames;

        #endregion

        #region Methods    
        public FunctionBuilder(List<FunctionBuilderInput> inputs, int maxOperations)
        {
            _inputs = inputs;
            _fitness = new FunctionBuilderFitness(_inputs.ToArray());
            _parameterNames = _fitness.ParameterNames;
            _maxOperations = maxOperations;

            //AvailableOperations = new ReadOnlyCollection<string>(new[] { "+", "-", "/", "*" });
            _availableOperations = new [] { "+", "*" };
        }
        #endregion
        
        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// Configure the Genetic Algorithm.
        /// </summary>
        /// <param name="ga">The genetic algorithm.</param>
        public override void ConfigGA(GeneticAlgorithm ga)
        {
            ga.CrossoverProbability = 0.9f; // 0.1f
            ga.MutationProbability = 0.9f; // 0.4f//
            ga.Reinsertion = new ElitistReinsertion();
        }

        /// <summary>
        /// Draws the specified best chromosome.
        /// </summary>
        /// <param name="bestChromosome">The best chromosome.</param>
        public override string Draw(IChromosome bestChromosome)
        {
            var best = bestChromosome as FunctionBuilderChromosome;
            return best.BuildFunction();

/*            var sb = new StringBuilder();
*//*            foreach (var input in _inputs)
            {
                sb.AppendLine($@"{string.Join(", ", input.Arguments)} = {input.ExpectedResult}");
            }*//*
            //sb.AppendLine($"Max operations: {_maxOperations}");
            if (best != null)
            {
                sb.AppendLine($"Function: {best.BuildFunction()}");
            }
            return sb.ToString();
*/        }

        /// <summary>
        /// Creates the chromosome.
        /// </summary>
        /// <returns>The sample chromosome.</returns>
        public override IChromosome CreateChromosome()
        {
           
            return new FunctionBuilderChromosome(_availableOperations, _parameterNames, _maxOperations);
        }

        /// <summary>
        /// Creates the fitness.
        /// </summary>
        /// <returns>The fitness.</returns>
        public override IFitness CreateFitness()
        {
            return _fitness;
        }

        /// <summary>
        /// Creates the crossover.
        /// </summary>
        /// <returns>The crossover.</returns>
        public override ICrossover CreateCrossover()
        {
            //return new 
            return new ThreeParentCrossover();
        }

        /// <summary>
        /// Creates the mutation.
        /// </summary>
        /// <returns>The mutation.</returns>
        public override IMutation CreateMutation()
        {
            //return new PartialShuffleMutation();
            //return new InsertionMutation();
            //return new ReverseSequenceMutation();
            //return new DisplacementMutation();
            return new UniformMutation(true);
        }

        /// <summary>
        /// Creates the selection.
        /// </summary>
        /// <returns>The selection.</returns>
        public override ISelection CreateSelection()
        {
            //return new TruncationSelection();
            //return new RankSelection();
            //return new StochasticUniversalSamplingSelection();
            //return new RouletteWheelSelection();
            return new EliteSelection();
        }

        /// <summary>
        /// Creates the termination.
        /// </summary>
        /// <returns> 
        /// The termination.
        /// </returns>
        public override ITermination CreateTermination()
        {
            return new FitnessThresholdTermination(0);
        }
        #endregion
    }
}