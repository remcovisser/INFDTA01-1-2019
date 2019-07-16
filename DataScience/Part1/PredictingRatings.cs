using System.Collections.Generic;
using System.Linq;
using DataScience.Formulas;

namespace DataScience.Part1
{
    public class PredictingRatings
    {
        Dictionary<int, Dictionary<int, double>> data;
        int product_id;
        int user_id;
        Dictionary<int, double> similarities;
        InterfaceDistance distance;

        public PredictingRatings(InterfaceDistance distance, Dictionary<int, Dictionary<int, double>> data, int productId, int userId)
        {
            this.data = data;
            product_id = productId;
            user_id = userId;
            this.distance = distance;
        }

        // Filter out the information about the rating and product for an user if the user has not rated the current product
        private Dictionary<double, double> FilterOutRatingsIfUserDidNotRateProduct()
        {
            Dictionary<double, double> parsedRatings = new Dictionary<double, double>();
            foreach (KeyValuePair<int, double> similarity in similarities)
            {
                foreach (KeyValuePair<int, Dictionary<int, double>> rating in data)
                {
                    if (rating.Key == similarity.Key && data.ContainsKey(rating.Key) && data[rating.Key].ContainsKey(product_id))
                    {
                        parsedRatings.Add(similarity.Value, data[rating.Key][product_id]);
                    }
                }
            }

            return parsedRatings;
        }

        public double DoCalculation()
        {
            similarities = new NearestNeighbours(distance, data, user_id).DoCalculation().GetResult();
            Dictionary<double, double> ratingsParsed = FilterOutRatingsIfUserDidNotRateProduct();
            double sumOfSimilarity = ratingsParsed.Keys.Sum();

            List<double> influences = new List<double>();
            foreach (KeyValuePair<double, double> ratingParsed in ratingsParsed)
            {
                influences.Add(ratingParsed.Key / sumOfSimilarity);
            }

            List<double> weighted = new List<double>();
            int i = 0;
            foreach (KeyValuePair<double, double> ratingParsed in ratingsParsed)
            {
                weighted.Add(ratingParsed.Value * influences[i]);
                i++;
            }

            return weighted.Sum();
        }
    }
}