using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class RunProcess
    {
        static Process process = new Process();
        public static void Test1()
        {
            //T1();
            T2();
        }
        static void T1()
        {
            process.StartInfo.FileName = Environment.CurrentDirectory;//+ @"\Record.txt";
            process.Start();
            try { process.Kill(); }//此句不能关闭文件夹；仅能使用一次，使用之后HasExited属性值变为false
            catch (Exception ex) { Console.WriteLine("异常：{0}", ex.Message); }
            process.StartInfo.FileName = "firewall.cpl";//notepad
            process.Start();
        }
        static void T2()
        {
            process.StartInfo.FileName = "cmd";//若不设置下面五项，则直接启动cmd
            process.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.CreateNoWindow = true;//不显示程序窗口
            process.Start();
            process.StandardInput.WriteLine("ipconfig" + "&exit");//若不执行exit，后面调用ReadToEnd()会假死。
            process.StandardInput.AutoFlush = true;
            #region 批处理命令符号
            //“&”，前面一个命令不管是否执行成功都执行后面；
            // “&&”，必须前一个命令执行成功才会执行后面；
            // “||”，必须前一个命令执行失败才会执行后面。
            #endregion
            StreamReader reader = process.StandardOutput;
            string line = "";
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                if (!line.Equals(""))
                    Console.WriteLine(line);
            }
            process.WaitForExit();//等待程序执行完退出进程
            process.Close();
        }
    }
}
