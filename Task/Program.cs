using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/**
 * 150615 xiuzhu 
 */
namespace UsingTask
{
    class Program
    {
        private void doWork()
        {
            Console.WriteLine("doWork with no parameter!");
        }

        private void doWorkWithObject(object o)
        {
            Console.WriteLine("doWork with object: " + o.ToString());
        }

        private void printInt(int i)
        {
            Console.Write(i + " ");
        }

        private void doCancelableWork(CancellationToken token){
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancel detected.");
                    return;
                }
                Console.WriteLine(i + " second.");
                Thread.Sleep(1000);
            }
        }

        private void doCancelableWorkThrowException(CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                else
                {
                    Console.WriteLine(i + " second.");
                    Thread.Sleep(500);
                }
                
            }
        }
        private async void callDoCancelableWorkThrowException(CancellationTokenSource source)
        {
            
            Task task1 = null;
            try
            {
                task1 = Task.Run(() => doCancelableWorkThrowException(source.Token), source.Token);
                await task1;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Exception= " + e.Message);
            }
            finally
            {
                Console.WriteLine("Task status: " + task1.Status);
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            
            //Task task1 = new Task(p.doWork);
            //task1.Start();

            //Action<object> action = p.doWorkWithObject; //用这种方式给Task调用的函数传参
            //Task task2 = new Task(action, "hello world!");
            //task2.Start();

            //Parallel.For(0, 10, p.printInt);
            //Console.WriteLine("");

            //List<int> intList = new List<int>{1,2,3,4,5,6,7,8,9};
            //Parallel.ForEach<int>(intList, p.printInt);


            //Test cancel a task
            //CancellationTokenSource source = new CancellationTokenSource();
            //CancellationToken token = source.Token;
            //Task task = new Task(() => p.doCancelableWork(token));
            //task.Start();
            //Thread.Sleep(5000);   //calcel the task 5s later.
            //source.Cancel();
            ////test task status.
            //Console.WriteLine("Task status: " + task.Status);
            //Thread.Sleep(1000);
            //Console.WriteLine("Task status: " + task.Status);   //Notice: result is RanToComplete, not Cancel

            //test cancel task which throws TaskCanceledException

            CancellationTokenSource source = new CancellationTokenSource();
            p.callDoCancelableWorkThrowException(source);
            Thread.Sleep(1100);
            source.Cancel();
           

            Console.ReadLine();
        }
    }
}
