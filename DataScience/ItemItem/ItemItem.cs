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
            foreach (var item in data[userId].Where(kvp => kvp.Value == 0.0).ToList())
            {
                data[userId].Remove(item.Key);
            }

            return data[userId].Values.Average();
        }

        public Tuple<double, int> Similarity(int productId1, int productId2)
        {
            Dictionary<int, List<double>> ratingsParsed = new Dictionary<int, List<double>>();
            Dictionary<int, double> ratingsProduct1 = new Dictionary<int, double>();
            Dictionary<int, double> ratingsProduct2 = new Dictionary<int, double>();

            foreach (KeyValuePair<int, Dictionary<int, double>> ratings in data)
            {
                double ratingproduct1 = -1.0;
                double ratingproduct2 = -1.0;

                foreach (KeyValuePair<int, double> rating in ratings.Value)
                {
                    if (rating.Key == productId1)
                    {
                        ratingproduct1 = rating.Value;
                    }

                    if (rating.Key == productId2)
                    {
                        ratingproduct2 = rating.Value;
                    }
                }

                if (ratingproduct1 >= 0 && ratingproduct2 >= 0)
                {
                    ratingsParsed.Add(ratings.Key, new List<double> {ratingproduct1, ratingproduct2});
                    ratingsProduct1.Add(ratings.Key, ratingproduct1);
                    ratingsProduct2.Add(ratings.Key, ratingproduct2);
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

        public Tuple<double, int> Deviation(int product1, int product2)
        {
            int count = 0;
            double deviation = 0;

            foreach (KeyValuePair<int, Dictionary<int, double>> ratings in data)
            {
                if (ratings.Value.ContainsKey(product1) && ratings.Value.ContainsKey(product2))
                {
                    deviation += ratings.Value[product1] - ratings.Value[product2];
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

        public List<int> GetUniqueItemIds()
        {
            List<int> itemIds = new List<int>();

            foreach (Dictionary<int, double> rating in data.Values)
            {
                List<int> keys = rating.Keys.ToList();
                itemIds.AddRange(keys);
            }

            itemIds = itemIds.Distinct().ToList();
            itemIds.Sort();

            return itemIds;
        }

        public void Average()
        {
            Console.WriteLine("\n");
            foreach (int userId in data.Keys)
            {
                Console.WriteLine("User " + userId + " has an average rating of " + UserAverage(userId));
            }
        }

        public void Similarities()
        {
            List<int> itemIds = GetUniqueItemIds();
            int itemIdsCount = itemIds.Count;

            Console.WriteLine("\n");
            for (var productIndex1 = 0; productIndex1 < itemIdsCount; productIndex1++)
            {
                int product1 = itemIds[productIndex1];
                for (int productIndex2 = 0; productIndex2 < itemIdsCount; productIndex2++)
                {
                    // No need to calculate the similarity between the same items, since that will always be 1
                    if (productIndex1 == productIndex2)
                    {
                        break;
                    }

                    int product2 = itemIds[productIndex2];
                    Tuple<double, int> similarity = Similarity(product1, product2);

                    Console.WriteLine("Similarity between product " + product1 + " and " + product2 + " = " + similarity.Item1 + " (" + similarity.Item2 + ")");
                }
            }
        }

        public void Predicted(Func<int, int, double> func, string extra = "")
        {
            var itemIds = GetUniqueItemIds();

            Console.WriteLine("\n");
            foreach (var (userId, ratings) in data)
            {
                foreach (int itemId in itemIds)
                {
                    if (!ratings.Keys.Contains(itemId))
                    {
                        Console.WriteLine("Predicted rating " + extra + "for user " + userId + " for product " + itemId + " = " + func(userId, itemId));
                    }
                }
            }
        }

        public void PredictedOneSlope()
        {
            Predicted(PredictionOneSlope, "with Oneslope ");
        }

        public void PredictedRatings()
        {
            Predicted(PredictRating);
        }

        public void PredictedOneSlopeMovieLens(int userId, int productId)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Predicted rating with Oneslope for user 1 for product 31 = " + PredictionOneSlope(userId, productId));
        }
    }
}