using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Logics
    {
        public static void Test1()
        {
            //提示：打断点分步查看细节。
            Value();
            //IfElse();
            //Switch();
            //While();
            //For();
            //Foreach();
        }
        static void Value()//赋值运算
        {
            int a = 5;
            Console.WriteLine(a++ + 3);//运算后自增
            a = 5;
            Console.WriteLine(++a + 3);//自增后运算
            a = 5;
            a += 2;//a=a+2
            Console.WriteLine(a);
            a = 5;
            a -= 2;
            Console.WriteLine(a);
            a = 5;
            a *= 5;
            Console.WriteLine(a);
            a = 5;
            a /= 5;
            a = 5;
            Console.WriteLine(a);
            Console.WriteLine("{0}, {1}, {2}", a == 5, a != 5, a > 4);
            Console.WriteLine(a > 4 ? "a>4" : "a<=4");//三元运算
            int int1 = a > 4 ? 1 : 0;//若a>4则将1复制给int1，否则将0赋值给in1
            Console.WriteLine();
        }
        static void IfElse()
        {
            int a = 5;
            if (a < 6)
            {
                if (a == 5) Console.WriteLine("a等于5");
                else Console.WriteLine("a不等于5");
                if (a == 5) if (a > 4) Console.WriteLine("a=5 and a>4");
                if (a == 5 && a > 4) Console.WriteLine("a=5 and a>4");
                if (a < 1 || a >= 4) Console.WriteLine("a<1 or a>=4");
            }
            else
            {
                //...
            }
            Console.WriteLine();
        }
        static void Switch()
        {
            int a = 5;
            switch (a)
            {
                case 1: Console.WriteLine("值为1的情况"); break;
                case 2: Console.WriteLine("值为2的情况"); break;
                case 3: //注意此处没有语句与break，当值为3时与值为4,5的情况相同
                case 4:
                case 5: Console.WriteLine("值为5的情况"); break;
                default: break;
            }
        }
        static void While()
        {
            int a = 5;
            while (a < 10)
            {
                Console.WriteLine(a++);
            }
            Console.WriteLine();
            //==============
            a = 5;
            while (a < 10)
            {
                a++;
                if (a == 8) continue;//跳过本次循环，本次后面的代码不运行
                Console.WriteLine(a);
            }
            Console.WriteLine();
            //==============
            a = 5;
            while (a < 10)
            {
                a++;
                if (a == 8) break;//终止所有循环
                Console.WriteLine(a);
            }
            Console.WriteLine();
            //==============
            a = 5;
            do
            {
                Console.WriteLine(a++);
            }
            while (a < 10);
        }
        static void For()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                //continue,break的用法同while
            }
            Console.WriteLine();
        }
        static void Foreach()
        {
            foreach (int each in new int[] { 3, 2, 1 })
            {
                Console.WriteLine(each);
                //continue,break的用法同while
            }
            Console.WriteLine();
        }
    }
}
