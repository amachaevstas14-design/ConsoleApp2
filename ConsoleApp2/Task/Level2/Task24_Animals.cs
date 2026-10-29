using ConsoleApp2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Task.Level2
{
    public sealed class Task24_Animals : ITask
    {
        public string Title { get; } = "ООП: Animal -> Dog/Cat/Bird";

        public string Description { get; } =
            "Построить иерархию животных на абстрактном классе и продемонстрировать полиморфизм.";

        public string[] Requirements { get; } = new[]
        {
            "Абстрактный класс Animal: свойство Name, абстрактный метод MakeSound(), переопределённый ToString().",
        "Классы Dog, Cat и Bird наследуются от Animal и реализуют MakeSound().",
        "Класс Bird дополнительно имеет метод Fly().",
        "Полиморфизм: вызов MakeSound() в цикле по List<Animal>."
        };

        public void Run()
        {
            List<Animal> animals = new List<Animal>
           {
            new Dog("Рекс"),
            new Cat("Мурка"),
            new Bird("Кряква"),
            new Dog("Шарик")
           };

            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal.ToString());
                Console.WriteLine($"  Звук: {animal.MakeSound()}");

                if (animal is Bird bird)
                {
                    Console.WriteLine($"  {bird.Fly()}");
                }

                Console.WriteLine();
            }
        }
    }
    public abstract class Animal
    {
        protected Animal(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public abstract string MakeSound();

        public override string ToString()
        {
            return $"{GetType().Name} (имя: {Name})";
        }
    }
    public sealed class Dog : Animal
    {
        public Dog(string name) : base(name)
        {
        }

        public override string MakeSound() => "Гав-гав!";
    }

    public sealed class Cat : Animal
    {
        public Cat(string name) : base(name)
        {
        }
        public override string MakeSound() => "Мяу-мяу!";
    }
    public sealed class Bird : Animal
    {
        public Bird(string name) : base(name)
        {
        }
        public override string MakeSound() => "Кря-кря!";
        public string Fly()
        {
            return $"{Name} летит по небу!";
        }

        
    }
}
