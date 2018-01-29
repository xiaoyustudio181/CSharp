using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MyFrame
{
    public class MyDB
    {
        public static OleDbConnection GetAccessConnection(string AccessPath, string Password = "")
        {
            OleDbConnection con = new OleDbConnection(
                "data source=" + AccessPath + ";"+
                "provider=Microsoft.Jet.OLEDB.4.0;" +//Microsoft.ACE.OLEDB.12.0
                "mode=12;" +//独占方式
                ""+
                "Jet OleDb:DataBase Password=" + Password);
            try
            {
                con.Open();
                con.Close();
                return con;
            }
            catch (Exception ex)
            {
                MyDialog.Msg(ex.Message, 3);
                return null;
            }
        }
        public static SqlConnection GetSqlServerConnection(string ServerIP, string Password, string DBName, string UserID = "sa")
        {
            SqlConnection con = new SqlConnection(
                "data source=" + ServerIP + ";"+
                "initial catalog=" + DBName + ";"+
                "uid=" + UserID + ";"+
                "pwd=" + Password + ";"+
                "integrated security=no;"+
                "connection timeout=3;"+
                ""+
                "persist security info=false;");
            try
            {
                con.Open();//若IP地址有误，运行到这里时会等待很久，因此用connection timeout定时结束等待
                con.Close();
                return con;
            }
            catch (Exception ex)
            {
                MyDialog.Msg(ex.Message, 3);
                return null;
            }
        }
        /// <summary>
        /// 获取 Excel 文件内所有表格的数据。
        /// </summary>
        /// <param name="ExcelPath">文件路径。</param>
        /// <param name="ReadAllSheets">是否读取所有Sheet内容。若不是则只读取第一个Sheet。</param>
        /// <returns>所有数据。</returns>
        public static DataTableCollection GetExcelDataTables(string ExcelPath, bool ReadAllSheets = false)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataSet dataset = new DataSet();
            DataTable info;//表格名集合
            DataRow row;
            string sheetName;
            //ConStr="Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"; //只能操作Excel2007之前(.xls)文件
            //ConStr="Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //可以操作.xls與.xlsx文件
            //*/
            OleDbConnection con = new OleDbConnection(
                "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + ExcelPath + ";" +
                "Extended Properties=Excel 8.0;");
            /*/
            OleDbConnection con = new OleDbConnection(
                "Provider=Microsoft.Ace.OLEDB.12.0;" +
                "Data Source=" + ExcelPath + ";" +
                "Extended Properties=Excel 12.0;");//前提是安装了新版Office
            //*/
            cmd.Connection = con;
            try
            {
                con.Open();
                info = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                con.Close();
                for (int r = 0; r < info.Rows.Count; r++)
                {
                    row = info.Rows[r];
                    sheetName = row["TABLE_NAME"].ToString();
                    cmd.CommandText = "select * from [" + sheetName + "]";
                    adapter.Fill(dataset, sheetName);
                    if (!ReadAllSheets) break;
                    //注意：若要在查询语句中指定Sheet的名称，其后要加一个$，如Sheet1$, Sheet2$
                }
                return dataset.Tables;
                //程序将第一行有数据的行作为表头行。
                //程序认为表头之前的空行不存在。
                //程序从表头的下一行开始读取数据。
            }
            catch (Exception ex)
            {
                MyDialog.Msg(ex.Message, 3);
                return null;
            }
        }
    }
}
