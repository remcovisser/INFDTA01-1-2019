using System;
using System.Collections.Generic;

namespace DataScience
{
    public static class PrintResults
    {
        public static void PrintDistances(Dictionary<int, double> results, int userId)
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<int, double> result in results)
            {
                Console.WriteLine("Pearson coefficient between users " + userId + " and " + result.Key + " and " + result.Value + ": ");
            }
        }

        public static void PrintPredictedRatings(Dictionary<int, double> result, int userId)
        {
            Console.WriteLine("\n");
            foreach (var predictedRating in result)
            {
                Console.WriteLine("Predict the rating that user " + userId + " would give to items " + predictedRating.Key + " = " + predictedRating.Value);
            }
        }

        public static void PrintNearestNeighbours(Dictionary<int, double> result)
        {
            int index = 0;
            Console.WriteLine("\n");
            foreach (KeyValuePair<int, double> rating in result)
            {
                index++;
                Console.WriteLine("Neighbour " + index + " with product id = " + rating.Key + " has a similarity of " + rating.Value);
            }
        }

        public static void PrintAverages(Dictionary<int, double> results)
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<int, double> result in results)
            {
                Console.WriteLine("User " + result.Key + " has an average rating of " + result.Value);
            }
        }

        public static void PrintSimilarities(Dictionary<Tuple<int, int>, Tuple<double, int>> results)
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<Tuple<int, int>, Tuple<double, int>> result in results)
            {
                Console.WriteLine("Similarity between product " + result.Key.Item1 + " and " + result.Key.Item2 + " = " + result.Value.Item1 + " (" + result.Value.Item2 + ")");
            }
        }

        public static void PrintPredictedRatingOneSlope(Dictionary<int, Tuple<int, double>> results, string extra)
        {
            Console.WriteLine("\n");
            foreach (KeyValuePair<int, Tuple<int, double>> result in results)
            {
                Console.WriteLine("Predicted rating " + extra + "for user " + result.Key + " for product " + result.Value.Item1 + " = " + result.Value.Item2);
            }
        }

        public static void PrintPredictedRatingMoiveLens(int userId, int productId, double result)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Predicted rating with Oneslope for user " + userId + " for product " + productId + " = " + result);
        }
    }
}