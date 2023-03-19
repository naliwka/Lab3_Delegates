using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Lab_3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criterias:
            string category = "Vegetable";
            double minPrice = 5.0;
            
            string directoryPath = "json_files";

            // To filter products by criterias
            Predicate<Product> filter = p => p.Category == category && p.Price < minPrice;
            // To display filtered products
            Action<Product> display = p => Console.WriteLine($"Name: {p.Name}, Category: {p.Category}, Price: {p.Price}");

            foreach (string file in Directory.EnumerateFiles(directoryPath, "*.json"))
            {
                // To read the file and deserialize JSON data
                StreamReader reader = new StreamReader(file);
                string jsonText  = reader.ReadToEnd();
                Product product = JsonConvert.DeserializeObject<Product>(jsonText);
                if (filter(product))
                {
                    display(product);
                }
            }           
        }
    }
}
