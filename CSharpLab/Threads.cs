using MyFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Threads
    {
        public static void Test1()
        {
            #region 线程
            /*一个正在运行的程序在操作系统中被视为一个进程，一个进程可以包括一个或多个线程。
             * 线程是操作系统分配处理器时间的基本单元，在进程中可以有多个线程同时执行代码。
             * 线程上下文，包括为了使线程在其宿主进程地址中无缝地继续执行所需的所有信息，包括线程的CPU寄存器组和堆栈。*/
            #endregion
            T1();
            //T2();
            //T3();
            //T4();
        }
        static void T1()
        {
            /*/
            Method1();//一般执行
            MyDialog.Msg("完成");
            /*/
            Thread thread1 = new Thread(new ThreadStart(Method1));//定义线程的方法
            thread1.Start();//线程执行
            MyDialog.Msg("完成");
            thread1.Abort();//只有开始过的线程才能终止，即便已经运行结束
            thread1.Abort();//可再次终止，不会报错
            //*/
        }
        static void T2()
        {
            /*/
            Method2("Sophie");//一般执行
            MyDialog.Msg("完成");
            /*/
            Thread thread2 = new Thread(new ParameterizedThreadStart(Method2));//定义线程（有参数）
            thread2.Start("Sophie");//线程执行（有参数）
            MyDialog.Msg("完成");
            thread2.Abort();
            //*/
        }
        static void T3()
        {
            Thread thread3 = new Thread(() => //定义线程，匿名线程方法
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine("Method 3 : " + i++);
                    Thread.Sleep(100);//增加延时
                    if (i == 50000) break;
                }
            });
            thread3.Start();
            MyDialog.Msg("完成");
            thread3.Abort();
        }
        static void T4()
        {
            Thread thread4 = new Thread((object obj) => //定义线程，匿名线程方法（有参数）
            {
                for (int i = 0; i < 50000; i++)
                    Console.WriteLine("Method 4 : {0} - {1}", obj, i);
            });
            thread4.Start("Sienna");
            MyDialog.Msg("完成");
            if (thread4.IsAlive) thread4.Abort();
        }
        static void Method1()
        {
            for (int i = 0; i < 5000; i++)
            {
                if (i == 1000) MyDialog.Msg("已到1000");//运行到这里，线程会停止并等待对话框结束
                Console.WriteLine("Method 1 : " + i);
            }
        }
        static void Method2(object param)
        {
            for (int i = 0; i < 50000; i++)
                Console.WriteLine("Method 2: {0} - {1}", param, i);
        }
    }
}
