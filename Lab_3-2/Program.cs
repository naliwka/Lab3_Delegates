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
            string category = "Vegetable";
            double minPrice = 5.0;
            
            string folderPath = "json_files";

            Predicate<Product> filter = p => p.Category == category && p.Price < minPrice;
            Action<Product> display = p => Console.WriteLine($"Name: {p.Name}, Category: {p.Category}, Price: {p.Price}");

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.json"))
            {
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
