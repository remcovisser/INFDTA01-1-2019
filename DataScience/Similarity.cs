using System;
using System.Collections.Generic;
using System.Linq;

namespace INFDTA021
{
    public class Similarity
    {
        private Dictionary<int, double> user1;
        private Dictionary<int, double> user2;

        // Set the user properties
        public Similarity(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            this.user1 = user1;
            this.user2 = user2;
        }


        // Filter out the ratings which are not present for both users (Euclidean, Manhattan, Pearson)
        private Tuple<List<double>, List<double>> FilterMissingValues()
        {
            List<double> user1_parsed_ratings = new List<double>();
            List<double> user2_parsed_ratings = new List<double>();

            foreach (KeyValuePair<int, double> user1_ratings in user1)
            {
                foreach (KeyValuePair<int, double> user2_ratings in user2)
                {
                    if (user1_ratings.Key == user2_ratings.Key)
                    {
                        user1_parsed_ratings.Add(user1_ratings.Value);
                        user2_parsed_ratings.Add(user2_ratings.Value);
                    }
                }
            }
          
            return new Tuple<List<double>, List<double>>(user1_parsed_ratings, user2_parsed_ratings);
        }


        // Transform missing values to 0 (Cosine)
        private Tuple<List<double>, List<double>> TransformMissingToZero()
        {
            Dictionary<int, double> user1_ratings_transformed = new Dictionary<int, double>();
            Dictionary<int, double> user2_ratings_transformed = new Dictionary<int, double>();
           
            foreach(int key in user1.Keys.ToList()) 
            {
                if (!user2_ratings_transformed.ContainsKey(key)) {
                    user2_ratings_transformed.Add(key, user1[key]);
                }
                if (!user1_ratings_transformed.ContainsKey(key))
                {
                    if (user2.ContainsKey(key))
                    {
                        user1_ratings_transformed.Add(key, user2[key]);
                    }
                    else
                    {
                        user1_ratings_transformed.Add(key, 0);
                    }
                }
            }

            foreach (int key in user2.Keys.ToList())
            {
                if (!user1_ratings_transformed.ContainsKey(key))
                {
                    user1_ratings_transformed.Add(key, user2[key]);
                }

                if (!user2_ratings_transformed.ContainsKey(key))
                {
                    if (user1.ContainsKey(key))
                    {
                        user2_ratings_transformed.Add(key, user2[key]);
                    }
                    else
                    {
                        user2_ratings_transformed.Add(key, 0);
                    }
                }
            }
           
            return new Tuple<List<double>, List<double>>(user1_ratings_transformed.Values.ToList(), user2_ratings_transformed.Values.ToList());
        }


        // Transform distance to similarity
        private double DistanceToSimilarity(double distance)
        {
            return 1 / (1 + distance);
        }
      

        // Calculate the Pearson coefficient
        public double Pearson()
        {
            // Get the parsed data
            Tuple<List<double>, List<double>> parsed_ratings = this.FilterMissingValues();
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


        // Calculate the Euclidean 
        public double Euclidean()
        {
            // Get the parsed data
            Tuple<List<double>, List<double>> parsed_ratings = this.FilterMissingValues();
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
            result = this.DistanceToSimilarity(distance);
            return result;
        }


        // Calculate the Cosine
        public double Cosine()
        {
            // Get the parsed data
            Tuple<List<double>, List<double>> parsed_ratings = this.TransformMissingToZero();
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
