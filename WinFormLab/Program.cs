using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormLab
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()//从这里可以改变要启动的第一个窗体。
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("输出测试的内容······");
            Application.Run(new TestForm());
            //Application.Run(new DataView());
            //Application.Run(new BindBox());
            //Application.Run(new Timer());
        }
    }
}
