using System;
using System.Collections.Generic;
using System.Linq;
using DataScience.Formulas;

namespace DataScience.Part1
{
    public class NearestNeighbours
    {
        Dictionary<int, Dictionary<int, double>> data;
        InterfaceDistance distance;
        int userId;
        int amount;
        Dictionary<int, double> result;
        double treshhold;

//         Set the user properties
        public NearestNeighbours(InterfaceDistance _distance, Dictionary<int, Dictionary<int, double>> _data,
            int _userId,
            int _amount = 3,
            double _treshhold = 0.35)
        {
            distance = _distance;
            data = _data;
            userId = _userId;
            amount = _amount;
            treshhold = _treshhold;
        }

        public NearestNeighbours DoCalculation()
        {
            Dictionary<int, double> similarities = new Dictionary<int, double>();
            for (int i = 1; i < data.Count; i++)
            {
                if (i != userId)
                {
                    similarities.Add(i, new Coefficient(distance, data[userId], data[i]).DoCalculation());
                }
            }

            result = GetNearestNeighbours(similarities);

            return this;
        }

        public Dictionary<int, double> GetResult()
        {
            return result;
        }

        public Dictionary<int, double> GetNearestNeighbours(Dictionary<int, double> similarities)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            List<int> userRatedProducts = data[userId].Keys.ToList();

            for (int i = 1; i < data.Count; i++)
            {
                List<int> target_reated_products = data[i].Keys.ToList();
                if (similarities.ContainsKey(i) && similarities[i] > treshhold &&
                    !userRatedProducts.SequenceEqual(target_reated_products))
                {
                    result[i] = similarities[i];
                }
            }

            return result.OrderByDescending(x => x.Value).Take(amount)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void PrintResult()
        {
            int i = 1;
            foreach (KeyValuePair<int, double> rating in result)
            {
                Console.WriteLine("Nearest neighbour " + i + ": " + rating.Key + " with similarity " + rating.Value);
                i++;
            }
        }
    }
}