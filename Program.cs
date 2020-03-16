﻿using System;
using Klingon;

namespace KlingonTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            string rl = "";
            double Z = 0;
            while (true)
            {
                Console.Clear();
                Console.Write("Zahl: ");
                rl = Console.ReadLine();
                if (double.TryParse(rl, out Z) && (Math.Abs(Z) < 10000000000))
                {
                    Console.Clear();
                    Console.WriteLine("Zahl: " + Z);
                    Console.WriteLine(Translate.NumberToKlingon(Z));
                }
                else
                {
                    if (Translate.KlingonToNumber(rl, out Z))
                    {
                        Console.WriteLine(Z);
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }
                Console.WriteLine("Drücke Enter");

                Console.ReadKey();
            }
        }
    }
}
