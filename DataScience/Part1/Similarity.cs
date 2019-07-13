using System;
using System.Collections.Generic;
using DataScience.Formulas;

namespace DataScience.Part1
{
    public class Similarity
    {
        private InterfaceDistance distance;
        Tuple<List<double>, List<double>> parsed_ratings;

        public Similarity(InterfaceDistance distance, Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            this.distance = distance;
            FilterValues filterValues = new FilterValues(user1, user2);

            if (distance is Cosine)
            {
                parsed_ratings = filterValues.TransformMissingToZero();
            }
            else
            {
                parsed_ratings = filterValues.FilterMissingValues();
            }
        }

        public double DoCalculation()
        {
            return distance.CalculateDistnace(parsed_ratings);
        }
    }
}