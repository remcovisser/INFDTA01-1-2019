using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Euclidean : InterfaceDistance
    {
        public double CalculateDistance(Tuple<List<double>, List<double>> parsed_ratings)
        {
            List<double> user1Ratings = parsed_ratings.Item1;
            List<double> user2Ratings = parsed_ratings.Item2;
            int n = user1Ratings.Count;
            double distance = 0.0;

            // Calculate the values using sigma
            for (int i = 0; i < n; i++)
            {
                distance += Math.Pow(user1Ratings[i] - user2Ratings[i], 2);
            }

            distance = Math.Sqrt(distance);

            return 1 / (1 + distance);
        }
    }
}