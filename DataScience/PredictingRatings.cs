using System;
using System.Collections.Generic;
using System.Linq;

namespace INFDTA021
{
    public class PredictingRatings
    {
        Dictionary<int, Dictionary<int, double>> data;
        int product_id;
        int user_id;
        Dictionary<int, double> similarities;

        public PredictingRatings(Dictionary<int, Dictionary<int, double>> data, int product_id, int user_id)
        {
            this.data = data;
            this.product_id = product_id;
            this.user_id = user_id;
        }

        // Filter out the information about the rating and product for an user if the user has not rated the current product
        private Dictionary<double, double> FilterOutRatingsIfUserDidNotRateProduct()
        {
            Dictionary<double, double> parsed_ratings = new Dictionary<double, double>();
            foreach (KeyValuePair<int, double> similarity in similarities)
            {
                foreach (KeyValuePair<int, Dictionary<int, double>> rating in data)
                {
                    if (rating.Key == similarity.Key && data.ContainsKey(rating.Key) &&
                        data[rating.Key].ContainsKey(product_id))
                    {
                        // TODO: Fix this -> if rating.key is not unique (same similarity) it fails --> Transform to user_id with dictionary
                        parsed_ratings.Add(similarity.Value, data[rating.Key][product_id]);
                    }
                }
            }

            return parsed_ratings;
        }

        public double Pearson()
        {
            similarities = new NearestNeighbours(data, user_id).Pearson().GetResult();
            Dictionary<double, double> ratings_parsed = FilterOutRatingsIfUserDidNotRateProduct();
            double sum_of_similarity = ratings_parsed.Keys.Sum();

            List<double> influences = new List<double>();
            foreach (KeyValuePair<double, double> rating_parsed in ratings_parsed)
            {
                influences.Add(rating_parsed.Key / sum_of_similarity);
            }

            List<double> weighted = new List<double>();
            int i = 0;
            foreach (KeyValuePair<double, double> rating_parsed in ratings_parsed)
            {
                weighted.Add(rating_parsed.Value * influences[i]);
                i++;
            }

            double total = weighted.Sum(item => item);
            return total;
        }
    }
}