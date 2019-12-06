using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvariantAndContravariant
{
    class Father{}
    class Son : Father{}

    interface IConvariantInterface<out T>{}
    interface IContravariantInterface<in T>{}

    class Program
    {
        static void Main(string[] args)
        {
            //协变
            IConvariantInterface<Father> conFather = null;
            IConvariantInterface<Son> conSon = null;
            conFather = conSon;
            //conSon = conFather;  //compile error

            //逆变
            IContravariantInterface<Father> contraFather = null;
            IContravariantInterface<Son> contraSon = null;
            //contraFather = contraSon;  //compile error: Cannot implicitly convert type 'ConvariantAndContravariant.IContravariantInterface<ConvariantAndContravariant.Son>' to 'ConvariantAndContravariant.IContravariantInterface<ConvariantAndContravariant.Father>'. An explicit conversion exists (are you missing a cast?)
            contraSon = contraFather;

            /* List in C# is invariant – List<T> */
            List<Father> fList = new List<Father>();
            List<Son> sList = new List<Son>();
            //fList = sList;    //compile error
            //sList = fList;    //compile error

            /* Legal as IEnumerable<out T> is convariant*/
            IEnumerable<Father> fIE = sList;
        }
    }
}
