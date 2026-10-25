using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Common
{
    public sealed class TaskEntry
    {
        public int Level { get; }
        public int Number { get; }
        public Func<ITask> Factory { get; }

        public TaskEntry(int level, int number, Func<ITask> factory)
        {
            Level = level;
            Number = number;
            Factory = factory;
        }
    }
} 

