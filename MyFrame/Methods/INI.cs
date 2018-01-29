using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyFrame
{
    public class INI
    {
        static string INIPath;
        static StringBuilder result;
        static string temp;
        static INI()
        {
            INIPath = Root + @"\Config.ini";
            result = new StringBuilder();
        }
        //示例
        public static void SetXXX(object Value)
        {
            Set("Basic", "XXX", Value);
        }
        public static string GetXXX()
        {
            return Get("Basic", "XXX");
        }
        #region 其他封装
        public static string Root//根目录
        {
            get { return Environment.CurrentDirectory; }
        }
        public static void OpenRoot()//打开根目录
        {
            MyProcess.Run(Root);
        }
        public static void OpenConfig()//打开配置文件
        {
            MyProcess.Run(INIPath);
        }
        #endregion
        #region 二次封装
        public static void Set(string section, string key, object value)//写入配置
        {
            WritePrivateProfileString(section, key, value.ToString(), INIPath);
        }
        public static string Get(string section, string key)//读取配置
        {
            GetPrivateProfileString(section, key, "Not Found", result, 34, INIPath);//最大只能读34位字符
            temp = result.ToString();
            if (temp.Equals("") || temp.Equals("Not Found"))
            {
                if (temp == "Not Found")
                    Console.WriteLine("意外：请检查配置文件是否存在，或配置中是否存在指定的键。[{0}]-{1}", section, key);
                if (temp == "")
                    Console.WriteLine("意外：配置文件中的指定键值为空。[{0}]-{1}", section, key);
                temp = "ERROR";//两种情况的默认返回值
            }
            return temp;
        }
        #endregion
        #region 方法引用
        /// <summary>
        /// 向指定配置文件的指定块写入指定键的值。若配置文件不存在，则创建文件并添加指定的键。
        /// </summary>
        /// <param name="section">块</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="INIPath">路径</param>
        /// <returns>非零表示成功，零表示失败（类型可以是任意整数类型，除了sbyte）</returns>
        [DllImport("kernel32")]
        static extern byte WritePrivateProfileString(string section, string key, string value, string INIPath);//写入配置
        /// <summary>
        /// 从指定配置文件的指定块读取指定键的值。
        /// </summary>
        /// <param name="section">块</param>
        /// <param name="key">键</param>
        /// <param name="default_value">默认值</param>
        /// <param name="result">结果（引用类型）</param>
        /// <param name="size">目的缓存器的大小</param>
        /// <param name="INIPath">路径</param>
        /// <returns>string的长度（类型可以是任意整数类型）</returns>
        [DllImport("kernel32")]
        static extern sbyte GetPrivateProfileString(string section, string key, string default_value, StringBuilder result, int size, string INIPath);//读取配置
        #endregion
    }

}
