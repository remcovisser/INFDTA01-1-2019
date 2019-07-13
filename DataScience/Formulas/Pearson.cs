using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Pearson : InterfaceDistance
    {
        public double CalculateDistance(Tuple<List<double>, List<double>> parsedRatings)
        {
            List<double> user1Ratings = parsedRatings.Item1;
            List<double> user2Ratings = parsedRatings.Item2;
            int n = user1Ratings.Count;

            // Set default values
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSquared = 0;
            double sumOfYSquared = 0;
            double sumOfXTimesY = 0;

            // Calculate the values using sigma
            for (int i = 0; i < n; i++)
            {
                sumOfX += user1Ratings[i];
                sumOfY += user2Ratings[i];
                sumOfXSquared += Math.Pow(user1Ratings[i], 2);
                sumOfYSquared += Math.Pow(user2Ratings[i], 2);
                sumOfXTimesY += user1Ratings[i] * user2Ratings[i];
            }

            //  Calculate the rest of the values using the values from the loop
            double avarageOfSumOfYSquared = sumOfY * sumOfY / n;
            double avarageOfSumOfXSquared = sumOfX * sumOfX / n;
            double avarageOfSumOfXTimesSumOfY = sumOfX * sumOfY / n;

            // Calculate the final values
            double x = sumOfXTimesY - avarageOfSumOfXTimesSumOfY;
            double y = Math.Sqrt(sumOfXSquared - avarageOfSumOfXSquared);
            double z = Math.Sqrt(sumOfYSquared - avarageOfSumOfYSquared);

            // Calculate the result
            return x / (y * z);
        }
    }
}