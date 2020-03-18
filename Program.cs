using System;
using System.Globalization;
using Klingon;

namespace KlingonTranslator
{
    class Program
    {
        static void Main()
        {
            string rl = "";
            double Z = 0;
            while (true)
            {
                Console.Clear();
                Console.Write("Number: ");
                rl = Console.ReadLine();

                if (double.TryParse(rl.Replace('.', ','), NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "," }, out Z))
                {
                    Console.Clear();
                    Console.WriteLine("Number: " + Z);
                    if (Translate.TryNumberToKlingon(Z, out var klingonNumber))
                    {
                        Console.WriteLine(klingonNumber);
                    }
                }
                else
                {
                    if (Translate.TryKlingonNumber(rl, out Z))
                    {
                        Console.WriteLine(Z);
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                }
                Console.WriteLine("Press Any Button");

                Console.ReadKey();
            }
        }
    }
}
