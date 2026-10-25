using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level1
{
    public sealed class Task11_MatrixDiagonal : ITask
    {
        public string Title { get; } = "Диагональ матрицы NxN";

        public string Description { get; } = "Построить квадратную матрицу и найти сумму элементов её главной диагонали.";

        public string[] Requirements { get; } = new[]
        {
            "Размер N вводится пользователем (минимум 2).",
        "Матрица заполняется случайными числами.",
        "Сумма считается без LINQ, в одном цикле for.",
        "Вывести саму матрицу и сумму."
        };

        public void Run()
        {
            int size = ConsoleHelper.ReadInt("Введите размер матрицы N (минимум 2): ", 2, 20);

            int[,] matrix = FillRandom(size, 0, 99);

            Console.WriteLine("Матрица:");
            Print(matrix);

            Console.WriteLine($"Сумма главной диагонали: {DiagonalSum(matrix)}");
        }

        private static int[,] FillRandom(int size, int min, int max)
        {
            var random = new Random();
            int[,] matrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    matrix[row, column] = random.Next(min, max + 1);
                }
            }

            return matrix;
        }

        private static int DiagonalSum(int[,] matrix)
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                sum += matrix[i, i];
            }

            return sum;
        }

        private static void Print(int[,] matrix)
        {
            int size = matrix.GetLength(0);

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Console.Write($"{matrix[row, column],4}");
                }

                Console.WriteLine();
            }
        }
    }
}
