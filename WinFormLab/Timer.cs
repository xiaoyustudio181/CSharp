using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyFrame;
using System.Threading;
namespace WinFormLab
{
    public partial class Timer : Form
    {
        MyWinForm FM;
        System.Windows.Forms.Timer timer;
        System.Windows.Forms.Timer timer2;
        MyScanGun gun;
        public Timer()
        {
            InitializeComponent();
            FM = new MyWinForm(this);
            FM.SetDefaultStyle("Timer");
            timer = new System.Windows.Forms.Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 10;
            timer.Enabled = true;
            //timer.Start();
            //============================
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1;
            timer2.Tick += timer2_Tick;
            gun = new MyScanGun("COM3", "9600");//根据实际情况设置
            if (!gun.Open()) return;
            timer2.Start();
        }
        string result;
        void timer2_Tick(object sender, EventArgs e)//计时器方法循环读取扫描枪数据
        {
            result=gun.GetScanResult();
            if (result == "X") return;//扫描枪内无数据
            label3.Text = result;

        }
        void timer_Tick(object sender, EventArgs e)//计时器方法（循环执行，除非.Stop()或终止整个进程）
        {
            //label1.Text = DateTime.Now.Second.ToString();
            //label1.Text = DateTime.Now.Millisecond.ToString();
            label1.Text = DateTime.Now.ToLongTimeString();
            //timer.Stop();
        }
        Thread thread;//比起Timer，线程循环极耗费CPU
        private void Timer_Load(object sender, EventArgs e)
        {
            DateTime now;
            thread = new Thread(() => {
                while (true)
                {
                    now = DateTime.Now;
                    this.Invoke(new EventHandler(delegate { //线程内操作控件
                        label2.Text = now.ToLongTimeString(); 
                    }));
                }
            });
            thread.Start();
        }

        private void Timer_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }
    }
}
