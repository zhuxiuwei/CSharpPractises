using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingAsyncMethods
{
    class TaskReturnValue
    {
        private static async Task<int> calValAsync(int source)
        {
            Task<int> t = Task.Run(() =>{
                for (int i = 0; i < 10; i++)
                {
                    source += 1;
                    Console.WriteLine("Calcuating...");
                    Thread.Sleep(100);
                }
                return source;
            } );
            await t;
            Console.WriteLine("Calcuate done");
            return t.Result;
        }

        public static async void callCalValAsync(int source)
        {
            int result = await calValAsync(source);
            Console.WriteLine("result is " + result);
        }
    }
}
