using System;
using System.Collections.Generic;
using System.IO;

namespace DataScience.Data
{
    public class Importer
    {
        public Dictionary<int, Dictionary<int, double>> GetContent(string path, string file, bool withOutZero = false)
        {
            // Get the data from the file and parse it to an array
            var parsedData = File.ReadAllText(path + file).Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();
            List<int> uniqueProductKeys = new List<int>();

            foreach (string rawRating in parsedData)
            {
                // Get specific data from the array and parse it to the correct datatype
                string[] ratingValues = rawRating.Split(',');

                // If it is the fist line, continue
                if (ratingValues[2] == "")
                {
                    continue;
                }

                int userId = Int32.Parse(ratingValues[0]);
                int articleId = Int32.Parse(ratingValues[1]);
                double score = double.Parse(ratingValues[2]);

                if (!uniqueProductKeys.Contains(articleId))
                {
                    uniqueProductKeys.Add(articleId);
                }

                // Only add the user_id to the dictionary once, without this check unnecessarily code is executed
                if (!data.ContainsKey(userId))
                {
                    Dictionary<int, double> ratings = new Dictionary<int, double>();
                    data.Add(userId, ratings);
                }

                // Add the rating to the user ratings dictionary
                Dictionary<int, double> userRatings = data[userId];
                userRatings.Add(articleId, score);
            }

            if (withOutZero)
            {
                return data;
            }

            return AddNotRatedProducts(data, uniqueProductKeys);
        }

        // Function to read the CSV file with the Movielens data
        public Dictionary<int, Dictionary<int, double>> GetMovielensData(string path, string file, bool withOutZero = false)
        {
            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();
            List<int> uniqueProductKeys = new List<int>();
            
            using (var reader = new StreamReader(path + file))
            {
                // If it is the fist line, skip it
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Get specific data from the array and parse it to the correct datatype
                    int userId = Int32.Parse(values[0]);
                    int articleId = Int32.Parse(values[1]);
                    double score = double.Parse(values[2]);
                    
                    if (!uniqueProductKeys.Contains(articleId))
                    {
                        uniqueProductKeys.Add(articleId);
                    }

                    // Only add the userId to the dictionary once
                    if (!data.ContainsKey(userId))
                    {
                        Dictionary<int, double> ratingDic = new Dictionary<int, double>();
                        data.Add(userId, ratingDic);
                    }

                    // Add the rating to the user ratings dictionary
                    Dictionary<int, double> userRatings = data[userId];
                    userRatings.Add(articleId, score);
                }
            }

            if (withOutZero)
            {
                return data;
            }

            return AddNotRatedProducts(data, uniqueProductKeys);
        }

        private Dictionary<int, Dictionary<int, double>> AddNotRatedProducts(Dictionary<int, Dictionary<int, double>> data, List<int> uniqueProductKeys)
        {
            foreach (KeyValuePair<int, Dictionary<int, double>> userRatings in data)
            {
                foreach (int productId in uniqueProductKeys)
                {
                    if (!userRatings.Value.ContainsKey(productId))
                    {
                        var rating = new Dictionary<int, double>();
                        rating.Add(productId, 0.0);
                        data[userRatings.Key].Add(productId, 0.0);
                    }
                }
            }

            return data;
        }
    }
}