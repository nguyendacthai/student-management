using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Class2 : Class1
    {
        public new string A()
        {
            return "a";
        }

        public override int B()
        {
            return 3;
        }
    }
}
