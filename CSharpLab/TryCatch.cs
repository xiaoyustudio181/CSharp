using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class TryCatch
    {
        public static void Test1()
        {
            #region 异常处理
            /*System.Exception类是异常类的基类。
             * 异常处理结构：try-catch，try-finally，try-catch-finally。
             * try指明可能出现异常的代码；
             * catch指明对所出现的异常的处理，没有异常则不执行；
             * finally是无论是否发生异常均要执行，用于清理资源或执行要在try末尾的操作，没有必要则可不使用。
             * throw后跟表达式可抛出Exception异常类或其派生类型，其后不跟表达式则表示将异常再次抛出。*/
            #endregion
            try { Convert.ToInt32("a"); }
            catch { Console.WriteLine("异常情况发生！"); }
            //====
            try { Convert.ToInt32("a"); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //====
            try { throw new Exception("丧尸闯入。"); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //====
            try { throw new MyException("Wrong age.", 160); }
            catch (MyException ex) { Console.WriteLine(ex.Message); }
        }
    }
    class MyException : ApplicationException
    {
        new public string Message { get; set; }//隐藏父类的Message字段
        public MyException(string Msg, int Age)
        {
            Message = Msg;
            if (Age < 0) 
                Console.WriteLine("Age<0 ?");
            else if (Age > 150) 
                Console.WriteLine("Age>150 ?");
        }
    }

}
