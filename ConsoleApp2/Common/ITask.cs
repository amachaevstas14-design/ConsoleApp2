using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Common
{
    public interface ITask
    {
        string Title { get; }

        string Description { get; }

        string[] Requirements { get; }

        void Run();
    }
}
