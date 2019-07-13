using System;
using System.Collections.Generic;

namespace DataScience.Formulas
{
    public interface InterfaceDistance
    {
        double CalculateDistnace(Tuple<List<double>, List<double>> parsed_ratings);
    }
}