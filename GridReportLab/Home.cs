using grproLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#region 首要配置
/* 1，右击工具箱的空白部分——添加新选项卡，命名为Grid++Report；
 * 2，右击Grid++Report选项卡——选择项，点击“COM组件”选项卡，选中：
 * “Grid++Report Designer 5.6”、
 * “Grid++Report DisplayViewer 5.6”、
 * “Grid++Report PrintViewer 5.6”，
 * 点击确定。之后工具箱新选项卡中新增了这两个控件，项目引用中也自动添加了相应的引用。*/
#endregion

namespace GridReportLab
{
    public partial class Home : Form
    {
        private string root = Environment.CurrentDirectory;
        private string dbPath;
        private GridppReport Report;//固定查询报表。首次使用GridppReport对象时，右击对象名——解析——添加引用。
        private GridppReport Report2;//动态报表。
        private GridppReport Report3;//动态图形报表。
        public Home()
        {
            InitializeComponent();
            dbPath = new DirectoryInfo(root).Parent.Parent.Parent.FullName + @"\Northwind.mdb";
            SetReport();
            LoadDisplayViewer1();
            LoadPrintViewer1();
            LoadDesignViewer1();
            LoadExportType();
            //=======================
            SetReport2();
            InitializeDB();
            LoadData();
            //=======================
            SetReport3();
            LoadData2();
        }
        private void SetReport()
        {
            Report = new GridppReport();
            Report.LoadFromFile(root + @"\3e.交叉表纵向百分比.grf");
            Report.DetailGrid.Recordset.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + dbPath;
        }
        private void SetReport2()
        {
            Report2 = new GridppReport();
            Report2.LoadFromFile(root + @"\MyCustomReport.grf");
            Report2.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report2Initialize);//必要，只执行一次
            Report2.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report2FetchRecord);//必要，只执行一次
        }
        private void SetReport3()
        {
            Report3 = new GridppReport();
            Report3.LoadFromFile(root + @"\MyCustomDiagram.grf");
            Report3.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report3Initialize);
            Report3.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report3FetchRecord);
            Report3.ProcessRecord += new _IGridppReportEvents_ProcessRecordEventHandler(Report3ProcessRecord);
        }
        private void LoadDisplayViewer1()
        {
            axGRDisplayViewer1.Report = Report;
            axGRDisplayViewer1.Start();
        }
        private void LoadPrintViewer1()
        {
            axGRPrintViewer1.Report = Report;
            axGRPrintViewer1.Start();

            comboBox1.SelectedIndex = 0;
        }
        private void LoadDesignViewer1()
        {
            axGRDesigner1.Report = Report;
        }
        private void LoadExportType()
        {
            comboBox2.Items.Add(new ExportInfo("Excel", "xls", GRExportType.gretXLS));
            comboBox2.Items.Add(new ExportInfo("RTF", "rtf", GRExportType.gretRTF));
            comboBox2.Items.Add(new ExportInfo("PDF", "pdf", GRExportType.gretPDF));
            comboBox2.Items.Add(new ExportInfo("Html", "htm", GRExportType.gretHTM));
            comboBox2.Items.Add(new ExportInfo("Image", "tif", GRExportType.gretIMG));
            comboBox2.Items.Add(new ExportInfo("Text", "txt", GRExportType.gretTXT));
            comboBox2.Items.Add(new ExportInfo("CSV", "csv", GRExportType.gretCSV));
            comboBox2.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
            //上一句的运行将导致axGRPrintViewer1控件失效，因此下面两句重启控件。
            axGRPrintViewer1.Stop();
            LoadPrintViewer1();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.ShowToolbar = true;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: axGRDisplayViewer1.Report.Language = 2052; break;//简体中文
                case 1: axGRDisplayViewer1.Report.Language = 1028; break;//繁体中文
                case 2: axGRDisplayViewer1.Report.Language = 1033; break;//英文
                default: break;
            }
            axGRDisplayViewer1.UpdateLanguage();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.ShowToolbar = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ExportInfo item = ((ExportInfo)comboBox2.SelectedItem);
            Report.ExportDirect(item.ExportType, "Export." + item.ExtFileName, true, false);//最后一个参数设定导出后是否打开
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Report.grd";
            save.Filter = "报表文件 (*.grd)|*.grd|所有文件|*";
            if (save.ShowDialog() == DialogResult.OK)
                Report.GenerateDocumentFile(save.FileName);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "报表文件 (*.grd)|*.grd|所有文件|*";
            if (open.ShowDialog() == DialogResult.OK)
                axGRPrintViewer1.LoadFromDocumentFile(open.FileName);
        }
        OleDbConnection con;
        OleDbCommand cmd=new OleDbCommand();
        OleDbDataAdapter adapter;
        DataSet dataset=new DataSet();
        DataTable table, table2;
        private void InitializeDB()
        {
            con = new OleDbConnection("data source=" + dbPath + ";provider=Microsoft.Jet.OLEDB.4.0;");
            cmd.Connection = con;
            adapter = new OleDbDataAdapter(cmd);
            con.Open();
        }
        private void LoadData()
        {
            if (table != null) table.Clear();
            cmd.CommandText = "select LastName+FirstName as Name,Title,TitleOfCourtesy,HomePhone,PostalCode from Employees order by EmployeeID";
            adapter.Fill(dataset, "Employees");
            table = dataset.Tables["Employees"];
            //=======================开始载入数据到DataGridView
            //样式设定
            dataGridView1.Rows.Clear();//不会清除掉表头
            dataGridView1.Font = new Font("微软雅黑", 11.5F, FontStyle.Bold);//字体（加粗参数可不加）
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            //=======================表头
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "姓名";
            column1.Width = 80;
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "职位";
            column2.Width = 100;
            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.HeaderText = "尊称";
            column3.Width = 70;
            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.HeaderText = "家庭电话";
            column4.Width = 150;
            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.HeaderText = "邮政编码";
            column5.Width = 100;
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            //=======================数据
            for (int count = 0; count < table.Rows.Count; count++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = 35;
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = table.Rows[count]["Name"];
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = table.Rows[count]["Title"];
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = table.Rows[count]["TitleOfCourtesy"];
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = table.Rows[count]["HomePhone"];
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = table.Rows[count]["PostalCode"];
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                dataGridView1.Rows.Add(row);
            }
        }
        private void LoadData2()
        {
            if (table2 != null) table2.Clear();
            cmd.CommandText = "select ShipCity,count(*) as Num from Orders group by ShipCity having ShipCity='北京' or ShipCity='成都' or ShipCity='青岛' or ShipCity='上海'";
            adapter.Fill(dataset, "Chart");
            table2 = dataset.Tables["Chart"];
            //=======================开始载入数据到DataGridView
            //样式设定
            dataGridView2.Rows.Clear();//不会清除掉表头
            dataGridView2.Font = new Font("微软雅黑", 11.5F, FontStyle.Bold);//字体（加粗参数可不加）
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            //=======================表头
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "货主城市";
            column1.Width = 100;
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "订单总数";
            column2.Width = 100;
            dataGridView2.Columns.Add(column1);
            dataGridView2.Columns.Add(column2);
            //=======================数据
            for (int count = 0; count < table2.Rows.Count; count++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = 35;
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = table2.Rows[count]["ShipCity"];
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = table2.Rows[count]["Num"];
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                dataGridView2.Rows.Add(row);
            }
        }
        private void Report2Initialize()//生成自定义报表：第一步，定义格式
        {
            Report2.DetailGrid.Columns.RemoveAll();
            Report2.DetailGrid.Recordset.Fields.RemoveAll();
            //重复打印方式(RepeatStyle)等通用属性已在.grf文件内设定。
            //=========================================
            Report2.ControlByName("StaticBox1").AsStaticBox.Text = "员工信息表";
            Report2.ControlByName("StaticBox1").AsStaticBox.Font.Bold = true;
            Report2.ControlByName("StaticBox1").AsStaticBox.Font.Name = "楷体";
            Report2.ControlByName("StaticBox1").AsStaticBox.Font.Point = 20;
            //=========================================新增列 (Column)
            Report2.DetailGrid.AddColumn("col_Name", "姓名", "Name", 6);
            Report2.DetailGrid.AddColumn("col_Title", "职位", "Title", 8);
            Report2.DetailGrid.AddColumn("col_TitleOfCourtesy", "尊称", "TitleOfCourtesy", 3);
            Report2.DetailGrid.AddColumn("col_HomePhone", "家庭电话", "HomePhone", 9);
            Report2.DetailGrid.AddColumn("col_PostalCode", "邮政编码", "PostalCode", 6);
            //=========================================列样式
            foreach (IGRColumn each in Report2.DetailGrid.Columns)
            {
                //标题格 (TitleCell)
                each.TitleCell.TextAlign = GRTextAlign.grtaMiddleCenter;//居中
                each.TitleCell.Font.Bold = true;//粗体
                each.TitleCell.Font.Name = "楷体";//字体
                each.TitleCell.Font.Point = 15;//大小
                //内容格 (ContentCell)
                each.ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;//居中
                each.ContentCell.Font.Bold = false;//粗体
                each.ContentCell.Font.Name = "微软雅黑";//字体
                each.ContentCell.Font.Point = 12;//大小
            }
            //=========================================新增字段 (Field)
            Report2.DetailGrid.Recordset.AddField("Name", GRFieldType.grftString);
            Report2.DetailGrid.Recordset.AddField("Title", GRFieldType.grftString);
            Report2.DetailGrid.Recordset.AddField("TitleOfCourtesy", GRFieldType.grftString);
            Report2.DetailGrid.Recordset.AddField("HomePhone", GRFieldType.grftString);
            Report2.DetailGrid.Recordset.AddField("PostalCode", GRFieldType.grftString);
        }
        private void Report2FetchRecord()//生成自定义报表：第二步，载入数据
        {
            for (int count = 0; count < table.Rows.Count; count++)
            {
                Report2.DetailGrid.Recordset.Append();//单行新增开始
                Report2.FieldByName("Name").AsString = table.Rows[count]["Name"].ToString();
                Report2.FieldByName("Title").AsString = table.Rows[count]["Title"].ToString();
                Report2.FieldByName("TitleOfCourtesy").AsString = table.Rows[count]["TitleOfCourtesy"].ToString();
                Report2.FieldByName("HomePhone").AsString = table.Rows[count]["HomePhone"].ToString();
                Report2.FieldByName("PostalCode").AsString = table.Rows[count]["PostalCode"].ToString();
                Report2.DetailGrid.Recordset.Post();//单行新增结束
            }
        }
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            con.Dispose();
        }
        private void button8_Click(object sender, EventArgs e)//预览自定义报表
        {
            Report2.PrintPreview(true);//会自动调用 Initialize 和 FetchRecord 两个事件。
        }
        private void button9_Click(object sender, EventArgs e)//生成表格
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Excel文件|*.xls";
            save.FileName = System.DateTime.Now.ToString("yyMMdd.HHmmss");
            if (save.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                //添加Excel引用，右击项目引用，在COM选项卡中搜索Microsoft Excel，添加一个引用。
                Microsoft.Office.Interop.Excel.Application App = new Microsoft.Office.Interop.Excel.Application();//旧写法 Excel.Application
                Microsoft.Office.Interop.Excel.Workbooks books = App.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook book = books.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Sheets sheets = book.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
                //range = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[dataGridView.RowCount + 1, dataGridView.ColumnCount]);//旧写法，会报异常
                Microsoft.Office.Interop.Excel.Range range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[dataGridView1.RowCount + 1, dataGridView1.ColumnCount]];
                //range.NumberFormatLocal = "@";
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    sheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText.ToString().Trim();//表头
                for (int row = 0; row < dataGridView1.RowCount; row++)
                    for (int column = 0; column < dataGridView1.ColumnCount; column++)
                        sheet.Cells[row + 2, column + 1] = dataGridView1.Rows[row].Cells[column].Value.ToString().Trim();
                //range = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[dataGridView.RowCount + 1, dataGridView.ColumnCount]);//旧写法，会报异常
                range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[dataGridView1.RowCount + 1, dataGridView1.ColumnCount]];
                range.Columns.AutoFit();
                range.RowHeight = 18;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                book.Saved = true;
                book.SaveCopyAs(save.FileName);
                App.Visible = false;
                App.Quit();
                GC.Collect();
                Cursor = Cursors.Default;
                MessageBox.Show("表格已导出。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        IGRChart chart;
        IGRChart chart2;
        private void Report3Initialize()//生成图形报表
        {
            chart = Report3.ControlByName("Chart1").AsChart;
            chart.Title = "订单地区统计";
            chart.TitleFont.Name = "楷体";
            chart.TitleFont.Bold = true;
            chart.TitleFont.Point = 17;
            chart.Font.Name = "楷体";
            //chart.Font.Bold = true;
            chart.SeriesCount = 1;
            chart.Font.Point = 13;
            chart.GroupCount = Convert.ToInt16(table2.Rows.Count);
            chart.YAxisSpace = 5;
            chart.YAxisMaximum = 30;
            chart.YAxisMinimum = 0;
            //坐标可见性(CoordLineVisible)等属性已在.grf文件设定
            //chart.PrepareSnapShot();
            for (int count = 0; count < table2.Rows.Count; count++)
            {//添加数据
                chart.set_GroupLabel((short)count, table2.Rows[count][0].ToString());//各项名称
                //object aaa = table.Rows[count][1];
                chart.set_Value(0, (short)count, Convert.ToDouble(table2.Rows[count][1]));//各项数据
            }
            //chart.SnapShot();
            //=======================================以上是条形图
            //=======================================
            //=======================================以下是饼图
            chart2 = Report3.ControlByName("Chart2").AsChart;
            chart2.Title = "订单地区统计2";
            chart2.TitleFont.Name = "楷体";
            chart2.TitleFont.Bold = true;
            chart2.TitleFont.Point = 17;
            chart2.Font.Name = "楷体";
            //chart2.Font.Bold = true;
            chart2.SeriesCount = Convert.ToInt16(table2.Rows.Count);
            chart2.Font.Point = 13;
            chart2.GroupCount = 1;
            //坐标可见性(CoordLineVisible)等属性已在.grf文件设定
            //chart.PrepareSnapShot();
            for (int count = 0; count < table2.Rows.Count; count++)
            {//添加数据
                chart2.set_SeriesLabel((short)count, table2.Rows[count][0].ToString());//各项名称
                chart2.set_Value((short)count, 0, Convert.ToDouble(table2.Rows[count][1]));//各项数据
            }
            chart2.set_GroupLabel(0, "订单地区统计");
            //chart.SnapShot();
        }
        private void Report3FetchRecord()
        {
            //程序未运行到此处......
            MessageBox.Show("Report3FetchRecord");
        }
        private void Report3ProcessRecord()
        {
            //程序未运行到此处......
            MessageBox.Show("Report3ProcessRecord");
        }
        private void button10_Click(object sender, EventArgs e)//预览图形报表
        {
            Report3.PrintPreview(true);//会自动调用 Initialize, FetchRecord, ProcessRecord 三个事件。
        }
    }
    public class ExportInfo
    {
        private GRExportType exportType;//输出类型
        private string displayText;//类型名称
        private string extFileName;//输出名称
        public ExportInfo(string displayText, string extFileName, GRExportType exportType)
        {
            this.displayText = displayText;
            this.extFileName = extFileName;
            this.exportType = exportType;
        }
        public string DisplyText
        {
            get { return displayText; }
        }
        public string ExtFileName
        {
            get { return extFileName; }
        }
        public GRExportType ExportType
        {
            get { return exportType; }
        }
        public override string ToString()
        {
            return displayText;
        }
    }
}
