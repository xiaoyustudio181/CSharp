using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyFrame;

namespace WinFormLab
{
    public partial class DataView : Form
    {
        MyWinForm FM;
        string AccessPath = new DirectoryInfo(INI.Root).Parent.Parent.Parent.FullName + @"\Northwind.mdb";
        OleDbConnection conA;
        SqlConnection conS;
        MyGridViewA GVA;
        MyGridViewS GVS;
        MyListViewA LVA;
        MyListViewS LVS;
        public DataView()
        {
            InitializeComponent();
            FM = new MyWinForm(this);
            FM.SetDefaultStyle("数据");
            conA = MyDB.GetAccessConnection(AccessPath);
            conS = MyDB.GetSqlServerConnection("127.0.0.1", "111", "Northwind");
            Catalog();
        }
        void Catalog(int page=3)
        {
            switch (page)
            {
                case 1: GVLoadA(); break;
                case 2: GVLoadS(); break;
                case 3: LVLoadA(); break;
                case 4: LVLoadS(); break;
                case 5: BindDataGridView(); break;
                case 6: CreateDataGridView(); break;
                case 7: CreateListView(); break;
                default: break;
            }
        }
        void GVLoadA()
        {
            GVA = new MyGridViewA(dataGridView1);
            //GVA.AlignCenter = true;//内容对齐方式（默认靠左），仅在SetStyle之前设置有效
            //GVA.CanEdit = true;//是否可编辑（默认不可编辑）
            //GVA.CanResizeColumns = false;//是否可调整列宽（默认可调整，自动适应时设置无效）
            //GVA.CanResizeRows = true;//是否可调整行高（默认不可调整，自动适应时设置无效）
            GVA.SetStyle();//设定样式
            //GVA.SetStyle("宋体",16f,FontStyle.Bold);//设定样式2

            GVA.SetDB(conA, "Employees");//设置数据库
            //GVA.ColumnWidths = new int[] { 50, 90, 80 };//列宽（默认自动适应）
            //GVA.RowHeight = 35;//行高（默认自动适应）
            //GVA.SetColumnSortModes(new bool[] { true, false, false });//列排序功能（默认禁用）
            //GVA.RowColor = Color.AntiqueWhite;//行色（默认白色，绑定数据时无效）
            GVA.QueryFields = "EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称";//输出列控制
            //GVA.QueryFields = "BirthDate";//输出列控制
            GVA.OrderBy("EmployeeID asc");//排序字段与排序方式（默认按首列升序）
            //GVA.Where("EmployeeID>2");//条件限制（默认无条件限制）
            //GVA.ClearWhere();//清除条件限制
            //GVA.ClearOrderBy();//重置排序方式
            //GVA.UseSpecialSQL("select EmployeeID as ID from Employees");//自定义SQL语句
            //GVA.AbandonSpecialSQL();//不使用自编SQL语句并清除掉
            //分组必赋值以下3个变量，否则报错（若PerPage为0或未赋值则不分页）
            GVA.PerPage = 5;//每页显示条数
            GVA.NowPage = 1;//当前页码
            GVA.KeyColumn = "EmployeeID";//主键列
            //*/ 
            GVA.ShowData();//代码生成数据
            /*/
            GVA.BindData();//控件绑定数据
            //*/
            Console.WriteLine("载入条数：" + GVA.CountRows);//获取总行数（显示数据后更新）
            numericUpDown1.Maximum = (decimal)GVA.TotalPages;
        }
        void GVLoadS()
        {
            GVS = new MyGridViewS(dataGridView1);
            //GVS.AlignCenter = true;//内容对齐方式（默认靠左），仅在SetStyle之前设置有效
            //GVS.CanEdit = true;//是否可编辑（默认不可编辑）
            //GVS.CanResizeColumns = false;//是否可调整列宽（默认可调整，自动适应时设置无效）
            //GVS.CanResizeRows = true;//是否可调整行高（默认不可调整，自动适应时设置无效）
            GVS.SetStyle();//设定样式
            //GVS.SetStyle("宋体", 16f, FontStyle.Bold);//设定样式2

            GVS.SetDB(conS, "Employees");//设置数据库
            //GVS.ColumnWidths = new int[] { 50, 90, 80 };//列宽（默认自动适应）
            //GVS.RowHeight = 35;//行高（默认自动适应）
            //GVS.SetColumnSortModes(new bool[] { true, false, false });//列排序功能（默认禁用）
            GVS.RowColor = Color.AntiqueWhite;//行色（默认白色，绑定数据时无效）
            GVS.QueryFields = "EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称";//输出列控制
            //GVS.QueryFields = "BirthDate";//输出列控制
            //GVS.OrderBy("EmployeeID desc");//排序字段与排序方式（默认按首列升序）
            //GVS.Where("EmployeeID>2");//条件限制（默认无条件限制）
            //GVS.ClearWhere();//清除条件限制
            //GVS.ClearOrderBy();//重置排序方式
            //GVS.UseSpecialSQL("select EmployeeID as ID from Employees");//自定义SQL语句
            //GVS.AbandonSpecialSQL();//不使用自编SQL语句并清除掉
            /*/ 
            GVS.ShowData();//代码生成数据
            /*/
            GVS.BindData();//控件绑定数据
            //*/
            Console.WriteLine("载入条数：" + GVS.CountRows);//获取总行数（显示数据后更新）
        }
        void LVLoadA()
        {
            LVA = new MyListViewA(listView1);
            LVA.SetDB(conA, "Employees");
            //LVA.RowColor = Color.AntiqueWhite;//行色（默认白色）
            LVA.ColumnWidths = new int[] { 0, 80, 80 };//列宽（默认80）
            //LVA.RowHeight = 35;//行高（默认25）
            //LVA.AlignCenter = true;//对齐方式（默认靠左）
            LVA.SetStyle();
            //LVA.SetStyle("宋体", 16f, FontStyle.Bold);//设定样式2

            LVA.QueryFields = "EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称";//输出列控制
            //LVA.OrderBy("EmployeeID desc");//排序字段与排序方式（默认按首列升序）
            //LVA.Where("EmployeeID>2");//条件限制（默认无条件限制）
            //LVA.ClearWhere();//清除条件限制
            //LVA.ClearOrderBy();//重置排序方式
            //LVA.UseSpecialSQL("select EmployeeID as ID,LastName+FirstName as 姓名 from Employees");//使用自编SQL语句
            //LVA.AbandonSpecialSQL();//不使用自编SQL语句并清除掉
            LVA.ShowData();
            Console.WriteLine("载入条数：" + LVA.CountRows);//获取总行数（数据显示后更新）
        }
        void LVLoadS()
        {
            LVS = new MyListViewS(listView1);
            LVS.SetDB(conS, "Employees");
            //LVS.RowColor = Color.AntiqueWhite;//行色（默认白色）
            LVS.ColumnWidths = new int[] { 0, 80, 100 };//列宽（默认80）
            //LVS.RowHeight = 35;//行高（默认25）
            //LVS.AlignCenter = true;//对齐方式（默认靠左）
            LVS.SetStyle();
            //LVS.SetStyle("宋体", 16f, FontStyle.Bold);//设定样式2

            LVS.QueryFields = "EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称";//输出列控制
            //LVS.OrderBy("EmployeeID desc");//排序字段与排序方式（默认按首列升序）
            //LVS.Where("EmployeeID>2");//条件限制（默认无条件限制）
            //LVS.ClearWhere();//清除条件限制
            //LVS.ClearOrderBy();//重置排序方式
            //LVS.UseSpecialSQL("select EmployeeID as ID,LastName+FirstName as 姓名 from Employees");//使用自编SQL语句
            //LVS.AbandonSpecialSQL();//不使用自编SQL语句并清除掉
            LVS.ShowData(true);
            Console.WriteLine("载入条数："+LVS.CountRows);//获取总行数（数据显示后更新）
        }
        private void Form3_DragDrop(object sender, DragEventArgs e)//获取拖入文件的路径
        {
            MyDialog.Msg(FM.DragInPath);
        }
        //====死生成法
        OleDbDataAdapter adapter;
        OleDbCommand cmd=new OleDbCommand();
        DataSet dataset = new DataSet();
        DataTable table;
        void BindDataGridView()//GridView死绑定
        {
            cmd.Connection = conA;
            adapter = new OleDbDataAdapter(cmd);
            cmd.CommandText = "select EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称 from Employees";
            adapter.Fill(dataset, "ViewBind");
            dataGridView1.Columns.Clear();//自动生成匹配列数
            while (dataGridView1.Columns.Count < dataset.Tables["ViewBind"].Columns.Count)
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());//可手动为控件设置列
            dataGridView1.AutoGenerateColumns = false;//不加此句则将按数据表字段生成列
            dataGridView1.DataSource = dataset.Tables["ViewBind"];//绑定数据
            dataGridView1.Columns[0].DataPropertyName = "ID";//绑定列
            dataGridView1.Columns[1].DataPropertyName = "姓名";
            dataGridView1.Columns[2].DataPropertyName = "尊称";
            //样式设定=============================
            dataGridView1.Font = new Font("微软雅黑", 11.5F, FontStyle.Regular);//字体（加粗参数可不加）
            dataGridView1.RowTemplate.Height = 30;//行高（仅在绑定数据时有效）
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            //其他样式均在控件上手动设置
            #region 上色（绑定的数据无法上色）
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Pink;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Pink;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Pink;
                }
            }
            #endregion
            //内容居中
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = style;
            dataGridView1.ColumnHeadersDefaultCellStyle = style;
            dataGridView1.RowHeadersDefaultCellStyle = style;
        }
        void CreateDataGridView()//GridView死生成
        {
            //=====生成表头
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
            Column1.HeaderText = "ID";
            Column1.Width = 50;
            DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            Column2.HeaderText = "姓名";
            Column2.Width = 80;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;//禁用单击表头改变排序
            DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            Column3.HeaderText = "尊称";
            Column3.Width = 80;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;//禁用单击表头改变排序
            dataGridView1.Columns.Add(Column1);
            dataGridView1.Columns.Add(Column2);
            dataGridView1.Columns.Add(Column3);
            //=====填充数据
            cmd.Connection = conA;
            adapter = new OleDbDataAdapter(cmd);
            cmd.CommandText = "select EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称 from Employees";
            adapter.Fill(dataset, "ViewBind");
            table = dataset.Tables["ViewBind"];
            DataGridViewRow NewRow;
            for (int count = 0; count < table.Rows.Count; count++)
            {
                NewRow = new DataGridViewRow();
                NewRow.Height = 35;
                DataGridViewTextBoxCell Cell1 = new DataGridViewTextBoxCell();
                Cell1.Value = table.Rows[count]["ID"];
                DataGridViewTextBoxCell Cell2 = new DataGridViewTextBoxCell();
                Cell2.Value = table.Rows[count]["姓名"];
                DataGridViewTextBoxCell Cell3 = new DataGridViewTextBoxCell();
                Cell3.Value = table.Rows[count]["尊称"];
                NewRow.Cells.Add(Cell1);
                NewRow.Cells.Add(Cell2);
                NewRow.Cells.Add(Cell3);
                dataGridView1.Rows.Add(NewRow);
            }
            //=====样式设定
            dataGridView1.Font = new Font("微软雅黑", 11.5F, FontStyle.Regular);//字体（加粗参数可不加）
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            //dataGridView1.RowTemplate.Height = 40;//行高（仅在绑定列数据时有效）
            //dataGridView1.Rows.Clear();//不会清除掉表头
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//行色
            {
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.AntiqueWhite;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.AntiqueWhite;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.AntiqueWhite;
                }
            }
            //内容居中
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = style;
            dataGridView1.ColumnHeadersDefaultCellStyle = style;
            dataGridView1.RowHeadersDefaultCellStyle = style;
        }
        void CreateListView()//ListView死生成
        {
            //=====生成表头
            ColumnHeader Header0 = new ColumnHeader();//不可见首列，为了使它的后一列能居中而存在
            Header0.Text = "0";
            Header0.Width = 0;
            ColumnHeader Header1 = new ColumnHeader();
            Header1.Text = "ID";
            Header1.Width = 50;
            Header1.TextAlign = HorizontalAlignment.Center;//第一列不能居中，但可以给它前面增加一列空列，使本列成为非首列
            ColumnHeader Header2 = new ColumnHeader();
            Header2.Text = "姓名";
            Header2.Width = 80;
            Header2.TextAlign = HorizontalAlignment.Center;//内容居中
            ColumnHeader Header3 = new ColumnHeader();
            Header3.Text = "尊称";
            Header3.Width = 80;
            Header3.TextAlign = HorizontalAlignment.Center;//内容居中
            listView1.Columns.Add(Header0);
            listView1.Columns.Add(Header1);
            listView1.Columns.Add(Header2);
            listView1.Columns.Add(Header3);
            //=====填充数据
            cmd.Connection = conA;
            adapter = new OleDbDataAdapter(cmd);
            cmd.CommandText = "select EmployeeID as ID,LastName+FirstName as 姓名,TitleOfCourtesy as 尊称 from Employees";
            adapter.Fill(dataset, "ViewBind");
            table = dataset.Tables["ViewBind"];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string RowName = "Row_" + i;
                listView1.Items.Add(RowName, table.Rows[i]["ID"].ToString(), 0);//给首列赋值（后面可获取其值）
                listView1.Items[RowName].SubItems.Add(table.Rows[i]["ID"].ToString());//第二列
                listView1.Items[RowName].SubItems.Add(table.Rows[i]["姓名"].ToString());//第三列
                listView1.Items[RowName].SubItems.Add(table.Rows[i]["尊称"].ToString());//第三列
            }
            //=====样式设定
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.View = View.Details;
            listView1.Font = new Font("微软雅黑", 11.5F, FontStyle.Regular);//字体（加粗参数可不加）
            ImageList imageList1 = new ImageList();
            imageList1.ImageSize = new Size(1, 35);
            listView1.SmallImageList = imageList1;//只能通过设置ImageList的高度来设置行高
            for (int i = 0; i < listView1.Items.Count; i++)//行色
                if (i % 2 == 0) listView1.Items[i].BackColor = Color.LightGray;//AntiqueWhite,MediumAquamarine
        }

        private void dataGridView1_Click(object sender, EventArgs e)//不用cellclick事件，以免点击后跨单元格无效
        {
            if (GVA != null) GVA.GetSelectedValues();
            if (GVS != null) GVS.GetSelectedValues();

            //if (GVA != null) GVA.GetSelectedIndexes();
            //if (GVS != null) GVS.GetSelectedIndexes();
        }
        private void listView1_MouseUp(object sender, MouseEventArgs e)//不用click事件，以免点击后移动无效
        {
            if (LVA != null) LVA.GetSelectedValues();
            if (LVS != null) LVS.GetSelectedValues();

            //if (LVA != null) LVA.GetSelectedIndexes();
            //if (LVS != null) LVS.GetSelectedIndexes();
        }

        #region 页码跳转
        private void button2_Click(object sender, EventArgs e)//上一页
        {
            if (--GVA.NowPage < 1)//小于最小页码
            {
                GVA.NowPage++;
                MyDialog.Msg("已在第一页。", 2);
                return;
            }
            GVA.ShowData(true);
            numericUpDown1.Value = (decimal)GVA.NowPage;
        }
        private void button3_Click(object sender, EventArgs e)//下一页
        {
            if (++GVA.NowPage > GVA.TotalPages)//超过最大页码
            {
                GVA.NowPage--;
                MyDialog.Msg("已在最后一页。", 2);
                return;
            }
            GVA.ShowData(true);
            numericUpDown1.Value = (decimal)GVA.NowPage;
        }
        private void button1_Click(object sender, EventArgs e)//第一页
        {
            GVA.NowPage = 1;
            GVA.ShowData();
            numericUpDown1.Value = (decimal)GVA.NowPage;
        }
        private void button4_Click(object sender, EventArgs e)//最后一页
        {
            GVA.NowPage = GVA.TotalPages;
            GVA.ShowData();
            numericUpDown1.Value = (decimal)GVA.NowPage;
        }
        private void button5_Click(object sender, EventArgs e)//跳转
        {
            GVA.NowPage = (double)numericUpDown1.Value;
            GVA.ShowData();
        }
        #endregion

    }
}
