using ConsoleApp2.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main()
        {
            var runner = new MenuRunner(new LevelMenu());
            runner.Run();
        }
    }
}
