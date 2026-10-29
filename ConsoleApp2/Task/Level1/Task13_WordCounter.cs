using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Common;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task13_WordCounter : ITask
    {
        public string Title { get; } = "Счётчик вхождений слов в тексте (Dictionary)";

        public string Description { get; } = "Посчитать, сколько раз каждое слово встречается в тексте, и вывести отсортированный результат.";

        public string[] Requirements { get; } = new[]
        {
            "Использовать Dictionary<string, int>.",
        "Разбить текст на слова по пробелам и знакам пунктуации.",
        "Привести слова к нижнему регистру.",
        "Вывести пары «слово -> количество», отсортированные по убыванию количества."
        };

        public void Run()
        {
            string text = ConsoleHelper.ReadLine("Введите текст: ");

            Dictionary<string, int> counts = CountWords(text);

            if (counts.Count == 0)
            {
                Console.WriteLine("Слов не найдено.");
                return;
            }

            List<KeyValuePair<string, int>> sorted = OrderByCountDescending(counts);

            foreach (KeyValuePair<string, int> pair in sorted)
            {
                Console.WriteLine($"{pair.Key} -> {pair.Value}");
            }
        }

        public static Dictionary<string, int> CountWords(string text)
        {
            string[] words = text.Split(
                (char[])null,
                StringSplitOptions.RemoveEmptyEntries);

            var counts = new Dictionary<string, int>(StringComparer.Ordinal);

            foreach (string rawWord in words)
            {
                string word = TrimPunctuation(rawWord).ToLowerInvariant();

                if (word.Length == 0)
                {
                    continue;
                }

                if (counts.TryGetValue(word, out int current))
                {
                    counts[word] = current + 1;
                }
                else
                {
                    counts[word] = 1;
                }
            }

            return counts;
        }

        private static string TrimPunctuation(string word)
        {
            int start = 0;
            int end = word.Length - 1;

            while (start <= end && !char.IsLetterOrDigit(word[start]))
            {
                start++;
            }

            while (end >= start && !char.IsLetterOrDigit(word[end]))
            {
                end--;
            }

            return word.Substring(start, end - start + 1);
        }

        private static List<KeyValuePair<string, int>> OrderByCountDescending(Dictionary<string, int> counts)
        {
            var pairs = new List<KeyValuePair<string, int>>(counts);
            pairs.Sort(CompareByCountThenWord);
            return pairs;
        }

        private static int CompareByCountThenWord(KeyValuePair<string, int> left, KeyValuePair<string, int> right)
        {
            int byCount = right.Value.CompareTo(left.Value);

            if (byCount != 0)
            {
                return byCount;
            }

            return string.Compare(left.Key, right.Key, StringComparison.Ordinal);
        }
    }

}
