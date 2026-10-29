using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task19_CustomStackAndQueue : ITask
    {
        public string Title { get; } = "Свой стек/очередь на массиве";

        public string Description { get; } =
            "Реализовать MyStack<T> и MyQueue<T> на основе массива.";

        public string[] Requirements { get; } = new[]
        {
            "Стек: Push, Pop, Peek, Count, IsEmpty.",
        "Очередь: Enqueue, Dequeue, Peek, Count, IsEmpty.",
        "При переполнении расширять внутренний массив вдвое.",
        "Pop и Dequeue на пустой коллекции должны выбрасывать InvalidOperationException."
        };

        public void Run()
        {
            Console.WriteLine("=== Стек MyStack<T> ===");

            var stack = new MyStack<int>();

            Console.WriteLine($"Стек пуст: {stack.IsEmpty}");

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50);

            Console.WriteLine($"Количество элементов: {stack.Count}");
            Console.WriteLine($"Верхний элемент: {stack.Peek()}");
            Console.WriteLine($"Извлечённый элемент: {stack.Pop()}");
            Console.WriteLine($"Новый верхний элемент: {stack.Peek()}");
            Console.WriteLine($"Количество элементов: {stack.Count}");

            Console.WriteLine("\nИзвлечение остальных элементов:");

            while (!stack.IsEmpty)
            {
                Console.WriteLine(stack.Pop());
            }

            Console.WriteLine($"Стек пуст: {stack.IsEmpty}");

            Console.WriteLine("\n=== Очередь MyQueue<T> ===");

            var queue = new MyQueue<int>();

            Console.WriteLine($"Очередь пуста: {queue.IsEmpty}");

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            queue.Enqueue(40);
            queue.Enqueue(50);

            Console.WriteLine($"Количество элементов: {queue.Count}");
            Console.WriteLine($"Первый элемент: {queue.Peek()}");
            Console.WriteLine($"Извлечённый элемент: {queue.Dequeue()}");
            Console.WriteLine($"Новый первый элемент: {queue.Peek()}");
            Console.WriteLine($"Количество элементов: {queue.Count}");

            Console.WriteLine("\nИзвлечение остальных элементов:");

            while (!queue.IsEmpty)
            {
                Console.WriteLine(queue.Dequeue());
            }

            Console.WriteLine($"Очередь пуста: {queue.IsEmpty}");
        }
    }

    public sealed class MyStack<T>
    {
        private T[] _items = new T[4];
        private int _count;

        public int Count => _count;

        public bool IsEmpty => _count == 0;

        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            _items[_count] = item;
            _count++;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Стек пуст.");
            }

            _count--;

            T item = _items[_count];
            _items[_count] = default;

            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Стек пуст.");
            }

            return _items[_count - 1];
        }
    }
    public sealed class MyQueue<T>
    {
        private T[] _items = new T[4];
        private int _count;

        public int Count => _count;

        public bool IsEmpty => _count == 0;

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            _items[_count] = item;
            _count++;
        }
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Очередь пуста.");
            }

            T item = _items[0];

            for (int i = 1; i < _count; i++)
            {
                _items[i - 1] = _items[i];
            }

            _count--;
            _items[_count] = default;

            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Очередь пуста.");
            }

            return _items[0];
        }
    }
}
