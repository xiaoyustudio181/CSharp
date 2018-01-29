using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Radix
    {
        public static void Test1()
        {
            int num = 65;
            Console.WriteLine("十进制数(DEC,decimal)：" + num);//65
            Console.WriteLine("二进制数(BIN,binary)：" + Convert.ToString(num, 2));//1000001
            Console.WriteLine("八进制数(OCT,octonary)：" + Convert.ToString(num, 8));//101
            Console.WriteLine("十六进制数(HEX,hexadecimal)：" + Convert.ToString(num, 16));//41
            Console.WriteLine("ASCII字符(ASCII)：" + Encoding.ASCII.GetString(new byte[] { (byte)num }));
            Console.WriteLine();

            num = 0101;
            Console.WriteLine("输出八进制数：" + num + "（未转化）");
            num = 0x41;
            Console.WriteLine("输出十六进制数：" + num + "（自动转化为十进制）");
            Console.WriteLine();

            string str = "Sophie", temp;
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            foreach (byte each in bytes)
            {
                temp = Encoding.ASCII.GetString(new byte[] { (byte)each });
                Console.Write("{0}-{1} | ", each, temp);
            }
        }
    }
}
