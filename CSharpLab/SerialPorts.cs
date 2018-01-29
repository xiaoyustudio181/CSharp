using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class SerialPorts
    {
        public static void Test1()
        {
            //本实验使用Virtual Serial Port Driver添加的一对虚拟串口。名字分别为COM01,COM02。
            SerialPort port1 = new SerialPort();
            SerialPort port2 = new SerialPort();
            Console.WriteLine("通信端口：" + port1.PortName);//默认值COM1
            Console.WriteLine("是否已打开：" + port1.IsOpen);//默认值COM1
            Console.WriteLine("串行波特率：" + port1.BaudRate);//默认值9600
            Console.WriteLine("每个字节的标准数据位长度：" + port1.DataBits);//默认值8
            Console.WriteLine("每个字节的标准停止位数：" + port1.StopBits);//默认值One
            Console.WriteLine("串口通信是否启动请求发送(RTS)信号：" + port1.RtsEnable);//RTS)信号，默认值false
            Console.WriteLine("读取操作未完成时发生超时之前的毫秒数：" + port1.ReadTimeout);//默认值-1
            Console.WriteLine("输入缓冲区大小：" + port1.ReadBufferSize);//默认值4096
            Console.WriteLine("输出缓冲区大小：" + port1.WriteBufferSize);//默认值2048
            if (!port1.IsOpen) port1.Close();//未打开时，关闭无异常
            port1.Close();//关闭两次无异常
            try//尝试打开串口
            {
                port1.PortName = "COM01";
                port2.PortName = "COM02";
                port1.Open();
                port2.Open();
                port1.DiscardOutBuffer();//清空发送缓冲区
                port2.DiscardOutBuffer();
                port1.DiscardInBuffer();//清空接收缓冲区
                port2.DiscardInBuffer();
                Console.WriteLine("串口打开成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (port1.IsOpen && port2.IsOpen)
            {
                port1.Write("Sophie");//串口1写数据到串口2
                Console.WriteLine("COM01 发送缓冲区数据的字节数：" + port1.BytesToWrite);
                Console.WriteLine("COM02 发送缓冲区数据的字节数: " + port2.BytesToWrite);
                Console.WriteLine("COM01 接收缓冲区数据的字节数: " + port1.BytesToRead);
                Console.WriteLine("COM02 接收缓冲区数据的字节数: " + port2.BytesToRead);
                char[] result = new char[6];
                port2.Read(result, 0, 6);//串口2读数据
                /*.Read(byte(char)[] buffer, int offset, int count)
                 * buffer：将输入写入到其中的字节(或字符)数组；
                 * offset：缓冲区数组中开始写入的偏移量(即开始写入的数组索引)；
                 * count：要读取的字节数。
                 * 返回值：读取的字节数（int类型）*/
                Console.WriteLine("读取到的数据：");//输出读取的数据
                foreach (byte each in result)
                    Console.Write(each + " ");//每个字符的十进制ASCII码
                Console.WriteLine("\n");

                //==========================================

                port2.Write(result, 0, 6);//串口2写数据到串口1
                Console.WriteLine("COM01 发送缓冲区数据的字节数：" + port1.BytesToWrite);
                Console.WriteLine("COM02 发送缓冲区数据的字节数: " + port2.BytesToWrite);
                Console.WriteLine("COM01 接收缓冲区数据的字节数: " + port1.BytesToRead);
                Console.WriteLine("COM02 接收缓冲区数据的字节数: " + port2.BytesToRead);
                result = new char[6];
                port1.Read(result, 0, 6);//串口1读数据
                Console.Write("读取到的数据：");
                Console.WriteLine(result);
                byte[] bytes = Encoding.ASCII.GetBytes(result);
                foreach (byte each in bytes)
                    Console.Write(each + " ");
                Console.WriteLine();
                Console.WriteLine(Encoding.ASCII.GetString(bytes));

                port1.Close();
                port2.Close();
            }
        }
    }
}
