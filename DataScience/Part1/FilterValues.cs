using System;
using System.Collections.Generic;
using System.Linq;

namespace DataScience.Part1
{
    public class FilterValues
    {
        private Dictionary<int, double> user1;
        private Dictionary<int, double> user2;

        public FilterValues(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            this.user1 = user1;
            this.user2 = user2;
        }

        // Filter out the ratings which are not present for both users (Euclidean, Manhattan, Pearson)
        public Tuple<List<double>, List<double>> FilterMissingValues()
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
        public Tuple<List<double>, List<double>> TransformMissingToZero()
        {
            Dictionary<int, double> user1_ratings_transformed = new Dictionary<int, double>();
            Dictionary<int, double> user2_ratings_transformed = new Dictionary<int, double>();

            foreach (int key in user1.Keys.ToList())
            {
                if (!user2_ratings_transformed.ContainsKey(key))
                {
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

            return new Tuple<List<double>, List<double>>(user1_ratings_transformed.Values.ToList(),
                user2_ratings_transformed.Values.ToList());
        }
    }
}