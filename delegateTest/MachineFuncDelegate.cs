using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateTest
{
    public class MachineFuncDelegate
    {
        //The delegate type。注意返回类型和参数。
        public delegate void machineFuncDelegate();
        //An instance of delegate
        private machineFuncDelegate funcDelegate;

        //提供两种方式给外部客户调用该代理.
        public machineFuncDelegate FuncDelegate
        {
            get
            {
                return this.funcDelegate;
            }
            set
            {
                this.funcDelegate = value;
            }
        }
        public void Add(machineFuncDelegate method)
        {
            funcDelegate += method;
        }
        public void Remove(machineFuncDelegate method)
        {
            funcDelegate -= method;
        }

        public void ExecuteFunc()
        {
            this.funcDelegate();
        }
    }

}
