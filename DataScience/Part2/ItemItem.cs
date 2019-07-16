using System;
using System.Collections.Generic;
using System.Linq;

namespace DataScience.Part2
{
    public class ItemItem
    {
        Dictionary<int, Dictionary<int, double>> data;

        public ItemItem(Dictionary<int, Dictionary<int, double>> data)
        {
            this.data = data;
        }

        // Calculate the average rating the user has given
        public double UserAverage(int userId)
        {
            return data[userId].Values.Average();
        }

        public Tuple<double, int> Similarity(int productId1, int productId2)
        {
            Dictionary<int, List<double>> ratingsParsed = new Dictionary<int, List<double>>();
            Dictionary<int, double> ratingsProduct1 = new Dictionary<int, double>();
            Dictionary<int, double> ratingsProduct2 = new Dictionary<int, double>();

            foreach (KeyValuePair<int, Dictionary<int, double>> ratings in data)
            {
                double ratingItem1 = -1.0;
                double ratingItem2 = -1.0;

                foreach (KeyValuePair<int, double> rating in ratings.Value)
                {
                    if (rating.Key == productId1)
                    {
                        ratingItem1 = rating.Value;
                    }

                    if (rating.Key == productId2)
                    {
                        ratingItem2 = rating.Value;
                    }
                }

                if (ratingItem1 >= 0 && ratingItem2 >= 0)
                {
                    ratingsParsed.Add(ratings.Key, new List<double> {ratingItem1, ratingItem2});
                    ratingsProduct1.Add(ratings.Key, ratingItem1);
                    ratingsProduct2.Add(ratings.Key, ratingItem2);
                }
            }

            double numerator, denominator;
            numerator = 0;

            foreach (KeyValuePair<int, List<double>> parsedRating in ratingsParsed)
            {
                double average = UserAverage(parsedRating.Key);
                var temp = 1.0;
                foreach (var rating in parsedRating.Value)
                {
                    temp *= rating - average;
                }

                numerator += temp;
            }

            double denominatorLeft = 0;
            foreach (KeyValuePair<int, double> rating in ratingsProduct1)
            {
                double average = UserAverage(rating.Key);
                denominatorLeft += Math.Pow(rating.Value - average, 2);
            }

            double denominatorRight = 0;
            foreach (KeyValuePair<int, double> rating in ratingsProduct2)
            {
                double average = UserAverage(rating.Key);
                denominatorRight += Math.Pow(rating.Value - average, 2);
            }

            denominator = Math.Sqrt(denominatorLeft) * Math.Sqrt(denominatorRight);
            double similarity = numerator / denominator;

            return new Tuple<double, int>(similarity, ratingsProduct1.Count);
        }

        public double PredictRating(int userId, int productId)
        {
            Dictionary<int, double> normalisedRatings = GetNormalisedRatings(userId);
            double numerator, denominator;
            numerator = denominator = 0;

            foreach (KeyValuePair<int, double> normalisedRating in normalisedRatings)
            {
                numerator += normalisedRating.Value * Similarity(productId, normalisedRating.Key).Item1;
                denominator += Math.Abs(Similarity(productId, normalisedRating.Key).Item1);
            }

            return Normalize.DeNormalizeRating(numerator / denominator);
        }

        private Dictionary<int, double> GetNormalisedRatings(int userId)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();

            foreach (KeyValuePair<int, double> rating in data[userId])
            {
                result.Add(rating.Key, Normalize.NormalizeRating(rating.Value));
            }

            return result;
        }

        public Tuple<double, int> Deviation(int item1, int item2)
        {
            int count = 0;
            double deviation = 0;

            foreach (KeyValuePair<int, Dictionary<int, double>> ratings in data)
            {
                if (ratings.Value.ContainsKey(item1) && ratings.Value.ContainsKey(item2))
                {
                    deviation += ratings.Value[item1] - ratings.Value[item2];
                    count++;
                }
            }

            return new Tuple<double, int>(deviation / count, count);
        }

        public double PredictionOneSlope(int user, int item)
        {
            double prediction, bot;
            prediction = bot = 0;

            foreach (var ratings in data[user])
            {
                Tuple<double, int> deviation = Deviation(item, ratings.Key);
                prediction += (ratings.Value + deviation.Item1) * deviation.Item2;
                bot += deviation.Item2;
            }

            return prediction / bot;
        }
    }
}