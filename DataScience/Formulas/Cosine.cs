using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Cosine : InterfaceDistance
    {
        public double CalculateDistance(Tuple<List<double>, List<double>> parsedRatings)
        {
            // Get the parsed data
            List<double> user1Ratings = parsedRatings.Item1;
            List<double> user2Ratings = parsedRatings.Item2;

            // Initialize variables and set default value
            double sumOfXSquared, sumOfYSquared, sumOfXTimesY;
            sumOfXSquared = sumOfYSquared = sumOfXTimesY = 0.0;

            // Calculate the values for each rating
            for (int i = 0; i < user1Ratings.Count; i++)
            {
                sumOfXTimesY += user1Ratings[i] * user2Ratings[i];
                sumOfXSquared += Math.Pow(user1Ratings[i], 2);
                sumOfYSquared += Math.Pow(user2Ratings[i], 2);
            }

            return sumOfXTimesY / (Math.Sqrt(sumOfXSquared) * Math.Sqrt(sumOfYSquared));
        }
    }
}