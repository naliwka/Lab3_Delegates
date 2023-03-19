using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab_3_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "TextFiles";

            // To tokenize the text in the file by separators and return a collection of words
            Func<string, IEnumerable<string>> wordParser = file => 
            {
                string[] separators = { " ", ".", ",", "!", "?", ";", ":", "-", "(", ")", "\"", "\'", "\n", "\r", "\t" };
                string[] words = File.ReadAllText(file).ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return words;
            };

            // To calculate the frequency of each word in a collection of words
            Func<IEnumerable<string>, IDictionary<string, int>> calculateWordFrequency = words => 
            {
                var frequency = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if (frequency.ContainsKey(word))
                    {
                        frequency[word]++;
                    }
                    else
                    {
                        frequency[word] = 1;
                    }
                }
                return frequency;
            };

            // To display word frequency statistics
            Action<IDictionary<string, int>> displayWordFrequency = frequency => 
            {
                foreach (var item in frequency.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            };

            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directory.GetFiles("*.txt"))
            {
                Console.WriteLine($"Statistics for file {file.Name}:");
                var words = wordParser(file.FullName);
                var frequency = calculateWordFrequency(words);
                displayWordFrequency(frequency);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}