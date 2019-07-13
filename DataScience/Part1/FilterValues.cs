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
            List<double> user1ParsedRatings = new List<double>();
            List<double> user2ParsedRatings = new List<double>();

            foreach (KeyValuePair<int, double> user1_ratings in user1)
            {
                foreach (KeyValuePair<int, double> user2_ratings in user2)
                {
                    if (user1_ratings.Key == user2_ratings.Key)
                    {
                        user1ParsedRatings.Add(user1_ratings.Value);
                        user2ParsedRatings.Add(user2_ratings.Value);
                    }
                }
            }

            return new Tuple<List<double>, List<double>>(user1ParsedRatings, user2ParsedRatings);
        }

        // Transform missing values to 0 (Cosine)
        public Tuple<List<double>, List<double>> TransformMissingToZero()
        {
            Dictionary<int, double> user1RatingsTransformed = new Dictionary<int, double>();
            Dictionary<int, double> user2RatingsTransformed = new Dictionary<int, double>();

            foreach (int key in user1.Keys.ToList())
            {
                if (!user2RatingsTransformed.ContainsKey(key))
                {
                    user2RatingsTransformed.Add(key, user1[key]);
                }

                if (!user1RatingsTransformed.ContainsKey(key))
                {
                    if (user2.ContainsKey(key))
                    {
                        user1RatingsTransformed.Add(key, user2[key]);
                    }
                    else
                    {
                        user1RatingsTransformed.Add(key, 0);
                    }
                }
            }

            foreach (int key in user2.Keys.ToList())
            {
                if (!user1RatingsTransformed.ContainsKey(key))
                {
                    user1RatingsTransformed.Add(key, user2[key]);
                }

                if (!user2RatingsTransformed.ContainsKey(key))
                {
                    if (user1.ContainsKey(key))
                    {
                        user2RatingsTransformed.Add(key, user2[key]);
                    }
                    else
                    {
                        user2RatingsTransformed.Add(key, 0);
                    }
                }
            }

            return new Tuple<List<double>, List<double>>(user1RatingsTransformed.Values.ToList(),
                user2RatingsTransformed.Values.ToList());
        }
    }
}