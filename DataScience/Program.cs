using System;
using System.Collections.Generic;

namespace INFDTA021
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // --------------------- Part 1 ---------------------
            var importer = new Importer();
//            Dictionary<int, Dictionary<int, double>> data = importer.GetContent("/DataScience/DataScience/userItem.data");
//            Dictionary<int, Dictionary<int, double>> data_movielens = importer.GetMovielensData("/DataScience/DataScience/ratings.csv");
//            
//            Console.WriteLine("Pearson coefficient between users 7 and 1: " + new Similarity(data[7], data[1]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 2: " + new Similarity(data[7], data[2]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 3: " + new Similarity(data[7], data[3]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 4: " + new Similarity(data[7], data[4]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 5: " + new Similarity(data[7], data[5]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 6: " + new Similarity(data[7], data[6]).Pearson());
//            Console.WriteLine("Pearson coefficient between users 7 and 7: " + new Similarity(data[7], data[7]).Pearson());
//   
//            Console.WriteLine("Euclidean distance similarity between users 7 and 1: " + new Similarity(data[7], data[1]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 2: " + new Similarity(data[7], data[2]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 3: " + new Similarity(data[7], data[3]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 4: " + new Similarity(data[7], data[4]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 5: " + new Similarity(data[7], data[5]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 6: " + new Similarity(data[7], data[6]).Euclidean());
//            Console.WriteLine("Euclidean distance similarity between users 7 and 7: " + new Similarity(data[7], data[7]).Euclidean());
//
//         
//
//            Console.WriteLine("Predict the rating that user 7 would give to items 106 = " + new PredictingRatings(data, 106, 7).Pearson());
//            Console.WriteLine("Predict the rating that user 7 would give to items 101 = " + new PredictingRatings(data, 101, 7).Pearson());
//            Console.WriteLine("Predict the rating that user 7 would give to items 103 = " + new PredictingRatings(data, 103, 7).Pearson());
//
//
//            
//            new NearestNeighbours(data, 7, 3,0.35).Pearson().PrintResult();
//            new NearestNeighbours(data, 7).Cosine().PrintResult();
//            new NearestNeighbours(data, 7).Euclidean().PrintResult();
//            
//            
//            // Using the MovieLens dataset, consider user 186 and create a list of the 8 top recommendations for him, 
//            new NearestNeighbours(data_movielens, 186, 8, 0.35).Pearson().PrintResult();


            // --------------------- Part 2 ---------------------
            Dictionary<int, Dictionary<int, double>> data2 =
                importer.GetContent("/DataScience/DataScience/itemItem.data");
            var itemItem = new ItemItem(data2);

//            Console.WriteLine("User 3 avarage = " +  itemItem.UserAvarage(3));
//            Console.WriteLine("User 4 avarage = " +  itemItem.UserAvarage(4));
//            Console.WriteLine("User 5 avarage = " +  itemItem.UserAvarage(5));

//            Console.WriteLine("Similarity between item 103 and 104 = " +  itemItem.Similarity(103, 104).Item1 + "(" + itemItem.Similarity(103, 104).Item2 + ")"); 
//            Console.WriteLine("Similarity between item 103 and 106 = " +  itemItem.Similarity(103, 106).Item1 + "(" + itemItem.Similarity(103, 106).Item2 + ")"); 
//            Console.WriteLine("Similarity between item 106 and 109 = " +  itemItem.Similarity(106, 109).Item1 + "(" + itemItem.Similarity(106, 109).Item2 + ")"); 
//            Console.WriteLine("Similarity between item 104 and 107 = " +  itemItem.Similarity(104, 107).Item1 + "(" + itemItem.Similarity(104, 107).Item2 + ")"); 
//            Console.WriteLine("Similarity between item 104 and 109 = " +  itemItem.Similarity(104, 109).Item1 + "(" + itemItem.Similarity(104, 109).Item2 + ")"); 

            Console.WriteLine("Predicted rating for user 1 for product 103 = " + itemItem.PredictRating(1, 103));
        }
    }
}