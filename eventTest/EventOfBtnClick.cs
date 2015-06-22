using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventTest
{
    public class ButtonEventArgs: EventArgs{
        public DateTime clickTime;
        public string clickUser;
    }

    public class MyButton   //publisher
    {
        public delegate void BtnClickDelegate(object sender, ButtonEventArgs e);
        public event BtnClickDelegate OnClick;
        public BtnClickDelegate btnDelegete;    //只是为了演示委托和事件的区别。

        public void Click()
        {
            if (OnClick != null)
            {
                OnClick(this, new ButtonEventArgs() { clickTime = new DateTime(), clickUser = "xiuzhu" });
            }
            else
            {
                Console.WriteLine("No register!");
                Console.ReadLine();
            }
        }

        
    }
    public class MyDisplayForm
    {
        public MyButton btn;

        public MyDisplayForm()  //subscriber
        {
            this.btn = new MyButton();
            btn.OnClick += new MyButton.BtnClickDelegate(btn_OnClick);
            btn.btnDelegete += btn_OnClick;
        }
      
        public void btn_OnClick(object sender, ButtonEventArgs e)   //user's business logic is here.
        {
            Console.WriteLine("Sender info: " + sender.GetType().ToString());
            Console.WriteLine("Click time: " + e.clickTime);
            Console.WriteLine("Click user: " + e.clickUser);
            Console.ReadLine();
        }
        public void Click2()
        {
            //演示委托和事件的区别。
            ////可以在任何地方调用delegate，但不能调用event。
            btn.btnDelegete(this, new ButtonEventArgs() { clickTime = new DateTime(), clickUser = "xiuzhu2" });     
        }

    }

    public class Program
    {
        public static void Main(String[] args)
        {
            MyDisplayForm form = new MyDisplayForm();
            form.btn.Click();   //pubisher states change will trigger subscriber metheod call

            form.Click2();
        }
    }
}
