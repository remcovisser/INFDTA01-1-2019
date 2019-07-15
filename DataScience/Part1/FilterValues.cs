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

        // Filter out the ratings which are not present for both users
        public Tuple<List<double>, List<double>> FilterMissingValues()
        {
            // Initialise and set default values
            List<double> user1ParsedRatings = new List<double>();
            List<double> user2ParsedRatings = new List<double>();

            foreach (KeyValuePair<int, double> user1Ratings in user1)
            {
                foreach (KeyValuePair<int, double> user2Ratings in user2)
                {
                    if (user1Ratings.Key == user2Ratings.Key)
                    {
                        user1ParsedRatings.Add(user1Ratings.Value);
                        user2ParsedRatings.Add(user2Ratings.Value);
                    }
                }
            }

            return new Tuple<List<double>, List<double>>(user1ParsedRatings, user2ParsedRatings);
        }

        // Transform missing values to 0
        public Tuple<List<double>, List<double>> TransformMissingToZero()
        {
            // Initialise and set default values
            Dictionary<int, double> user1RatingsTransformed, user2RatingsTransformed;
            user1RatingsTransformed = user2RatingsTransformed = new Dictionary<int, double>();

            // Loop trough all the ratings from both users to find out which user rated which product and set zero values if needed
            foreach (KeyValuePair<int, double> user1Ratings in user1)
            {
                foreach (KeyValuePair<int, double> user2Ratings in user2)
                {
                    // If user1 has rated the product but user2 did not
                    if (user1.ContainsKey(user2Ratings.Key) && !user2.ContainsKey(user2Ratings.Key))
                    {
                        user1RatingsTransformed[user2Ratings.Key] = user1Ratings.Value;
                        user2RatingsTransformed[user2Ratings.Key] = 0;
                    }
                    // If user2 has rated the product but user1 did not
                    else if (!user1.ContainsKey(user2Ratings.Key) && user2.ContainsKey(user2Ratings.Key))
                    {
                        user1RatingsTransformed[user2Ratings.Key] = 0;
                        user2RatingsTransformed[user2Ratings.Key] = user2Ratings.Value;
                    }
                    // If both users have rated the product
                    else
                    {
                        user1RatingsTransformed[user1Ratings.Key] = user1Ratings.Value;
                        user2RatingsTransformed[user2Ratings.Key] = user2Ratings.Value;
                    }
                }
            }

            return new Tuple<List<double>, List<double>>(
                user1RatingsTransformed.Values.ToList(),
                user2RatingsTransformed.Values.ToList()
            );
        }
    }
}