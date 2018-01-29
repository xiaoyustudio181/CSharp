using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MyFrame
{
    public class MyGridViewS : MyGridViewA
    {
        public MyGridViewS(DataGridView GV) : base(GV) { }
        //====数据
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        public void SetDB(SqlConnection con, string TableName)
        {
            this.con = con;
            cmd.Connection = con;
            adapter = new SqlDataAdapter(cmd);
            this.TableName = TableName;
            //M = MyModel.GetInstance(con);
        }
        void MakeCommandText()
        {
            if (SpecialSql == null)//不使用自编SQL语句
            {
                if (PerPage == 0)//不分页
                {
                    Sql = "select " + QueryFields + " from " + TableName;
                    if (where != null) Sql += where;
                    if (orderBy != null) Sql += orderBy;
                    cmd.CommandText = Sql;
                }
                else//要分页
                {
                    //例句：select top PerPage * from Categories where CategoryID not in(select top ((NowPage-1)*PerPage) CategoryID from Categories order by CategoryID) order by CategoryID--第一页数据
                    Sql = "select count(*) from " + TableName;
                    if (where != null) Sql += where;
                    DataAmount = M.FetchResult(Sql);//获取数据总数
                    TotalPages = Math.Ceiling(Convert.ToDouble(DataAmount) / PerPage);//获取总页数
                    if (NowPage > TotalPages) NowPage = TotalPages; //大于最大页码，包括没有数据的情况(1>0)，TotalPages=0
                    if (NowPage < 1) NowPage = 1; //小于最小页码
                    Sql = "select top " + PerPage + " " + QueryFields + " from " + TableName;
                    TopN = (NowPage - 1) * PerPage;
                    if (where != null)//有where条件
                    {
                        Sql += where + " and " + KeyColumn + " not in";
                        if (TopN != 0)//非第一页
                            Sql += "(select top " + TopN + " " + KeyColumn + " from " + TableName;
                        else Sql += "(0";//因为Access查询不识别top 0，所以这种情况直接赋0

                        if (orderBy != null)//有order by排序
                        {
                            if (TopN != 0)//非第一页
                                Sql += where + " " + orderBy + ")" + orderBy;
                            else Sql += ") " + orderBy;
                        }
                        else Sql += ")";
                    }
                    else//无where条件
                    {
                        Sql += " where " + KeyColumn + " not in";
                        if (TopN != 0)//非第一页
                            Sql += "(select top " + TopN + " " + KeyColumn + " from " + TableName;
                        else Sql += "(0";//因为Access查询不识别top 0，所以这种情况直接赋0

                        if (orderBy != null)//有order by排序
                        {
                            if (TopN != 0)//非第一页
                                Sql += orderBy + ")" + orderBy;
                            else Sql += ") " + orderBy;
                        }
                        else Sql += ")";
                    }
                    cmd.CommandText = Sql;
                }
            }
            else cmd.CommandText = SpecialSql;//使用自编SQL语句
        }
        //====装载：代码生成
        bool CheckCondition()
        {
            if (con == null)
            {
                MyDialog.Msg(GV.Name + "尚未绑定数据库！", 3);
                Environment.Exit(0);
                return false;
            }
            if (QueryFields == null)
            {
                MyDialog.Msg(GV.Name + "尚未设置查询字段！", 3);
                Environment.Exit(0);
                return false;
            }
            return true;
        }
        new public void ShowData(bool SetFirstColumnAsPrimaryKey = false)
        {
            RemoveColumns();
            if (!CheckCondition()) return;
            MakeCommandText();
            if (table != null) table.Clear();
            adapter.Fill(dataset, "Tab");
            table = dataset.Tables["Tab"];
            if (SetFirstColumnAsPrimaryKey)//设置主键后，主键列不允许出现重复值
                table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            GenerateHeader();
            LoadData();
            RenderRowColor();
        }
        //====装载：数据绑定
        new public void BindData(bool SetFirstColumnAsPrimaryKey = false)
        {
            if (!CheckCondition()) return;
            MakeCommandText();
            if (table != null) table.Clear();
            adapter.Fill(dataset, "Tab");
            table = dataset.Tables["Tab"];
            GV.Columns.Clear();
            while (GV.Columns.Count < table.Columns.Count)//自动生成列
                GV.Columns.Add(new DataGridViewTextBoxColumn());
            if (SetFirstColumnAsPrimaryKey)
                table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            DefaultStylesB();
            GV.AutoGenerateColumns = false;
            GV.DataSource = table;
            for (int i = 0; i < GV.Columns.Count; i++)
            {
                GV.Columns[i].HeaderText = table.Columns[i].ColumnName;
                GV.Columns[i].Width = ColumnWidths[i];
                GV.Columns[i].DataPropertyName = table.Columns[i].ColumnName;
                GV.Columns[i].SortMode = ColumnSortModes[i];
            }
            GV.RowTemplate.Height = RowHeight;
            //RenderRowColor();//无效
        }
        //====操作
        public override int Update()
        {
            try
            {
                new SqlCommandBuilder(adapter);
                return adapter.Update(table);
            }
            catch (Exception ex)
            {
                MyDialog.Msg(ex.Message, 3);
                return 0;
            }
        }
    }
}
