using System;
using System.Collections.Generic;
using System.IO;

namespace INFDTA021
{
    public class Importer
    {
        public Dictionary<int, Dictionary<int, double>> GetContent(string file)
        {
            string[] parsed_data;
            parsed_data = GetData(file);    

            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();
          
            foreach(string raw_rating in parsed_data)
            {
                // Get specific data from the array and parse it to the correct datatype
                string[] rating_values = raw_rating.Split(',');

                if (rating_values[2] == "")
                {
                    continue;
                }
                    
                    
                int user_id = Int32.Parse(rating_values[0]);
                int article_id = Int32.Parse(rating_values[1]);
                double score = double.Parse(rating_values[2]);

                // Only add the user_id to the dictionary once
                if (!data.ContainsKey(user_id)) {
                    Dictionary<int, double> rating_dic = new Dictionary<int, double>();
                    data.Add(user_id, rating_dic);
                }

                // Add the rating to the user ratings dictionary
                Dictionary<int, double> user_ratings = data[user_id];
                user_ratings.Add(article_id, score);
            }

            return data;
        }

        // Get the data from the file and parse it to an array
        private string[] GetData(string file)
        {
            string raw_data = File.ReadAllText("../../" + file);
            string[] parsed_data = raw_data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return parsed_data;
        }

        public Dictionary<int, Dictionary<int, double>> GetMovielensData(string file)
        {
            Dictionary<int, Dictionary<int, double>> data = new Dictionary<int, Dictionary<int, double>>();
            using (var reader = new StreamReader("../../" + file))
            {
                reader.ReadLine(); // Skip first line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Get specific data from the array and parse it to the correct datatype
                    int user_id = Int32.Parse(values[0]);
                    int article_id = Int32.Parse(values[1]);
                    double score = double.Parse(values[2]);

                    // Only add the user_id to the dictionary once
                    if (!data.ContainsKey(user_id))
                    {
                        Dictionary<int, double> rating_dic = new Dictionary<int, double>();
                        data.Add(user_id, rating_dic);
                    }

                    // Add the rating to the user ratings dictionary
                    Dictionary<int, double> user_ratings = data[user_id];
                    user_ratings.Add(article_id, score);
                }
            }

            return data;
        }
    }
}
