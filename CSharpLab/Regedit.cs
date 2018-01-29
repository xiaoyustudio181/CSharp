using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Regedit
    {
        public static void Test1()
        {
            RegistryKey EventLabels = Registry.CurrentUser.OpenSubKey(@"AppEvents\EventLabels");
            RegistryKey sub;
            foreach (string each in EventLabels.GetSubKeyNames())
            {
                sub = EventLabels.OpenSubKey(each);
                Console.WriteLine(each);
                foreach (string each2 in sub.GetValueNames())
                {
                    Console.WriteLine("\t" + each2 + " : " + sub.GetValue(each2));
                }
            }
            Console.WriteLine();
            //================================写
            RegistryKey CurrentConfig = Registry.CurrentConfig;
            //RegistryKey shea = CurrentConfig.CreateSubKey("SheaZheng");//非管理员需要权限
            //shea.SetValue("", "Shea Zheng", RegistryValueKind.String);//值名称为空是设置本项的默认值
            //shea.SetValue("Game", "Biohazard", RegistryValueKind.String);
            //RegistryKey other = CurrentConfig.CreateSubKey(@"SheaZheng\Other");
            //other.SetValue("Configuration", "6", RegistryValueKind.String);
            //================================读
            //RegistryKey shea2 = CurrentConfig.OpenSubKey(@"\SheaZheng");
            //object result = shea2.GetValue("Game", "Not Found");
            //object result = Registry.GetValue("HKEY_CURRENT_CONFIG\\SheaZheng", "Game", "Not Found");
            //if (result == null)
            //    Console.WriteLine("Nothing......");
            //else
            //    Console.WriteLine(result);
        }
    }
}
