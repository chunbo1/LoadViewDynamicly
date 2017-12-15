using System;

namespace Core.Extensions
{
    ///<summary>
    /// Extension Methods for double
    ///</summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Returns annualized value
        /// </summary>
        /// <param name="value">Input Value</param>
        /// <param name="period">period</param>
        /// <param name="numPeriods">Total Number of Periods</param>
        /// <returns>Return Value</returns>
        public static double Annualize(this double value, double period, double numPeriods)
        {
            return Math.Pow((1 + value), period / numPeriods) - 1;
        }
    }
}
