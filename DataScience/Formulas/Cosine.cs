using System;
using System.Collections.Generic;
using System;

namespace DataScience.Formulas
{
    public class Cosine : InterfaceDistance
    {
        public double CalculateDistance(Tuple<List<double>, List<double>> parsed_ratings)
        {
            // Get the parsed data
            List<double> user1Ratings = parsed_ratings.Item1;
            List<double> user2Ratings = parsed_ratings.Item2;

            // Calculate the values using sigma
            double sumOfXSquared = 0.0;
            double sumOfYSquared = 0;
            double sumOfXTimesY = 0;
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