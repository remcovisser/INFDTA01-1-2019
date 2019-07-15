using System;
using System.Collections.Generic;
using DataScience.Formulas;
using DataScience.Part1;
using DataScience.Part2;

namespace DataScience
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // --------------------- Part 1 ---------------------
            Importer importer = new Importer();

            // Set formulas
            InterfaceDistance pearson = new Pearson();
            InterfaceDistance euclidean = new Euclidean();
            InterfaceDistance cosine = new Cosine();


            // Coefficient
            Dictionary<int, Dictionary<int, double>> data = importer.GetContent("../../DataScience/DataScience/Data/userItem.data");
            for (int i = 1; i < data.Count; i++)
            {
                Console.WriteLine("Pearson coefficient between users 7 and " + i + ": " + new Coefficient(pearson, data[7], data[i]).DoCalculation());
            }

            Console.WriteLine("\n");
            for (int i = 1; i < data.Count; i++)
            {
                Console.WriteLine("Euclidean coefficient between users 7 and " + i + ": " + new Coefficient(euclidean, data[7], data[i]).DoCalculation());
            }

            Console.WriteLine("\n");
            for (int i = 1; i < data.Count; i++)
            {
                Console.WriteLine("Cosine coefficient between users 7 and " + i + ": " + new Coefficient(cosine, data[7], data[i]).DoCalculation());
            }


            // Predicted rating
            Console.WriteLine("\n");
            Console.WriteLine("Predict the rating that user 7 would give to items 106 = " + new PredictingRatings(pearson, data, 106, 7).DoCalculation());
            Console.WriteLine("Predict the rating that user 7 would give to items 101 = " + new PredictingRatings(pearson, data, 101, 7).DoCalculation());
            Console.WriteLine("Predict the rating that user 7 would give to items 103 = " + new PredictingRatings(pearson, data, 103, 7).DoCalculation());


            Console.WriteLine("\n");
            new NearestNeighbours(pearson, data, 7, 5, 0.35).DoCalculation().PrintResult();

            // Nearest neighbour
            Dictionary<int, Dictionary<int, double>> dataMovieLens = importer.GetMovielensData("../../DataScience/DataScience/Data/ratings.csv");
            Console.WriteLine("\n");
            new NearestNeighbours(pearson, dataMovieLens, 186, 50, 0.35).DoCalculation().PrintResult();


            // --------------------- Part 2 ---------------------
            data = importer.GetContent("../../DataScience/DataScience/Data/itemItem.data");
            var itemItem = new ItemItem(data);


            // Average
            Console.WriteLine("\n");
            Console.WriteLine("User 3 has an average rating of " + itemItem.UserAverage(3));
            Console.WriteLine("User 3 has an average rating of " + itemItem.UserAverage(4));


            // Similarity
            Console.WriteLine("\n");
            Console.WriteLine("Similarity between item 103 and 104 = " + itemItem.Similarity(103, 104).Item1 + " (" + itemItem.Similarity(103, 104).Item2 + ")");
            Console.WriteLine("Similarity between item 103 and 106 = " + itemItem.Similarity(103, 106).Item1 + " (" + itemItem.Similarity(103, 106).Item2 + ")");
            Console.WriteLine("Similarity between item 103 and 107 = " + itemItem.Similarity(103, 107).Item1 + " (" + itemItem.Similarity(103, 107).Item2 + ")");
            Console.WriteLine("Similarity between item 103 and 109 = " + itemItem.Similarity(103, 109).Item1 + " (" + itemItem.Similarity(103, 109).Item2 + ")");

            Console.WriteLine("Similarity between item 104 and 106 = " + itemItem.Similarity(104, 106).Item1 + " (" + itemItem.Similarity(104, 106).Item2 + ")");
            Console.WriteLine("Similarity between item 104 and 107 = " + itemItem.Similarity(104, 107).Item1 + " (" + itemItem.Similarity(104, 107).Item2 + ")");
            Console.WriteLine("Similarity between item 104 and 109 = " + itemItem.Similarity(104, 109).Item1 + " (" + itemItem.Similarity(104, 109).Item2 + ")");
            
            Console.WriteLine("Similarity between item 106 and 107 = " + itemItem.Similarity(106, 107).Item1 + " (" + itemItem.Similarity(106, 107).Item2 + ")");
            Console.WriteLine("Similarity between item 106 and 109 = " + itemItem.Similarity(106, 109).Item1 + " (" + itemItem.Similarity(106, 109).Item2 + ")");
            
            Console.WriteLine("Similarity between item 107 and 109 = " + itemItem.Similarity(107, 109).Item1 + " (" + itemItem.Similarity(107, 109).Item2 + ")");


            // Predicted rating ItemItem
            Console.WriteLine("\n");
            Console.WriteLine("Predicted rating for user 1 for product 103 = " + itemItem.PredictRating(1, 103));


            // Deviation
            data = importer.GetContent("../../DataScience/DataScience/Data/itemItem2.data");
            itemItem = new ItemItem(data);

            Console.WriteLine("\n");
            Console.WriteLine("Deviation for 103 and 106 = " + itemItem.Deviation(103, 106).Item1);
            Console.WriteLine("Deviation for 106 and 109 = " + itemItem.Deviation(106, 109).Item1);
            Console.WriteLine("Deviation for 103 and 109 = " + itemItem.Deviation(103, 109).Item1);


            // Predicted rating OneSlope
            Console.WriteLine("\n");
            Console.WriteLine("Predicted rating with Oneslope for user 2 for product 109 = " + itemItem.PredictionOneSlope(2, 109));


            // Predicted rating Movie lens data set
            itemItem = new ItemItem(dataMovieLens);
            Console.WriteLine("\n");
            Console.WriteLine("Predicted rating with Oneslope for user 1 for product 31 = " + itemItem.PredictionOneSlope(1, 31));
        }
    }
}