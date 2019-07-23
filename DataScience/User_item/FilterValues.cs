using System;
using System.Collections.Generic;
using System.Linq;

namespace DataScience.User_item
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
            Dictionary<int, double> user1RatingsTransformed = new Dictionary<int, double>();
            Dictionary<int, double> user2RatingsTransformed = new Dictionary<int, double>();
            var value = 0.0;

            foreach (int key1 in user1.Keys.ToList())
            {
                foreach (int key2 in user2.Keys.ToList())
                {
                    if (!user1RatingsTransformed.ContainsKey(key2))
                    {
                        user1RatingsTransformed.Add(key2, user1.TryGetValue(key2, out value) ? user1[key2] : 0);
                    }

                    if (!user2RatingsTransformed.ContainsKey(key2))
                    {
                        user2RatingsTransformed.Add(key2, user2.TryGetValue(key2, out value) ? user2[key2] : 0);
                    }
                }
            }

            return new Tuple<List<double>, List<double>>(user1RatingsTransformed.Values.ToList(), user2RatingsTransformed.Values.ToList());
        }
    }
}