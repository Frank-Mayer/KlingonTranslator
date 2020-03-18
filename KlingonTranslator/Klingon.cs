using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klingon
{
    public static class Translate
    {
        public const string KlingonDecimalMark = "vI'";

        public const string KlingonNegativeSign = "boqHa'";

        public static readonly string[] KlingonNumbers =
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

        public static readonly string[] KlingonMultiply =
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

        private static int GetKlingonIndex(string s)
        {
            for (int i = 0; i < KlingonNumbers.Length; i++)
            {
                string n = KlingonNumbers[i];
                if (s.Contains(n))
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool TryKlingonNumber(string text, out double z)
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
                if (number == KlingonNegativeSign)
                {
                    continue;
                }

                if (number == KlingonDecimalMark)
                {
                    dec = true;
                    continue;
                }

                int index = GetKlingonIndex(number);
                if (index > -1)
                {
                    if (number == KlingonNumbers[index] || dec)
                    {
                        if (dec)
                        {
                            z += (index * Math.Pow(10, decCounter));
                            decCounter--;
                        }
                        else
                        {
                            z += index;
                        }
                        break;
                    }
                    else
                    {
                        for (int m = 1; m < KlingonMultiply.Length; m++)
                        {
                            if (number.Contains(KlingonMultiply[m]))
                            {
                                z += (index * (Math.Pow(10, m)));
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\"" + number + "\" not recognized");
                    return false;
                }
            }

            if (numbers[0] == KlingonNegativeSign)
            {
                z *= -1;
            }
            return r;
        }

        public static bool TryNumberToKlingon(double number, out string klingonNumber)
        {
            if (Math.Abs(number) >= 10000000000)
            {
                klingonNumber = "";
                return false;
            }

            if (number == 0)
            {
                klingonNumber = KlingonNumbers[0];
                return true;
            }

            StringBuilder klingonNumberBuilder = new StringBuilder();

            if (number < 0)
            {
                klingonNumberBuilder.Append(KlingonNegativeSign + " ");
                number = Math.Abs(number);
            }

            bool isDecimal = false;
            string tempStr = number.ToString();
            List<ushort> decimalPlaces = new List<ushort>();
            if (tempStr.Contains(","))
            {
                getDecimalPlaces(',', decimalPlaces, ref tempStr);
            }
            else if (tempStr.Contains("."))
            {
                getDecimalPlaces('.', decimalPlaces, ref tempStr);
            }
            isDecimal = decimalPlaces.Count > 0;

            char[] I = tempStr.ToCharArray();

            for (int i = 0; i < I.Length; i++)
            {
                int nowNumber;
                if (int.TryParse(I[i].ToString(), out nowNumber))
                {
                    if (nowNumber != 0)
                    {
                        if (klingonNumberBuilder.Length > 0)
                        {
                            klingonNumberBuilder.Append(" ");
                        }

                        klingonNumberBuilder.Append(KlingonNumbers[nowNumber]);

                        int j = (I.Length - 1) - i;
                        klingonNumberBuilder.Append(KlingonMultiply[j]);
                    }
                }
                else
                {
                    klingonNumber = "";
                    return false;
                }
            }

            if (isDecimal)
            {
                klingonNumberBuilder.Append(" ");
                klingonNumberBuilder.Append(KlingonDecimalMark);
                foreach (ushort i in decimalPlaces)
                {
                    klingonNumberBuilder.Append(" ");
                    klingonNumberBuilder.Append(KlingonNumbers[i]);
                }
            }

            klingonNumber = klingonNumberBuilder.ToString();
            return true;
        }

        private static void getDecimalPlaces(char splitter, List<ushort> decimalPlaces, ref string text)
        {
            if (text.ToCharArray().Where(x => x == splitter).Count() > 1)
            {
                throw new ArgumentException("Invalid Number");
            }

            string[] tempStrArr = text.Split(splitter);
            if (tempStrArr.Length >= 2)
            {
                text = tempStrArr[0];
                foreach (char decimalNumber in tempStrArr[1])
                {
                    decimalPlaces.Add(ushort.Parse(decimalNumber.ToString()));
                }
            }
        }
    }
}
