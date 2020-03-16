using System;
using System.Collections.Generic;
using System.Text;

namespace Klingon
{
    class Translate
    {
        private static string KlingonDecimalMark = "vI'";

        private static string KlingonNegativeSign = "boqHa'";

        private static string[] KlingonNumbers =
        {
            "pagh",
            "wa'",
            "cha'",
            "wej",
            "loS",
            "vagh",
            "jav",
            "Soch",
            "chorgh",
            "Hut"
        };

        private static string[] KlingonMultiply =
        {
            "",
            "maH",
            "vatlh",
            "SaD",
            "netlh",
            "bIp",
            "'uy'",
            "Saghan"
        };

        public static bool KlingonToNumber(string text, out double z)
        {
            bool r = true;
            z = 0;
            bool dec = false;
            int decCounter = -1;
            text = text.Replace("SanID", KlingonMultiply[3]);
            if (text == KlingonNumbers[0])
            {
                return true;
            }
            string[] numbers = text.Split(' ');
            foreach (string number in numbers)
            {
                bool done = false;
                if (number == KlingonNegativeSign)
                {
                    done = true;
                }

                if (number == KlingonDecimalMark)
                {
                    dec = true;
                    done = true;
                }

                if (!done)
                {
                    for (int n = 1; n <= 9; n++)
                    {
                        if (number.Contains(KlingonNumbers[n]))
                        {
                            if (number == KlingonNumbers[n] || dec)
                            {
                                if (dec)
                                {
                                    z += (n * Math.Pow(10, decCounter));
                                    decCounter--;
                                }
                                else
                                {
                                    z += n;
                                }
                                done = true;
                                break;
                            }
                            else
                            {
                                for (int m = 1; m < KlingonMultiply.Length; m++)
                                {
                                    if (number.Contains(KlingonMultiply[m]))
                                    {
                                        z += (n * (Math.Pow(10, m)));
                                        done = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!done)
                {
                    Console.WriteLine("\"" + number + "\" nicht erkannt");
                    return false;
                }
            }

            if (numbers[0] == KlingonNegativeSign)
            {
                z *= -1;
            }
            return r;
        }

        public static string NumberToKlingon(double number)
        {
            if (number == 0)
            {
                return KlingonNumbers[0];
            }

            StringBuilder z = new StringBuilder();

            if (number < 0)
            {
                z.Append(KlingonNegativeSign + " ");
                number = Math.Abs(number);
            }

            bool isDecimal = false;
            string tempStr = number.ToString();
            List<ushort> dec = new List<ushort>();
            if (tempStr.Contains(","))
            {
                string[] tempStrArr = tempStr.Split(',');
                tempStr = tempStrArr[0];
                foreach (char decimalNumber in tempStrArr[1])
                {
                    dec.Add(ushort.Parse(decimalNumber.ToString()));
                }
                isDecimal = true;
            }
            else if (tempStr.Contains("."))
            {
                string[] tempStrArr = tempStr.Split('.');
                tempStr = tempStrArr[0];
                foreach (char decimalNumber in tempStrArr[1])
                {
                    dec.Add(ushort.Parse(decimalNumber.ToString()));
                }
                isDecimal = true;
            }

            char[] I = tempStr.ToCharArray();

            for (int i = 0; i < I.Length; i++)
            {
                int nowNumber;
                if (int.TryParse(I[i].ToString(), out nowNumber))
                {
                    if (nowNumber != 0)
                    {
                        z.Append(KlingonNumbers[nowNumber]);

                        int j = (I.Length - 1) - i;
                        z.Append(KlingonMultiply[j]);
                        z.Append(" ");
                    }
                }
                else
                {
                    Console.WriteLine("Error tranalating '" + I[i] + "'");
                }
            }

            if (isDecimal)
            {
                z.Append(KlingonDecimalMark);
                foreach (ushort i in dec)
                {
                    z.Append(" ");
                    z.Append(KlingonNumbers[i]);
                }
            }

            return z.ToString();
        }
    }
}
