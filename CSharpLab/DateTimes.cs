using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CSharpLab
{
    public class DateTimes
    {
        static DateTime Time1 = new DateTime(2015, 12, 1, 23, 1, 59, 100);
        static DateTime Now = DateTime.Now;
        public static void Test1()
        {
            Console.WriteLine("Time1: {0}\nDate: {1}\nShortDateString: {2}\nLongDateString: {3}\nShortTimeString: {4}\nLongTimeString: {5}\nCustom1: {6}\nCustom2: {7}\n",
                Time1,
                Time1.Date,
                Time1.ToShortDateString(),
                Time1.ToLongDateString(),
                Time1.ToShortTimeString(),
                Time1.ToLongTimeString(),
                Time1.ToString("yyyy年MM月dd日 HH:mm:ss.fff"),
                Time1.ToString("yy-MM-dd hh:mm"));

            Console.WriteLine("{0}, {1}, {2}, {3}, {4}\n", Time1 > Now, Time1.Date > Now.Date, Time1.Month > Now.Month, Time1.Day > Now.Day, Time1.Hour > Now.Hour);//F,F,T,F,T

            DateTime datetime1 = new DateTime(2017, 3, 9, 15, 23, 32, 900);
            DateTime datetime2 = new DateTime(2017, 3, 6, 14, 23, 30, 800);
            Console.WriteLine(datetime1);
            Console.WriteLine(datetime2);
            Console.WriteLine("相减结果：" + (datetime1 - datetime2));
            Console.WriteLine("相差刻度：" + (datetime1 - datetime2).Ticks);
            Console.WriteLine("相差毫秒：" + (datetime1 - datetime2).Ticks / 10000);//两时刻相差的毫秒数
            Console.WriteLine("相差秒钟：" + (double)(datetime1 - datetime2).Ticks / 10000 / 1000);//两时刻相差的秒数
            Console.WriteLine("相差分钟：" + (double)(datetime1 - datetime2).Ticks / 10000 / 1000 / 60);//两时刻相差的分钟数
            Console.WriteLine("相差时数：" + (double)(datetime1 - datetime2).Ticks / 10000 / 1000 / 60 / 60);//两时刻相差的小时数
            Console.WriteLine("相差天数：" + (double)(datetime1 - datetime2).Ticks / 10000 / 1000 / 60 / 60 / 24);//两时刻相差的天数
            Console.WriteLine("相差天数：" + (datetime1 - datetime2).Ticks / 10000 / 1000 / 60 / 60 / 24);//两时刻相差的天数（约等）
            //===============================================
            timer.Elapsed += new ElapsedEventHandler(ShowTime);
            //timer.Start();//开始倒计时
        }
        static System.Timers.Timer timer = new System.Timers.Timer(1000);//1秒定时器        
        static void ShowTime(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime.ToLongTimeString());
            //timer.Stop();
        }
    }
}
