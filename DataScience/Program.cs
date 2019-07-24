using System;
using System.Collections.Generic;
using DataScience.Formulas;
using DataScience.User_item;
using DataScience.Item_item;
using DataScience.Data;

namespace DataScience
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            const bool PRINT = true;
            const int USER_ID = 7;
            const string dataPath = "../../DataScience/DataScience/Data/";
            const string userItemData = "userItem.data";
            const string movieLensData = "ratings.csv";
            const string movieLensDataSmall = "ratingsSmall.csv";
            const string itemItemData = "itemItem.data";
            const string itemItemData2 = "itemItem2.data";

            Importer importer = new Importer();

            // Initialise formulas
            InterfaceDistance pearson = new Pearson();
            InterfaceDistance euclidean = new Euclidean();


            // --------------------- User Item---------------------
            Dictionary<int, Dictionary<int, double>> data = importer.GetContent(dataPath, userItemData);

            // Distance
//            new Distance(pearson, data, USER_ID).DoCalculation(PRINT);
//            new Distance(euclidean, data, USER_ID).DoCalculation(PRINT);

//             Predicted rating 
            new PredictingRatings(pearson, data, USER_ID).DoCalculation(PRINT);
         
            // todo: 664 5
            Console.WriteLine("start");
            Dictionary<int, Dictionary<int, double>> dataMovieLens = importer.GetMovielensData(dataPath, movieLensData);
//            Dictionary<int, Dictionary<int, double>> dataMovieLensSmall = importer.GetMovielensData(dataPath, movieLensDataSmall);
            new PredictingRatings(pearson, dataMovieLens, 20).DoCalculation(PRINT);
            Console.WriteLine("end");
            
//            // Nearest neighbour
//            new NearestNeighbours(pearson, data, USER_ID, 5, 0.35).DoCalculation(PRINT);
//
//            // Nearest neighbour Movie lens
//           
//            new NearestNeighbours(pearson, dataMovieLens, 20, 50, 0.35).DoCalculation(PRINT);
//            
//
//
//            // --------------------- Item Item---------------------
//            data = importer.GetContent(dataPath, itemItemData);
//            var itemItem = new ItemItem(data);
//
//            // Average
//            itemItem.Average(PRINT);
////            Alternatieve manier:
////            var averages = itemItem.Average();
////            PrintResults.PrintAverages(averages);
//
//            // Similarity
//            itemItem.Similarities(PRINT);
//
//            // Predicted rating ItemItem
//            itemItem.PredictedRatings(PRINT);
//
//            // OneSlope
//            data = importer.GetContent(dataPath, itemItemData2, true);
//            itemItem = new ItemItem(data);
//
//            // Predicted rating OneSlope
//            itemItem.PredictedOneSlope(PRINT);
//
//            // Predicted rating Movie lens data set
//            itemItem = new ItemItem(dataMovieLens);
//            // veel 5
//            itemItem.PredictedOneSlopeMovieLens(20, 31, PRINT);
        }
    }
}