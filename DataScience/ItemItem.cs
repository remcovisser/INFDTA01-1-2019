using System;
using System.Collections.Generic;
using System.IO;

namespace INFDTA021
{
    public class ItemItem
    {
        private Dictionary<int, Dictionary<int, double>> data;

        public ItemItem(Dictionary<int, Dictionary<int, double>> data)
        {
            this.data = data;
        }

        public double UserAvarage(int userId)
        {
            Dictionary<int, double> userData = data[userId];
            double avarage = 0;
            int i = 0;
            foreach (KeyValuePair<int, double> rating in userData)
            {
                avarage += rating.Value;
                i += 1;
            }

            double result = avarage / i;

            return result;
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

                if (ratingItem1 != -1.0 && ratingItem2 != -1.0)
                {
                    ratingsParsed.Add(ratings.Key, new List<double> {ratingItem1, ratingItem2});
                    ratingsProduct1.Add(ratings.Key, ratingItem1);
                    ratingsProduct2.Add(ratings.Key, ratingItem2);
                }
            }

            double numerator = 0;
            double denominator = 0;
            foreach (KeyValuePair<int, List<double>> parsedRating in ratingsParsed)
            {
                double avarage = UserAvarage(parsedRating.Key);
                var temp = 1.0;
                foreach (var rating in parsedRating.Value)
                {
                    temp *= rating - avarage;
                }

                numerator += temp;
            }

            double denominatorLeft = 0;
            foreach (KeyValuePair<int, double> rating in ratingsProduct1)
            {
                double avarage = UserAvarage(rating.Key);
                denominatorLeft += Math.Pow(rating.Value - avarage, 2);
            }

            double denominatorRight = 0;
            foreach (KeyValuePair<int, double> rating in ratingsProduct2)
            {
                double avarage = UserAvarage(rating.Key);
                denominatorRight += Math.Pow(rating.Value - avarage, 2);
            }

            denominator = Math.Sqrt(denominatorLeft) * Math.Sqrt(denominatorRight);

            double similarity = numerator / denominator;
            int totalUsers = ratingsProduct1.Count;
            Tuple<double, int> result = new Tuple<double, int>(similarity, totalUsers);

            return result;
        }

        public double PredictRating(int userId, int productId)
        {
            Dictionary<int, double> normalisedRatings = GetNormalisedRatings(userId);
            double numerator = 0;
            double denominator = 0;

            foreach (KeyValuePair<int, double> normalisedRating in normalisedRatings)
            {
                numerator += normalisedRating.Value * Similarity(productId, normalisedRating.Key).Item1;
                denominator += Math.Abs(Similarity(productId, normalisedRating.Key).Item1);
            }

            double normalisedResult = numerator / denominator;
            double result = DeNormalizeRating(normalisedResult);

            return result;
        }

        private Dictionary<int, double> GetNormalisedRatings(int userId)
        {
            Dictionary<int, double> userRatings = data[userId];
            Dictionary<int, double> result = new Dictionary<int, double>();

            foreach (KeyValuePair<int, double> rating in userRatings)
            {
                result.Add(rating.Key, NormalizeRating(rating.Value));
            }

            return result;
        }

        private double NormalizeRating(double rating, double min = 1, double max = 5)
        {
            return 2 * ((rating - min) / (max - min)) - 1;
        }

        private double DeNormalizeRating(double rating, double min = 1, double max = 5)
        {
            return ((rating + 1) / 2) * (max - min) + min;
        }
    }
}