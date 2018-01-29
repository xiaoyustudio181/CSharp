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
namespace WinFormLab
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyError.NewRecord();

            if (textBox1.Text == "") 
                MyError.Set(textBox1, "!!!");
            else MyError.Clear(textBox1);

            if (textBox2.Text == "")
                MyError.Set(textBox2, "!!!");
            else MyError.Clear(textBox2);

            if (MyError.CheckAll()) MyDialog.Msg("");
        }
    }
}
