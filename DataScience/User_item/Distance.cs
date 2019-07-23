using System.Collections.Generic;
using DataScience.Formulas;

namespace DataScience.User_item
{
    public class Distance
    {
        Dictionary<int, Dictionary<int, double>> data;
        InterfaceDistance distance;
        int userId;

        public Distance(InterfaceDistance _distance, Dictionary<int, Dictionary<int, double>> _data, int _userId)
        {
            data = _data;
            distance = _distance;
            userId = _userId;
        }

        public Dictionary<int, double> DoCalculation(bool print = false)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();

            for (int i = 1; i < data.Count; i++)
            {
                double calculatedDistance = new Coefficient(distance, data[userId], data[i]).DoCalculation();
                result.Add(i, calculatedDistance);
            }

            if (print)
            {
                PrintResults.PrintDistances(result, userId);
            }

            return result;
        }
    }
}