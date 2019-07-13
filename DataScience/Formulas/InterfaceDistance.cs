using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public interface InterfaceDistance
    {
        double CalculateDistance(Tuple<List<double>, List<double>> parsedRatings);
    }
}