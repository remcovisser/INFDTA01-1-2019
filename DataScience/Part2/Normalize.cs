namespace DataScience.Part2
{
    public class Normalize
    {
        /**
         * Class with helper functions to normalize and denormalize values
         * Default range is from 1 tot 5
         */

        public static double NormalizeRating(double rating, double min = 1, double max = 5)
        {
            return 2 * ((rating - min) / (max - min)) - 1;
        }

        public static double DeNormalizeRating(double rating, double min = 1, double max = 5)
        {
            return (rating + 1) / 2 * (max - min) + min;
        }
    }
}