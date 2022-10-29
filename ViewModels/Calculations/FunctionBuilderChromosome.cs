using System.Globalization;
using System.Text;
using GeneticSharp;

namespace VouwwandImages.ViewModels.Calculations
{
    /// <summary>
    /// Function builder chromosome.
    /// </summary>
    public sealed class FunctionBuilderChromosome : ChromosomeBase
    {
        #region Constants
        /// <summary>
        /// The max integer operation.
        /// </summary>
        public const int MaxIntOperation = 9;
        #endregion

        #region Fields
        private readonly string[] _availableOperations;
        private readonly string[] _parameterNames;
        private readonly int _maxOperations;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Extensions.Mathematic.FunctionBuilderChromosome"/> class.
        /// </summary>
        /// <param name="availableOperations">Available operations.</param>
        /// <param name="parameterNames"></param>
        /// <param name="maxOperations">Max operations.</param>
        public FunctionBuilderChromosome(string[] availableOperations, string[] parameterNames, int maxOperations) : base(maxOperations)
        {
            _availableOperations = availableOperations;
            _parameterNames = parameterNames;
            _maxOperations = maxOperations;

            for (int i = 0; i < Length; i++)
            {
                ReplaceGene(i, GenerateGene(i));
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Builds the function.
        /// </summary>
        /// <returns>The function.</returns>
        public string BuildFunction()
        {
            var builder = new StringBuilder();

            foreach (var g in GetGenes())
            {
                var op = g.Value.ToString();

                if (!string.IsNullOrEmpty(op))
                {
                    builder.AppendFormat("{0} ", op);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a new chromosome using the same structure of this.
        /// </summary>
        /// <returns>The new chromosome.</returns>
        public override IChromosome CreateNew()
        {
            return new FunctionBuilderChromosome(_availableOperations, _parameterNames, _maxOperations);
        }

        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="geneIndex">Gene index.</param>
        public override Gene GenerateGene(int geneIndex)
        {
            var rnd = RandomizationProvider.Current;
            if (geneIndex % 2 == 1)
            {
                return new Gene(_availableOperations[rnd.GetInt(0, _availableOperations.Length)]);
            }
            if (rnd.GetInt(0, 2) == 0)
            {
                return new Gene(rnd.GetInt(1, MaxIntOperation + 1).ToString(CultureInfo.InvariantCulture));
            }
            return new Gene(_parameterNames[rnd.GetInt(0, _parameterNames.Length)]);
        }
        #endregion
    }
}
