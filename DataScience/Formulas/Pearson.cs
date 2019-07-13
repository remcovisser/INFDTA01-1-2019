using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public class Pearson : InterfaceDistance
    {
        public double CalculateDistnace(Tuple<List<double>, List<double>> parsed_ratings)
        {
            List<double> user1_ratings = parsed_ratings.Item1;
            List<double> user2_ratings = parsed_ratings.Item2; 
            int n = user1_ratings.Count;

            // Set default values
            double sum_of_x = 0;
            double sum_of_y = 0;
            double sum_of_x_squared = 0;
            double sum_of_y_squared = 0;
            double sum_of_x_times_y = 0;

            // Calculate the values using sigma
            for (int i = 0; i < n; i++) 
            {
                sum_of_x += user1_ratings[i];
                sum_of_y += user2_ratings[i];
                sum_of_x_squared += Math.Pow(user1_ratings[i], 2);
                sum_of_y_squared += Math.Pow(user2_ratings[i], 2);
                sum_of_x_times_y += user1_ratings[i] * user2_ratings[i];
            }
          
            //  Calculate the rest of the values using the values from the loop
            double avarage_of_sum_of_y_squared = (sum_of_y * sum_of_y) / n;
            double avarage_of_sum_of_x_squared = (sum_of_x * sum_of_x) / n;
            double avarage_of_sum_of_x_times_sum_of_y = (sum_of_x * sum_of_y) / n;
                    
            // Calculate the final values
            double a = sum_of_x_times_y - avarage_of_sum_of_x_times_sum_of_y;
            double b = Math.Sqrt(sum_of_x_squared - avarage_of_sum_of_x_squared);
            double c = Math.Sqrt(sum_of_y_squared - avarage_of_sum_of_y_squared);

            // Calculate the result
            double result = a / (b * c);
            return result;
        }
    }
}