using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Euclidean : InterfaceDistance
    {
        public double CalculateDistnace(Tuple<List<double>, List<double>> parsed_ratings)
        {
            List<double> user1_ratings = parsed_ratings.Item1;
            List<double> user2_ratings = parsed_ratings.Item2;
            int n = user1_ratings.Count;
            double distance = 0.0;
            double result = 0.0;

            // Calculate the values using sigma
            for (int i = 0; i < n; i++) 
            {
                distance += Math.Pow(user1_ratings[i] - user2_ratings[i], 2);
            }
            distance = Math.Sqrt(distance);
            result =  1 / (1 + distance);
            return result;
        }
    }
}