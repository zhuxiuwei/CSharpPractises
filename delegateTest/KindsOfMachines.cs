using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateTest
{
    public class MachineA
    {
        public void machinaAFunc(){
            Console.WriteLine("Function of machine A!");
        }
    }

    public class MachineB
    {
        public void machinaBFunc()
        {
            Console.WriteLine("Function of machine B!");
        }
    }

    public class MachineC
    {
        public void machinaCFunc()
        {
            Console.WriteLine("Function of machine C!");
        }
    }
}
