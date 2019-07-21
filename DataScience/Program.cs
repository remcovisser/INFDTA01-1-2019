using System;
using System.Collections.Generic;
using System.Linq;
using DataScience.Formulas;
using DataScience.Part1;
using DataScience.Part2;
using DataScience.Data;

namespace DataScience
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            const int USER_ID = 7;

            const string dataPath = "../../DataScience/DataScience/Data/";
            const string userItemData = "userItem.data";
            const string movieLensData = "ratings.csv";
            const string itemItemData = "itemItem.data";
            const string itemItemData2 = "itemItem2.data";

            Importer importer = new Importer();

            // Initialise formulas
            InterfaceDistance pearson = new Pearson();
            InterfaceDistance euclidean = new Euclidean();


            // --------------------- User Item---------------------
            Dictionary<int, Dictionary<int, double>> data = importer.GetContent(dataPath, userItemData);

            for (int i = 1; i < data.Count; i++)
            {
                Console.WriteLine("Pearson coefficient between users 7 and " + i + ": " + new Coefficient(pearson, data[USER_ID], data[i]).DoCalculation());
            }

            Console.WriteLine("\n");
            for (int i = 1; i < data.Count; i++)
            {
                Console.WriteLine("Euclidean coefficient between users 7 and " + i + ": " + new Coefficient(euclidean, data[USER_ID], data[i]).DoCalculation());
            }

            // Predicted rating 
            new PredictingRatings(pearson, data, USER_ID).DoCalculation().PrintResult();

            // Nearest neighbour
            new NearestNeighbours(pearson, data, USER_ID, 5, 0.35).DoCalculation().PrintResult();

            // Nearest neighbour Movie lens
            Dictionary<int, Dictionary<int, double>> dataMovieLens = importer.GetMovielensData(dataPath, movieLensData);
            new NearestNeighbours(pearson, dataMovieLens, 186, 50, 0.35).DoCalculation().PrintResult();


            // --------------------- Item Item---------------------
            data = importer.GetContent(dataPath, itemItemData);
            var itemItem = new ItemItem(data);

            // Average
            itemItem.PrintResultAverage();

            // Similarity
            itemItem.PrintResultSimilarity();

            // Predicted rating ItemItem
            itemItem.PrintResultPredictedRating();

            // OneSlope
            data = importer.GetContent(dataPath, itemItemData2, true);
            itemItem = new ItemItem(data);

            // Predicted rating OneSlope
            itemItem.PrintResultPredictedOneSlope();

            // Predicted rating Movie lens data set
            itemItem = new ItemItem(dataMovieLens);
            itemItem.PrintResultPredictedOneSlopeMovieLens(1, 31);
        }
    }
}