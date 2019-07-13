namespace DataScience.Part2
{
    public class Normalize
    {
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