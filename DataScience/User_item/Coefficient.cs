using System;
using System.Collections.Generic;
using DataScience.Formulas;

namespace DataScience.User_item
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
            parsed_ratings = distance is Cosine ? filterValues.TransformMissingToZero() : filterValues.FilterMissingValues();
        }

        public double DoCalculation()
        {
            return distance.CalculateDistance(parsed_ratings);
        }
    }
}