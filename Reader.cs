using System;

namespace Alphabet
{
    public static class Reader
    {
        public static string[,] ReadCharTable()
        {
            var width = int.Parse(Console.ReadLine());
            var height = int.Parse(Console.ReadLine());

            var chars = Console.ReadLine();

            var result = new string[height, width];
            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    var idx = r * width + c;
                    var nextChar = idx >= chars.Length ? " " : chars[idx].ToString();

                    result[r, c] = nextChar;
                }
            }
            return result;
        }

        public static string ReadWord() => Console.ReadLine();
    }
}