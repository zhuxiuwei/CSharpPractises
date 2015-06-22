using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication1
{
    class Program
    {
        public const double PI = 3.14159265358979323846;
        static int adjas = 1;
     

        static void hah(int i)
        {
            Console.WriteLine(i);
        }

        static void hah(int ? i)
        {
            Console.WriteLine(i);
            Console.WriteLine(i == null);
            Console.WriteLine(i.HasValue);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Program.PI);
            Program p = new Program();

            //Anonymous class
            var myAnonymousObject = new { Name = "John", Age = 47 };
            var anotherAnonymousObject = new { Name = "Michael", Age = 27 };
            anotherAnonymousObject = myAnonymousObject;
            Console.WriteLine(anotherAnonymousObject.Name + "," + anotherAnonymousObject.Age);

            int? nullAbleInt = null;
            Program.hah(nullAbleInt + 1);

            Season? s = null;
            s = Season.Spring;
            printSeason(s);

            
            var implicitlyTypedArray = new[]{1,2,3};
            foreach (int i in implicitlyTypedArray)
            {
                Console.WriteLine(i);
            }

            Father father = new Son();
            father.f();
            father.X += 100;
            Console.WriteLine(father.X);

            //test timer
            Console.WriteLine("Timer test!");
            Timer timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            timer.Enabled = true;

            Console.WriteLine(Guid.NewGuid().ToString());
            
            Console.ReadLine();
        }

        public static void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

        enum Season { Spring, Summer, Fall, Winter }
        static void printSeason(Season s)
        {
            Console.WriteLine(Season.Fall);
        }
        static void printSeason(Season? s)
        {
            Console.WriteLine(Season.Fall);
        }
    }

    class Father
    {
        private int _x;
        public virtual void f()
        {
            Console.WriteLine("father");
        }

        public virtual int X
        {
            get { return this._x; }
            set {this._x = value;}
        }
    }
    class Son : Father
    {
       
         public sealed override void f()
        {
            Console.WriteLine("son");
        }
    }


}
