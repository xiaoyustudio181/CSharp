using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyFrame;
using System.Data.OleDb;
using System.IO;
namespace DotNetBarLab
{
    public partial class Form1 : Form
    {
        MyWinForm FM;
        OleDbConnection con;
        OleDbCommand cmd=new OleDbCommand();
        OleDbDataAdapter adapter;
        DataSet dataset=new DataSet();
        DataTable table;
        DataRow row;
        string AccessPath = new DirectoryInfo(INI.Root).Parent.Parent.Parent.FullName + @"\Northwind.mdb";
        public Form1()
        {
            InitializeComponent();
            FM = new MyWinForm(this);
            FM.SetDefaultStyle("Test");
            con = MyDB.GetAccessConnection(AccessPath);
            cmd.Connection = con;
            cmd.CommandText = "select EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称 from Employees";
            adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dataset, "Tab");
            table = dataset.Tables["Tab"];

            BindDataToGVX();
            BindDataToComboX();
        }
        void BindDataToGVX()
        {
            dataGridViewX1.Font = new Font("华文中宋", 12.5f, FontStyle.Bold);
            dataGridViewX1.RowTemplate.Height = 35;
            //dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.Columns.Add("", "姓名1");
            dataGridViewX1.Columns.Add("", "姓名1");
            dataGridViewX1.Columns.Add("", "姓名1");
            dataGridViewX1.Columns[0].DataPropertyName = "ID";
            dataGridViewX1.Columns[1].DataPropertyName = "姓名";
            dataGridViewX1.Columns[2].DataPropertyName = "尊称";
            dataGridViewX1.DataSource = table;

            //dataGridViewX1.Columns[0].Width = 0;
            dataGridViewX1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewX1.AllowUserToAddRows = false;//是否允许手动添加新行
            dataGridViewX1.AllowUserToResizeColumns = false;//是否允许调整列宽
            dataGridViewX1.AllowUserToResizeRows = false;//是否允许调整行高
            dataGridViewX1.ReadOnly = true;//是否允许手动编辑
            //设置文字居中
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();//格子样式
            CellStyle.Alignment = true ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.NotSet;//内容是否居中
            dataGridViewX1.DefaultCellStyle = CellStyle;//默认格子样式
            dataGridViewX1.ColumnHeadersDefaultCellStyle = CellStyle;//默认表头格子样式
            dataGridViewX1.RowHeadersDefaultCellStyle = CellStyle;//默认首行格子样式
        }
        void BindDataToComboX()
        {
            comboBoxEx1.DataSource = table;
            comboBoxEx1.DisplayMember = "姓名";
            comboBoxEx1.ValueMember = "ID";
        }
    }
}
