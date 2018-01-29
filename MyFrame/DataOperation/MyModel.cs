using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;

namespace MyFrame
{
    public class MyModel
    {
        #region 初始化
        static MyModel instance;
        //*/
        //使用ACCESS数据库
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader reader;
        OleDbCommand cmdRea = new OleDbCommand();
        OleDbDataAdapter adapter;
        OleDbCommand cmdAda = new OleDbCommand();
        public static MyModel GetInstance(OleDbConnection con, bool NewModel = false)
        {
            if (!NewModel)//全局单例模式
            {
                if (instance == null ||//未曾实例化过
                    instance.con.ConnectionString == "")//.Dispose()后
                    instance = new MyModel(con);//新实例化
                return instance;
            }
            else return new MyModel(con);//新实例化
        }
        MyModel(OleDbConnection con)
        {
            this.con = con;
            cmd.Connection = con;
            cmdRea.Connection = con;
            cmdAda.Connection = con;
            adapter = new OleDbDataAdapter(cmdAda);
        }
        /*/
        //使用SQL SERVER数据库
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        SqlCommand cmdRea = new SqlCommand();
        SqlDataAdapter adapter;
        SqlCommand cmdAda = new SqlCommand();
        public static MyModel GetInstance(SqlConnection con, bool NewModel = false)
        {
            if (!NewModel)//全局单例模式
            {
                if (instance == null ||//未曾实例化过
                    instance.con.ConnectionString == "")//.Dispose()后
                    instance = new MyModel(con);//新实例化
                return instance;
            }
            else return new MyModel(con);//新实例化
        }
        MyModel(SqlConnection con)
        {
            this.con = con;
            cmd.Connection = con;
            cmdRea.Connection = con;
            cmdAda.Connection = con;
            adapter = new SqlDataAdapter(cmdAda);
        }
        //*/
        DataSet dataset = new DataSet();
        DataTable table;
        DataRow row;
        int num;
        object result;
        string temp;
        bool doOpenClose = true;
        #endregion

        #region 封装（数据源操作）
        public DataTable GetTable(string TableName)
        {
            return dataset.Tables[TableName];
        }
        public DataTable FillTable(string SQL, string TableName, bool SetKey = true)
        {
            cmdAda.CommandText = SQL;
            if (dataset.Tables[TableName] != null)
                dataset.Tables[TableName].Clear();
            adapter.Fill(dataset, TableName);
            table = dataset.Tables[TableName];
            if (SetKey) table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            return table;
        }
        //*/
        public int Update(string TableName)
        {
            new OleDbCommandBuilder(adapter);
            return adapter.Update(dataset, TableName);
        }
        public int Update(DataTable Table)
        {
            new OleDbCommandBuilder(adapter);
            return adapter.Update(Table);
        }
        /*/
        public int Update(string TableName)
        {
            new SqlCommandBuilder(adapter);
            return adapter.Update(dataset, TableName);
        }
        public int Update(DataTable Table)
        {
            new SqlCommandBuilder(adapter);
            return adapter.Update(Table);
        }
        //*/
        /*/
        //导出DataTable数据到表格.xls
        public void SaveToExcel(string TableName,string Name="Default")
        {
            SaveToExcel(dataset.Tables[TableName], Name);
        }
        string TargetPath;
        public void SaveToExcel(DataTable Table,string Name="Default")
        {
            if (Name == "Default")
                Name = MyTime.GetNowTimeCode();
            TargetPath=MyDialog.SavingDialog(Name, "xls");
            if (TargetPath == "") return;
            else
            {
                //添加Excel引用，右击项目引用，在框架选项卡中搜索Microsoft Excel，添加一个引用。
                Microsoft.Office.Interop.Excel.Application App = new Microsoft.Office.Interop.Excel.Application();//旧写法 Excel.Application
                Microsoft.Office.Interop.Excel.Workbooks books = App.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook book = books.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Sheets sheets = book.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
                //range = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[dataGridView.RowCount + 1, dataGridView.ColumnCount]);//旧写法，会报异常
                Microsoft.Office.Interop.Excel.Range range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[Table.Rows.Count + 1, Table.Columns.Count]];
                //range.NumberFormatLocal = "@";
                for (int i = 0; i < Table.Columns.Count; i++)
                    sheet.Cells[1, i + 1] = Table.Columns[i].ColumnName.ToString().Trim();//表头
                for (int row = 0; row < Table.Rows.Count; row++)
                    for (int column = 0; column < Table.Columns.Count; column++)
                        sheet.Cells[row + 2, column + 1] = Table.Rows[row][column].ToString().Trim();
                //range = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[dataGridView.RowCount + 1, dataGridView.ColumnCount]);//旧写法，会报异常
                range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[Table.Rows.Count + 1, Table.Columns.Count]];
                range.Columns.AutoFit();
                range.RowHeight = 18;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                book.Saved = true;
                book.SaveCopyAs(TargetPath);
                //App.Visible = false;
                App.Quit();
                GC.Collect();
            }
        }
        //*/
        #endregion
        #region 封装（基本操作）
        public object FetchResult(string SQL)
        {
            if (doOpenClose) con.Open();
            cmd.CommandText = SQL;
            result = cmd.ExecuteScalar();
            if (doOpenClose) con.Close();
            return result;
        }
        int affectedNum;
        public int Execute(string SQL)
        {
            if (doOpenClose) con.Open();
            cmd.CommandText = SQL;
            affectedNum = cmd.ExecuteNonQuery();
            if (doOpenClose) con.Close();
            return affectedNum;
        }
        public DataTable FetchDetails(string SQL)
        {
            cmdAda.CommandText = SQL;
            if (dataset.Tables["Temp"] != null)
                dataset.Tables["Temp"].Clear();
            adapter.Fill(dataset, "Temp");
            return dataset.Tables["Temp"];
        }
        #endregion
    }
}
