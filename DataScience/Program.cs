using System;
using System.Collections.Generic;
using DataScience.Formulas;
using DataScience.Part1;
using DataScience.Part2;

using System.Linq;

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
            InterfaceDistance euclidean  = new Euclidean();
            InterfaceDistance cosine = new Cosine();
            
            // Coefficient
            Dictionary<int, Dictionary<int, double>> data = importer.GetContent("/DataScience/DataScience/Data/userItem.data");
            Console.WriteLine("Pearson coefficient between users 7 and 1: " + new Coefficient(pearson, data[7], data[1]).DoCalculation());
            Console.WriteLine("Euclidean coefficient between users 7 and 1: " + new Coefficient(euclidean, data[7], data[1]).DoCalculation());
            Console.WriteLine("Cosine coefficient between users 7 and 1: " + new Coefficient(cosine, data[7], data[1]).DoCalculation());
            
            // Predicted rating
            Console.WriteLine("Predict the rating that user 7 would give to items 101 = " + new PredictingRatings(pearson, data, 106, 7).DoCalculation());
   
            // Nearest neighbour
            Dictionary<int, Dictionary<int, double>> dataMovieLens = importer.GetMovielensData("/DataScience/DataScience/Data/ratings.csv");
            new NearestNeighbours(pearson, dataMovieLens, 186, 25, 0.35).DoCalculation().PrintResult();

            
            
            // --------------------- Part 2 ---------------------
           data = importer.GetContent("/DataScience/DataScience/Data/itemItem.data");
           var itemItem = new ItemItem(data);

           // Avarage
            Console.WriteLine("User 1 avarage = " +  itemItem.UserAvarage(1));
            Console.WriteLine("User 2 avarage = " +  itemItem.UserAvarage(2));
            Console.WriteLine("User 3 avarage = " +  itemItem.UserAvarage(3));
            Console.WriteLine("User 4 avarage = " +  itemItem.UserAvarage(4));
            Console.WriteLine("User 5 avarage = " +  itemItem.UserAvarage(5));

            // Similarity
            Console.WriteLine("Similarity between item 103 and 104 = " +  itemItem.Similarity(103, 104).Item1 + "(" + itemItem.Similarity(103, 104).Item2 + ")"); 
            Console.WriteLine("Similarity between item 103 and 106 = " +  itemItem.Similarity(103, 106).Item1 + "(" + itemItem.Similarity(103, 106).Item2 + ")"); 
            Console.WriteLine("Similarity between item 106 and 109 = " +  itemItem.Similarity(106, 109).Item1 + "(" + itemItem.Similarity(106, 109).Item2 + ")"); 
            Console.WriteLine("Similarity between item 104 and 107 = " +  itemItem.Similarity(104, 107).Item1 + "(" + itemItem.Similarity(104, 107).Item2 + ")"); 
            Console.WriteLine("Similarity between item 104 and 109 = " +  itemItem.Similarity(104, 109).Item1 + "(" + itemItem.Similarity(104, 109).Item2 + ")"); 

            // Predicted rating ItemItem
            Console.WriteLine("Predicted rating for user 1 for product 103 = " + itemItem.PredictRating(1, 103));
            Console.WriteLine("Predicted rating for user 2 for product 103 = " + itemItem.PredictRating(2, 103));
            Console.WriteLine("Predicted rating for user 3 for product 106 = " + itemItem.PredictRating(3, 106));
            Console.WriteLine("Predicted rating for user 5 for product 107 = " + itemItem.PredictRating(5, 107));
    
            // Predicted rating OneSlope
            data = importer.GetContent("/DataScience/DataScience/Data/itemItem2.data");
            itemItem = new ItemItem(data);
            Console.WriteLine("Predicted rating with Oneslope for user 2 for product 109 = " + itemItem.PredictionOneSlope(2, 109));
            Console.WriteLine("Predicted rating with Oneslope for user 2 for product 109 = " + itemItem.PredictionOneSlope(3, 103));
            Console.WriteLine("Predicted rating with Oneslope for user 2 for product 109 = " + itemItem.PredictionOneSlope(4, 106));
           
            // Predicted rating Movie lens data set
            itemItem = new ItemItem(dataMovieLens);
            Console.WriteLine("Predicted rating with Oneslope for user 2 for product 110 = " + itemItem.PredictionOneSlope(2, 110));
        }
    }
}