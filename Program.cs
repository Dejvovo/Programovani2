

using System;
using System.Collections.Generic;
using System.Linq;

namespace Alphabet
{
    record IntermediateResult((int, int) position, int minimumClickCount);

    public class Program
    {
        public static void Main()
        {
            try
            {
                RunProgram();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RunProgram()
        {
            var charTable = Reader.ReadCharTable();
            var word = Reader.ReadWord();

            var minimumClickCount = GetMinimumClickCount(charTable, word);

            Console.WriteLine(minimumClickCount);
        }

        private static int ShortestDistance((int, int) position1, (int, int) position2) =>
            Math.Abs(position1.Item1 - position2.Item1) +
            Math.Abs(position1.Item2 - position2.Item2);

        private static List<(int, int)> GetCharacterPositions(string character, string[,] charTable) =>
            charTable.FindIndices(c => c == character).ToList();

        public static int GetMinimumClickCount(string[,] charTable, string word)
        {
            var STARTING_POSITION = (0, 0);

            var minClickCounts =
                GetCharacterPositions(word[0].ToString(), charTable)
                .Select(position => new IntermediateResult(position, ShortestDistance(STARTING_POSITION, position) + 1));

            if (word.Length == 1) return minClickCounts.Min(c => c.minimumClickCount);

            for (var idx = 1; idx < word.Length; idx++)
            {
                var nextCharPositions = GetCharacterPositions(word[idx].ToString(), charTable);
                if (nextCharPositions.Count == 0) continue;
                var nextMinClickCounts =
                    nextCharPositions.Select(nextCharPosition =>
                    {
                        var shortestDistance = minClickCounts.Min(m => ShortestDistance(m.position, nextCharPosition) + m.minimumClickCount);
                        return new IntermediateResult(nextCharPosition, shortestDistance + 1);
                    }).ToList();
                minClickCounts = nextMinClickCounts;
            }

            return minClickCounts.Min(counts => counts.minimumClickCount);
        }
    }
}