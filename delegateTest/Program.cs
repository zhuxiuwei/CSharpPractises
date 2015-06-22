using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            MachineA ma = new MachineA();
            MachineB mb = new MachineB();
            MachineC mc = new MachineC();

            MachineFuncDelegate mfd = new MachineFuncDelegate();
            mfd.FuncDelegate += ma.machinaAFunc;
            mfd.FuncDelegate += mb.machinaBFunc;
            mfd.Add(mc.machinaCFunc);

            mfd.ExecuteFunc();
        }
    }
}
