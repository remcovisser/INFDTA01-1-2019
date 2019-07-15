using System;
using System.Collections.Generic;
using System.IO;

namespace DataScience
{
    public class Importer
    {
        public Dictionary<int, Dictionary<int, double>> GetContent(string file)
        {
            // Get the data from the file and parse it to an array
            var parsedData = File.ReadAllText(file).Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();

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

            return data;
        }

        // Function to read the CSV file with the Movielens data
        public Dictionary<int, Dictionary<int, double>> GetMovielensData(string file)
        {
            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();
            using (var reader = new StreamReader(file))
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

            return data;
        }
    }
}