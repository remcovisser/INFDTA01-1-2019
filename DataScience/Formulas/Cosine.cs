using System;
using System.Collections.Generic;
using System;

namespace DataScience.Formulas
{
    public class Cosine : InterfaceDistance
    {
        public double CalculateDistnace(Tuple<List<double>, List<double>> parsed_ratings)
        {
            // Get the parsed data
            List<double> user1_ratings = parsed_ratings.Item1;
            List<double> user2_ratings = parsed_ratings.Item2;
            int n = user1_ratings.Count;
            double result = 0.0;

            // Calculate the values using sigma
            double sum_of_x_squared = 0;
            double sum_of_y_squared = 0;
            double sum_of_x_times_y = 0;
            for (int i = 0; i < n; i++)
            {
                sum_of_x_times_y += user1_ratings[i] * user2_ratings[i];
                sum_of_x_squared += Math.Pow(user1_ratings[i], 2);
                sum_of_y_squared += Math.Pow(user2_ratings[i], 2);
            }

            result = sum_of_x_times_y / (Math.Sqrt(sum_of_x_squared) * Math.Sqrt(sum_of_y_squared));

            return result;
        }
    }
}