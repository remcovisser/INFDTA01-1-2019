using System;
using System.Collections.Generic;
using DataScience.Formulas;

namespace DataScience.Part1
{
    public class Coefficient
    {
        private InterfaceDistance distance;
        Tuple<List<double>, List<double>> parsed_ratings;

        public Coefficient(InterfaceDistance distance, Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            this.distance = distance;
            FilterValues filterValues = new FilterValues(user1, user2);

            // Alter the data based on the used algorithm
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
            return distance.CalculateDistance(parsed_ratings);
        }
    }
}