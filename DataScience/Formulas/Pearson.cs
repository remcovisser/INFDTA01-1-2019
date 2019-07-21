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
            int parsedProducts = 0;

            // Initialize variables and set default value
            double sumOfX, sumOfY, sumOfXSquared, sumOfYSquared, sumOfXTimesY;
            sumOfX = sumOfY = sumOfXSquared = sumOfYSquared = sumOfXTimesY = 0.0;

            // Calculate the values using Sigma
            for (int i = 0; i < n; i++)
            {
                if (user1Ratings[i] == 0.0 || user2Ratings[i] == 0.0)
                {
                    continue;
                }

                parsedProducts++;
                sumOfX += user1Ratings[i];
                sumOfY += user2Ratings[i];
                sumOfXSquared += Math.Pow(user1Ratings[i], 2);
                sumOfYSquared += Math.Pow(user2Ratings[i], 2);
                sumOfXTimesY += user1Ratings[i] * user2Ratings[i];
            }

            //  Calculate the rest of the values using the values from the loop
            double avarageOfSumOfYSquared = Math.Pow(sumOfY, 2) / parsedProducts;
            double avarageOfSumOfXSquared = Math.Pow(sumOfX, 2) / parsedProducts;
            double avarageOfSumOfXTimesSumOfY = sumOfX * sumOfY / parsedProducts;

            // Calculate the final values
            double x = sumOfXTimesY - avarageOfSumOfXTimesSumOfY;
            double y = Math.Sqrt(sumOfXSquared - avarageOfSumOfXSquared);
            double z = Math.Sqrt(sumOfYSquared - avarageOfSumOfYSquared);

            // Calculate the result
            return x / (y * z);
        }
    }
}