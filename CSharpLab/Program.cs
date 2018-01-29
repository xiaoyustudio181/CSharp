using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MyFrame;
#region 我的 Visual Stuido 设置（SQLServer同）
/*工具——选项——
 * 常规——颜色主题：浅色。
 * 环境——字体和颜色：纯文本——Segoe UI Semibold，粗体，大小16。行号——黑色。
 * 文本编辑器——C#——常规：自动换行，行号。*/
#endregion
namespace CSharpLab
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (10)
            {
                case 1: Variables.Test1(); break;//值类型变量：整型、浮点型、布尔型、字符型
                case 2: Variables.Test2(); break;//引用类型变量：数组型、枚举类型、集合类型、结构类型、特殊类型、类型转换
                case 3: Variables.Test3(); break;//引用类型变量：对象
                case 4: Variables.Test4(); break;//引用类型变量：接口、委托、事件
                case 5: Logics.Test1(); break;//逻辑运算
                case 6: OperateFile.Test1(); break;//文件操作
                case 7: TryCatch.Test1(); break;//异常处理
                case 8: Threads.Test1(); break;//线程
                case 9: SerialPorts.Test1(); break;//串行端口控制
                case 10: DateTimes.Test1(); break;//日期时间操作
                case 11: Json.Test1(); break;//Json序列化对象数据
                case 12: RunProcess.Test1(); break;//运行程序
                case 13: Radix.Test1(); break;//进制计算
                case 14: Bitwise.Test1(); break;//位运算
                case 15: Regedit.Test1(); break;//注册表操作
                case 16: OperateDB.Access(); break;//数据库操作
                default: OtherTests(); break;
            }
            Console.ReadKey();
        }
        static void OtherTests()
        {

        }
    }
}