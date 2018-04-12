using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tresor
{
    class Program
    {
        static void Main(string[] args)
        {
            TresorCore core = new TresorCore();
            core.init(args);
            core.start();
        }
    }
}
