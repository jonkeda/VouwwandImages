using System;
using System.Collections.ObjectModel;
using GeneticSharp;
using GeneticSharp.Extensions;

namespace VouwwandImages.ViewModels.Calculations
{
    /// <summary>
    /// Function builder fitness.
    /// </summary>
    public class FunctionBuilderFitness : IFitness
    {
        #region Fields
        private readonly FunctionBuilderInput[] m_inputs;
        private readonly string[] m_parameterNames;

        public string[] ParameterNames
        {
            get { return m_parameterNames; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Extensions.Mathematic.FunctionBuilderFitness"/> class.
        /// </summary>
        /// <param name="inputs">The arguments values and expected results of the function.</param>
        public FunctionBuilderFitness(params FunctionBuilderInput[] inputs)
        {
            m_inputs = inputs;
            m_parameterNames = GetParameterNames(m_inputs[0].Arguments.Count);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the parameter names.
        /// </summary>
        /// <returns>The parameter names.</returns>
        /// <param name="parametersCount">Parameters count.</param>
        public static string[] GetParameterNames(int parametersCount)
        {
            string[] parameterNames = new string[parametersCount];

            for (int i = 0; i < parametersCount; i++)
            {
                parameterNames[i] = ((char)(i + 65)).ToString();
            }

            return parameterNames;
        }

        /// <summary>
        /// Performs the evaluation against the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome to be evaluated.</param>
        /// <returns>The fitness of the chromosome.</returns>
        public double Evaluate(IChromosome chromosome)
        {
            var c = chromosome as FunctionBuilderChromosome;
            var function = c.BuildFunction();
            var fitness = 0.0;

            foreach (var input in m_inputs)
            {
                try
                {
                    var result = GetFunctionResult(function, input);
                    var diff = Math.Abs(result - input.ExpectedResult);

                    fitness += diff;
                }
                catch (Exception e)
                {
                    return double.MinValue;
                }
            }

            return fitness * -1;
        }

        /// <summary>
        /// Gets the function result.
        /// </summary>
        /// <returns>The function result.</returns>
        /// <param name="function">The function.</param>
        /// <param name="input">The arguments values and expected results of the function.</param>
        public double GetFunctionResult(string function, FunctionBuilderInput input)
        {
            var expression = new NCalc.Expression(function);

            for (int i = 0; i < m_parameterNames.Length; i++)
            {
                expression.Parameters.Add(m_parameterNames[i], input.Arguments[i]);
            }

            var result = expression.Evaluate();
            if (result is int)
            {
                return (double)((int) result);
            }
            return (double)result;
        }
        #endregion
    }
}
