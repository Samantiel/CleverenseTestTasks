using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestTask1
{
    internal class Compression
    {
        static void Main(string[] args)
        {
        START:
            Console.Write("Введите строку латинских символов: ");
            string original = Console.ReadLine();
            string compressed = "";
            string decompressed = "";

            if (original.Length == 0)
            {
                Console.WriteLine("Вы введил пустую строку");
                goto START;
            }
            Regex reg = new Regex(@"[^a-zA-Z]+");

            Match m = reg.Match(original);

            if (m.Success)
            {
                Console.WriteLine("В вашей строке содержатся не латинские символы)");
                goto START;
            }

            for (int i = 0; i < original.Length; i++)
            {
                compressed += original[i];
                bool cont = true;
                int count = 1;
                while (cont)
                {
                    try
                    {
                        if (original[i] == original[i + count])
                        {
                            count++;
                        }
                        else cont = false;
                    }
                    catch { cont = false; }

                }
                if (count != 1)
                    compressed += $"{count}";

                i += count - 1;
            }

            Console.WriteLine($"Компрессированная строка: {compressed}");

            for (int i = 0; i < compressed.Length; i++)
            {
                try
                {
                    if (Char.IsNumber(compressed[i + 1]))
                    {
                        for (int j = 0; j < (int)Char.GetNumericValue(compressed[i + 1]); j++)
                            decompressed += compressed[i];
                    }
                    else
                    {
                        decompressed += compressed[i];
                    }
                }
                catch { decompressed += compressed[i]; }

                i++;
            }

            Console.WriteLine($"Декомпрессированная строка: {decompressed}");

            Console.ReadLine();
        }
    }
}
