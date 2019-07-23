using System.Collections.Generic;
using System.Linq;
using DataScience.Formulas;

namespace DataScience.User_item
{
    public class NearestNeighbours
    {
        Dictionary<int, Dictionary<int, double>> data;
        InterfaceDistance distance;
        int userId;
        int amount;
        double threshold;

        // Set the user properties
        public NearestNeighbours(InterfaceDistance _distance, Dictionary<int, Dictionary<int, double>> _data,
            int _userId,
            int _amount = 3,
            double _threshold = 0.35)
        {
            distance = _distance;
            data = _data;
            userId = _userId;
            amount = _amount;
            threshold = _threshold;
        }

        public Dictionary<int, double> DoCalculation(bool print = false)
        {
            Dictionary<int, double> similarities = new Dictionary<int, double>();

            for (int i = 1; i < data.Count; i++)
            {
                // No need to calculate the similarity between the user and itself, it will always be 1
                if (i == userId)
                {
                    continue;
                }

                similarities.Add(i, new Coefficient(distance, data[userId], data[i]).DoCalculation());
            }

            return GetNearestNeighbours(similarities, print);
        }

        public Dictionary<int, double> GetNearestNeighbours(Dictionary<int, double> similarities, bool print)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            List<int> userRatedProducts = data[userId].Keys.ToList();

            for (int i = 1; i < data.Count; i++)
            {
                // Get tall the rated prdocuts from the current user
                List<int> targetReatedProducts = data[i].Keys.ToList();

                // Check if the product if present in the similarities
                // and if it is above the given threshold
                // and if the user has rated another product then the target user
                if (similarities.ContainsKey(i) && similarities[i] > threshold && !userRatedProducts.SequenceEqual(targetReatedProducts))
                {
                    result[i] = similarities[i];
                }
            }

            // Get the results, order from high to low, take the given amount, build correct data structure
            result = result.OrderByDescending(x => x.Value).Take(amount).ToDictionary(pair => pair.Key, pair => pair.Value);

            if (print)
            {
                PrintResults.PrintNearestNeighbours(result);
            }

            return result;
        }
    }
}