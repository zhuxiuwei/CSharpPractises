using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingAsyncMethods
{
    ///想要的效果，就是在slowMethod内部是完全串行执行，但是对其它thread（这里的Main函数）是async的。
    class DefineMethods { 
        /**
         * 完全是顺序执行，阻塞式的。 -- bad
         * */
        public void slowMethod()
        {
            doFirstLongRunningOperation();
            doSecondLongRunningOperation();
            doThirdLongRunningOperation();
            Console.WriteLine("Processing Completed");
        }

        /**
         * - 对Main 函数的Thread来说，是并行执行的。   -- good
         * - slowMethod内部，t2 t3等t执行完了，并行执行。 --ok
         * - Processing completed和t是并行执行的 -- bad
         * 
         */
        public void slowMethod2()
        {
            Task t = new Task(doFirstLongRunningOperation);
            t.ContinueWith(doSecondLongRunningOperation);
            t.ContinueWith(doThirdLongRunningOperation);
            t.Start();
            Console.WriteLine("Processing Completed");
        }

        /*
         * - 对Main 函数的Thread来说，是阻塞式的。     -- bad
         * - slowMethod内部，t2 t3等t执行完了，并行执行。  -- ok
         * - Processing completed等t t2 t3都完了执行。  -- good
         */
        public void slowMethod3()   
        {
            Task t = new Task(doFirstLongRunningOperation); 
            var t2 = t.ContinueWith(doSecondLongRunningOperation);
            var t3 = t.ContinueWith(doThirdLongRunningOperation);
            t.Start();
            //t.Wait();
            Task.WaitAll(t2, t3);
            Console.WriteLine("\r\nProcessing Completed");
        }

        /*
         * - 对Main 函数的Thread来说，是并行执行的。     -- good
         * - slowMethod内部，t2 t3等t执行完了，并行执行。  -- ok
         * - Processing completed等t执行完了执行。它是和t2 t3并行的  -- bad
         * 本质和slowMethod2没区别。
         */
        public void slowMethod4()
        {
            Task t = new Task(doFirstLongRunningOperation);

            //t结束后，t2 t3 t4并发执行。
            var t2 = t.ContinueWith(doSecondLongRunningOperation);
            var t3 = t.ContinueWith(doThirdLongRunningOperation);
            var t4 = t.ContinueWith((tt) => Console.WriteLine("Processing Completed"));  
            t.Start();
        }

        /*
         * - 对Main 函数的Thread来说，是并行执行的。     -- good
         * - slowMethod内部，t2 t3等t执行完了，并行执行。  -- ok
         * - Processing completed t2 t3 t4全部执行才执行  -- ok
         * 效果和slowMethod5一样。
         */
        public void slowMethod4_2()
        {
            Task t = new Task(doFirstLongRunningOperation);

            //t结束后，t2 t3 t4并发执行。
            var t2 = t.ContinueWith(doSecondLongRunningOperation);
            var t3 = t2.ContinueWith(doThirdLongRunningOperation);
            var t4 = t3.ContinueWith((tt) => Console.WriteLine("Processing Completed"));
            t.Start();
        }

        /*
         * - 对Main 函数的Thread来说，是并行执行的。     -- good
         * - slowMethod内部，严格串行执行。  -- ok
         * - Processing completed等全部执行才执行  -- ok
         */
        public async void slowMethod5()
        {
            await doFirstLongRunningOperation2();
            await doSecondLongRunningOperation2();
            await doThirdLongRunningOperation2();
            await doFourthLongRunningOperation();
            Console.WriteLine("\r\nProcessing Completed");
        }

        private void doFirstLongRunningOperation()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.Write("1st ");
            }
        }
        private Task doFirstLongRunningOperation2()
        {
            Task t = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("1st ");
                }
            });
            return t;
        }

        private void doSecondLongRunningOperation()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("2nd ");
            }
        }
        private void doSecondLongRunningOperation(Task t)
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.Write("2nd ");
            }
        }
        private Task doSecondLongRunningOperation2()
        {
            Task t = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("2nd ");
                }
            });
            return t;
        }

        private void doThirdLongRunningOperation()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("3rd ");
            }
        }
        private void doThirdLongRunningOperation(Task t)
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.Write("3rd ");
            }
        }
        private Task doThirdLongRunningOperation2()
        {
            Task t = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("3rd ");
                }
            });
            return t;
        }

        private async Task doFourthLongRunningOperation()
        {
            Task first = Task.Run(() =>
            {
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("4th-1 ");
                };
            });
            Task second = Task.Run(() =>
            {
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {

                     {
                         Thread.Sleep(100);
                         Console.Write("4th-2 ");
                     };
                }
            });
            await first;
            await second;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            //DefineMethods m = new DefineMethods();
            //m.slowMethod();
            //m.slowMethod2();
            //m.slowMethod3();
            //m.slowMethod4();
            //m.slowMethod4_2();
            //m.slowMethod5();

            //task with return value
            //TaskReturnValue.callCalValAsync(10);

            //Lock Test zxw
            //new LockTestZXW().runLockTestZXW();

            //ManualResetEventSlim MSDN Demo
            //ManualResetEventSlimMSDN.MRES_SetWaitReset();
            //ManualResetEventSlimMSDN.MRES_SpinCountWaitHandle();

            //ManualResetEventSlim ProducerConsumer  
            ManualResetEventSlim_ProducerConsumer.SimpleProducerConsumer();

            Console.WriteLine("In Main!");
            Console.ReadKey();
        }
    }
}
