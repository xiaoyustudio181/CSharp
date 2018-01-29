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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace WinFormLab
{
    public partial class BindBox : Form
    {
        MyWinForm FM;
        string AccessPath = new DirectoryInfo(INI.Root).Parent.Parent.Parent.FullName + @"\Northwind.mdb";
        OleDbConnection conA;
        SqlConnection conS;

        MyComboBox CBB;
        MyListBox LB;
        MyCheckedListBox CLB;
        public BindBox()
        {
            InitializeComponent();
            FM = new MyWinForm(this);
            FM.SetDefaultStyle("选择");
            conA = MyDB.GetAccessConnection(AccessPath);
            conS = MyDB.GetSqlServerConnection("127.0.0.1", "111", "Northwind");

            CBB = new MyComboBox(comboBox1, "select CustomerID,ContactName from Customers", "ContactName", "CustomerID", conA);
            LB = new MyListBox(listBox1, "select CustomerID,ContactName from Customers", "CustomerID", "ContactName", conA);
            CLB=new MyCheckedListBox(checkedListBox1, "select CustomerID,ContactName from Customers", "ContactName", "CustomerID", conA);
            //checkedListBox1.Items.Add("selection1", true);//添加选中状态的项
        }
        private void button1_Click(object sender, EventArgs e)//重载ComboBox
        {
            //CBB.Reload();//一般重载
            CBB.Reset("select ContactName,CompanyName from Customers", "CompanyName", "ContactName");//更换SQL重载
        }
        private void button2_Click(object sender, EventArgs e)//重载ListBox
        {
            //LB.Reload();//一般重载
            LB.Reset("select ContactName,CompanyName from Customers", "CompanyName", "ContactName");//更换SQL重载
        }
        private void button3_Click(object sender, EventArgs e)//重载CheckedListBox
        {
            //CLB.Reload();//一般重载
            CLB.Reset("select ContactName,CompanyName from Customers", "CompanyName", "ContactName");//更换SQL重载
        }
    }
}
