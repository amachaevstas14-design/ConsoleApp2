using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ConsoleApp2.Task.Level1;
using ConsoleApp2.Task.Level2;

namespace ConsoleApp2.Common
{
    public static  class TaskRegistry
    {
        private static readonly TaskEntry[] _entries = new[]
    {
        new TaskEntry(1, 1, CreateTask01),
        new TaskEntry(1, 2, CreateTask02),
        new TaskEntry(1, 3, CreateTask03),
        new TaskEntry(1, 4, CreateTask04),
        new TaskEntry(1, 5, CreateTask05),
        new TaskEntry(1, 6, CreateTask06),
        new TaskEntry(1, 7, CreateTask07),
        new TaskEntry(1, 8, CreateTask08),
        new TaskEntry(1, 9, CreateTask09),
        new TaskEntry(1, 10, CreateTask10),
        new TaskEntry(1, 11, CreateTask11),
        new TaskEntry(1, 12, CreateTask12),
        new TaskEntry(1, 13, CreateTask13),
        new TaskEntry(1, 14, CreateTask14),
        new TaskEntry(2,16, CreateTask16),
        new TaskEntry(2,17,CreateTask17 ),
        new TaskEntry(2,18, CreateTask18),
        new TaskEntry(2,19, CreateTask19)

        };

        public static IReadOnlyList<TaskEntry> GetTasks(int level)
        {
            var result = new List<TaskEntry>();

            foreach (TaskEntry entry in _entries)
            {
                if (entry.Level == level)
                {
                    result.Add(entry);
                }
            }

            result.Sort(CompareByNumber);
            return result;
        }

        private static int CompareByNumber(TaskEntry left, TaskEntry right)
        {
            return left.Number.CompareTo(right.Number);
        }

        private static ITask CreateTask01()
        {
            return new Task01_HelloWorld();
        }

        private static ITask CreateTask02()
        {
            return new Task02_Primitives();
        }

        private static ITask CreateTask03()
        {
            return new Task03_Calculator();
        }

        private static ITask CreateTask04()
        {
            return new Task04_Evenness();
        }

        private static ITask CreateTask05()
        {
            return new Task05_MultiplicationTable();
        }

        private static ITask CreateTask06()
        {
            return new Task06_DigitSum();
        }

        private static ITask CreateTask07()
        {
            return new Task07_StringReversal();
        }

        private static ITask CreateTask08()
        {
            return new Task08_FizzBuzz();
        }

        private static ITask CreateTask09()
        {
            return new Task09_ArrayStatistics();
        }

        private static ITask CreateTask10()
        {
            return new Task10_SortingComparison();
        }

        private static ITask CreateTask11()
        {
            return new Task11_MatrixDiagonal();
        }

        private static ITask CreateTask12()
        {
            return new Task12_Palindrome();
        }

        private static ITask CreateTask13()
        {
            return new Task13_WordCounter();
        }

        private static ITask CreateTask14()
        {
            return new Task14_AgeCalculator();
        }

        private static ITask CreateTask16()
        {
            return new Task16_StringExtensions();
        }

        private static ITask CreateTask17()
        {
            return new Task17_StringBuilderVsConcatenation();
        }

        private static ITask CreateTask18()
        {
            return new Task18_TryParseVsTryCatch();
        }

        private static ITask CreateTask19()
        {
            return new Task19_CustomStackAndQueue();
        }

        
    }
}
