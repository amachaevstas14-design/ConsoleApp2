using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ConsoleApp2.Task.Level1;
using ConsoleApp2.Task.Level2;
using ConsoleApp2.Task.Level3;

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
        new TaskEntry(2,19, CreateTask19),
        new TaskEntry(2,20, CreateTask20),
        new TaskEntry(2,21, CreateTask21),
        new TaskEntry(2,22, CreateTask22),
        new TaskEntry(2,23,CreateTask23),
        new TaskEntry(2,24, CreateTask24),
        new TaskEntry(2,25, CreateTask25),
        new TaskEntry(2,26, CreateTask26),
        new TaskEntry(2,27, CreateTask27),
        new TaskEntry(2,28, CreateTask28),
        new TaskEntry(2,29, CreateTask29),
        new TaskEntry(2,30, CreateTask30),
        new TaskEntry(2,31, CreateTask31),
        new TaskEntry(2,32, CreateTask32),
        new TaskEntry(2,33, CreateTask33),
        new TaskEntry(2,34, CreateTask34),
        new TaskEntry(3,36, CreateTask36),
        new TaskEntry(3, 37, CreateTask37),
        new TaskEntry(3,38, CreateTask38),
        new TaskEntry(3,39, CreateTask39),
        new TaskEntry(3,40, CreateTask40),
        new TaskEntry(3,41,CreateTask41)

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

        private static ITask CreateTask20()
        {
            return new Task20_GenericMethods();
        }

        private static ITask CreateTask21()
        {
            return new Task21_EnumDaysOfWeek();
        }

        private static ITask CreateTask22()
        {
            return new Task22_ParamsSum();
        }

        private static ITask CreateTask23()
        {
            return new Task23_NullableOperators();
        }

        private static ITask CreateTask24()
        {
            return new Task24_Animals();
        }

        private static ITask CreateTask25()
        {
            return new Task25_Shapes();
        }

        private static ITask CreateTask26()
        {
            return new Task26_DiscountStrategy();
        }

        private static ITask CreateTask27()
        {
            return new Task27_StudentComparers();
        }

        private static ITask CreateTask28()
        {
            return new Task28_PointEquality();
        }

        private static ITask CreateTask29()
        {
            return new Task29_NoificationFactory();
        }

        private static ITask CreateTask30()
        {
            return new Task30_BankAccount();
        }

        private static ITask CreateTask31()
        {
            return new Task31_DisposeUsing();
        }

        private static ITask CreateTask32()
        {
            return new Task32_Temperature();
        }

        private static ITask CreateTask33()
        {
            return new Task33_LinqEmployees();
        }

        private static ITask CreateTask34()
        {
            return new Task34_LinqWordGroups();
        }

        private static ITask CreateTask36()
        {
            return new Task36_Duplicates();
        }

        private static ITask CreateTask37()
        {
            return new Task37_FibonacciMemoization();
        }

        private static ITask CreateTask38()
        {
            return new Task38_ListVsLinkedList();
        }

        private static ITask CreateTask39()
        {
            return new Task39_YieldReturn();
        }

        private static ITask CreateTask40()
        {
            return new Task40_AsyncDownload();
        }

        private static ITask CreateTask41()
        {
            return  new Task41_ParallelAsync();
        }
    }
}
