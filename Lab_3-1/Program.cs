using System;
using System.IO;
using System.Linq;

namespace Lab_3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "transactions.csv";
            string dateFormat = "dd/MM/yyyy";
            int batchSize = 10;

            Func<string, DateTime> getDate = (string transaction) =>
            {
                string[] fields = transaction.Split(',');
                return DateTime.ParseExact(fields[0], dateFormat, null);
            };

            Func<string, double> getAmount = (string transaction) =>
            {
                string[] fields = transaction.Split(',');
                return Double.Parse(fields[1]);
            };

            Action<DateTime, double> displayTotal = (DateTime date, double total) =>
            {
                Console.WriteLine("{0}: {1}", date.ToString(dateFormat), total);
            };

            int count = 0;
            int countDates = 0;
            int i = 1;
            DateTime currentDate = DateTime.MinValue;
            double currentTotal = 0;

            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                DateTime date = getDate(line);
                double amount = getAmount(line);

                if (countDates > batchSize)
                {
                    i++;
                    countDates = 1;
                }

                if (date != currentDate)
                {
                    if (count > 0)
                    {
                        displayTotal(currentDate, currentTotal);
                        string tempFilePath = string.Format("transactions_{0}.csv", i);
                        using (StreamWriter writer = new StreamWriter(tempFilePath, true))
                        {
                            writer.WriteLine("{0},{1}", currentDate.ToString(dateFormat), currentTotal);
                        }
                        currentTotal = 0;
                        count = 0;
                    }
                    currentDate = date;
                    countDates++;
                }
                currentTotal += amount;
                count++;
                
                if (line == File.ReadLines(filePath).Last())
                {
                    if (count > 0)
                    {
                        displayTotal(currentDate, currentTotal);
                        string tempFilePath = string.Format("transactions_{0}.csv", i);
                        using (StreamWriter writer = new StreamWriter(tempFilePath, true))
                        {
                            writer.WriteLine("{0},{1}", currentDate.ToString(dateFormat), currentTotal);
                        }
                        currentTotal = 0;
                        count = 0;
                    }
                }
            }
        }
    }
}
