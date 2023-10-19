using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rascunhos.Entities;

namespace Rascunhos
{

    class Program
    {
        static void Main(string[] args)
        {
            // C:\Users\crist\OneDrive\Área de Trabalho\Products.csv

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Product> list = new List<Product>();

            using(StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                }
            }

            double avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average price = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            IEnumerable<string> names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }
        }      
    }

}
