using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace MyFrame
{
    public class MyListViewA : MyListView
    {
        public MyListViewA(ListView LV) : base(LV) { }
        //==========================数据
        OleDbConnection con;
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter adapter;
        protected DataSet dataset = new DataSet();
        protected DataTable table;
        public void SetDB(OleDbConnection con, string TableName)
        {
            this.con = con;
            cmd.Connection = con;
            adapter = new OleDbDataAdapter(cmd);
            this.TableName = TableName;
            //M = new MyModel(con);
        }
        protected string orderBy, where, Sql, SpecialSql;
        public string TableName { get; set; }
        public string QueryFields { get; set; }
        public DataTable DataTable { get { return table; } }
        public void UseSpecialSQL(string SQL)
        {
            SpecialSql = SQL;
        }
        public void AbandonSpecialSQL()
        {
            SpecialSql = null;
        }
        public void Where(string Condition)
        {
            where = " where " + Condition;
        }
        public void OrderBy(string Condition)
        {
            orderBy = " order by " + Condition;
        }
        public void ClearWhere()
        {
            where = null;
        }
        public void ClearOrderBy()
        {
            orderBy = null;
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
        //==========================装载
        bool CheckCondition()
        {
            if (con == null)
            {
                MyDialog.Msg(LV.Name + "尚未绑定数据库！", 3);
                Environment.Exit(0);
                return false;
            }
            if (QueryFields == null)
            {
                MyDialog.Msg(LV.Name + "尚未设置查询字段！", 3);
                Environment.Exit(0);
                return false;
            }
            return true;
        }
        protected void GenerateHeader()
        {
            DefaultStyles();
            ColumnHeader Header;
            bool HasFirstColumnBuilt = false;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Header = new ColumnHeader();
                if (i == 0 && !HasFirstColumnBuilt)
                {
                    Header.Text = "";
                    Header.Width = 0;
                    i -= 1;//多循环一次，添加一个0宽度首列
                    HasFirstColumnBuilt = true;
                    LV.Columns.Add(Header);
                    continue;
                }
                Header.Text = table.Columns[i].ColumnName;
                Header.Width = ColumnWidths[i];
                Header.TextAlign = AlignCenter ? HorizontalAlignment.Center : HorizontalAlignment.Left;
                LV.Columns.Add(Header);
                //1，生成ListView时，为了美观可设第一列主键宽度为0，可通过点击事件取得其值；
                //2，生成ListView时，首列内容不能居中，但可设为0宽度首列，使其成为第二列。
            }
        }
        void DefaultStyles()
        {
            #region 若未设置列宽（默认80）
            if (ColumnWidths.Length == 0)
            {
                ColumnWidths = new int[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    ColumnWidths[i] = 80;
            }
            #endregion
        }
        protected void LoadData()
        {
            string RowName;
            int RowCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                RowName = "Row" + RowCount++;
                LV.Items.Add(RowName, table.Rows[i][0].ToString(), 0);//首列（用于装主键）
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (table.Rows[i][j].GetType() == typeof(DateTime))
                        LV.Items[RowName].SubItems.Add(((DateTime)table.Rows[i][j]).ToString("HH:mm, yyyy.MM.dd"));
                    else LV.Items[RowName].SubItems.Add(table.Rows[i][j].ToString());
                }
            }
        }
        public void ShowData(bool SetFirstColumnAsPrimaryKey = false)
        {
            LV.Clear();
            if (!CheckCondition()) return;
            MakeCommandText();
            if (table != null) table.Clear();
            adapter.Fill(dataset, "Tab");
            table = dataset.Tables["Tab"];
            if (SetFirstColumnAsPrimaryKey)
                table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            GenerateHeader();
            LoadData();
            RenderRowColor();
        }
        //==========================操作
        public virtual int Update()
        {
            try
            {
                new OleDbCommandBuilder(adapter);
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
