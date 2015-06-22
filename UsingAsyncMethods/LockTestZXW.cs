using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
///xwzhu 150622@home
namespace UsingAsyncMethods
{
    
    class LockTestZXW

    {
        private static int i = 0;

        //Can not guarantee get set same value!
        private void SetAndRead(string name)
        {
            int temp = (new Random()).Next(500,1000);
            Console.WriteLine(name + " set value: " + temp);
            LockTestZXW.i = temp;
            Thread.Sleep(temp * 3);
            Console.WriteLine(name + " get value: " + LockTestZXW.i);
        }

        private object myLock = new object();
        //with lock, guarantee get set same value!
        private void SetAndReadLock(string name)
        {
            lock (myLock)
            {
                int temp = (new Random()).Next(500, 1000);
                Console.WriteLine(name + " set value: " + temp);
                LockTestZXW.i = temp;
                Thread.Sleep(temp * 3);
                Console.WriteLine(name + " get value: " + LockTestZXW.i);
            }
        }

        public void runLockTestZXW(){
            LockTestZXW lt = new LockTestZXW();

            //without lock
            //Task.Run(() => lt.SetAndRead("a"));
            //Task.Run(() => lt.SetAndRead("b"));
            //Task.Run(() => lt.SetAndRead("c"));
            //Task.Run(() => lt.SetAndRead("d"));
            //Task.Run(() => lt.SetAndRead("e"));
            //Task.Run(() => lt.SetAndRead("f"));
            //Task.Run(() => lt.SetAndRead("g"));
            //Task.Run(() => lt.SetAndRead("h"));
            //Task.Run(() => lt.SetAndRead("i"));
            //Task.Run(() => lt.SetAndRead("j"));
            //Task.Run(() => lt.SetAndRead("k"));

            //with lock
            Task.Run(() => lt.SetAndReadLock("a"));
            Task.Run(() => lt.SetAndReadLock("b"));
            Task.Run(() => lt.SetAndReadLock("c"));
            Task.Run(() => lt.SetAndReadLock("d"));
            Task.Run(() => lt.SetAndReadLock("e"));
            Task.Run(() => lt.SetAndReadLock("f"));
            Task.Run(() => lt.SetAndReadLock("g"));
            Task.Run(() => lt.SetAndReadLock("h"));
            Task.Run(() => lt.SetAndReadLock("i"));
            Task.Run(() => lt.SetAndReadLock("j"));
            Task.Run(() => lt.SetAndReadLock("k"));

        }
    }
}
