using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Euclidean : InterfaceDistance
    {
        public double CalculateDistance(Tuple<List<double>, List<double>> parsedRatings)
        {
            List<double> user1Ratings = parsedRatings.Item1;
            List<double> user2Ratings = parsedRatings.Item2;
            double distance = 0.0;

            // Sum of user1Ratings i - user1Ratings2 i squared
            for (int i = 0; i < user1Ratings.Count; i++)
            {
                distance += Math.Pow(user1Ratings[i] - user2Ratings[i], 2);
            }

            distance = Math.Sqrt(distance);

            return 1 / (1 + distance);
        }
    }
}