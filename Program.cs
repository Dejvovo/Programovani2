using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simulace
{
    class Program   
    {
        private static string nazevSouboru = "obchod_data.txt";

        static List<int> ProvedSimulaci(int pocetZakazniku, Random random) {
            var model = new Model(nazevSouboru, pocetZakazniku, random);
            model.Vypocet();
            var casyZakazniku = model.VsichniZakaznici.Select(z => z.CasKteryStravilVObchodnimDome);  

            return casyZakazniku.ToList();
        }


        static void Main(string[] args)
        {

            for(var pocetZakazniku = 1; pocetZakazniku <= 91; pocetZakazniku += 10) {
                var prumery = new List<double>(); 
                var random = new Random(12345);
    
                for(var i=0; i< 10; i++) {

                    var casy = ProvedSimulaci(pocetZakazniku, random);
                    var prumer = casy.Average();
                    prumery.Add(prumer);
                }

                var max = prumery.Max();
                var min = prumery.Min();
                prumery.Remove(max);
                prumery.Remove(min);


                Console.WriteLine($"{pocetZakazniku} zakaznik -> {Math.Round(prumery.Average(), 2)}");
            }

            Console.WriteLine(" KONEC --------------------------------");
            Console.ReadLine();
        }
    }
}