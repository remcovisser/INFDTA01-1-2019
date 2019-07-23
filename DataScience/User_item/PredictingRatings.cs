using System.Collections.Generic;
using System.Linq;
using DataScience.Formulas;

namespace DataScience.User_item
{
    public class PredictingRatings
    {
        Dictionary<int, Dictionary<int, double>> data;
        Dictionary<int, double> similarities;
        InterfaceDistance distance;
        int userId;

        public PredictingRatings(InterfaceDistance distance, Dictionary<int, Dictionary<int, double>> data, int userId)
        {
            this.data = data;
            this.distance = distance;
            this.userId = userId;
        }

        private Dictionary<double, double> FilterOutRatingsIfUserDidNotRateProduct(int product_id)
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

        public Dictionary<int, double> DoCalculation(bool print = false)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            var userRatings = data[userId];

            foreach (KeyValuePair<int, double> productRating in userRatings)
            {
                var productId = productRating.Key;

                if (productRating.Value != 0)
                {
                    continue;
                }

                similarities = new NearestNeighbours(distance, data, userId).DoCalculation();
                Dictionary<double, double> ratingsParsed = FilterOutRatingsIfUserDidNotRateProduct(productId);

                var sumOfSimilarityTimesRating = 0.0;
                var sumOfRatings = 0.0;
                foreach (var rating in ratingsParsed)
                {
                    sumOfSimilarityTimesRating += rating.Key * rating.Value;
                    if (rating.Value > 0)
                    {
                        sumOfRatings += rating.Key;
                    }
                }

                var predictedRating = sumOfSimilarityTimesRating / sumOfRatings;

                result.Add(productId, predictedRating);
            }

            result = result.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            if (print)
            {
                PrintResults.PrintPredictedRatings(result, userId);
            }

            return result;
        }
    }
}