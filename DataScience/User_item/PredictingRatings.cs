using System;
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

        private Dictionary<int, Tuple<double, double>> FilterOutRatingsIfUserDidNotRateProduct(int product_id)
        {
            Dictionary<int, Tuple<double, double>> parsedRatings = new Dictionary<int, Tuple<double, double>>();
            int key = 0;
            foreach (KeyValuePair<int, double> similarity in similarities)
            {
                foreach (KeyValuePair<int, Dictionary<int, double>> rating in data)
                {
                    if (rating.Key == similarity.Key && data.ContainsKey(rating.Key) && data[rating.Key].ContainsKey(product_id))
                    {
                        parsedRatings.Add(key, new Tuple<double, double>(similarity.Value, data[rating.Key][product_id]));
                        key++;
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
                Dictionary<int, Tuple<double, double>> ratingsParsed = FilterOutRatingsIfUserDidNotRateProduct(productId);

                var sumOfSimilarityTimesRating = 0.0;
                var sumOfRatings = 0.0;
                foreach (var rating in ratingsParsed)
                {
                    sumOfSimilarityTimesRating += rating.Value.Item1 * rating.Value.Item2;
                    if (rating.Value.Item2 > 0)
                    {
                        sumOfRatings += rating.Value.Item1;
                    }
                }

                var predictedRating = sumOfSimilarityTimesRating / sumOfRatings;

                result.Add(productId, predictedRating);
            }

            result = result.OrderByDescending(x => x.Value).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);

            if (print)
            {
                PrintResults.PrintPredictedRatings(result, userId);
            }

            return result;
        }
    }
}