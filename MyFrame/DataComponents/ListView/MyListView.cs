using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyFrame
{
    public class MyListView
    {
        protected ListView LV;
        public MyListView(ListView LV)
        {
            this.LV = LV;
        }
        public string[] SelectedValues { set; get; }//需先调用方法获取
        public int[] SelectedIndexes { set;get; }
        public string[] GetSelectedValues()
        {
            int Count = CountSelectedRows;
            SelectedValues = new string[Count];
            Console.WriteLine("{0}选中行数：{1}", LV.Name, Count);
            for (int i = 0; i < Count; i++)
            {
                SelectedValues[i] = LV.SelectedItems[i].Text;
                Console.WriteLine("{0}选中值：{1}", LV.Name, SelectedValues[i]);
            }
            Console.WriteLine();
            return SelectedValues;
        }
        public int[] GetSelectedIndexes()
        {
            int Count = CountSelectedRows;
            SelectedIndexes = new int[Count];
            Console.WriteLine("{0}选中行数：{1}", LV.Name, Count);
            for (int i = 0; i < Count; i++)
            {
                SelectedIndexes[i] = LV.SelectedItems[i].Index;//获取每行的索引
                Console.WriteLine("{0}选中索引：{1}", LV.Name, SelectedIndexes[i]);
            }
            Console.WriteLine();
            return SelectedIndexes;
        }        
        //====选中值
        public int CountRows { get { return LV.Items.Count; } }//行总数
        public int CountSelectedRows { get { return LV.SelectedItems.Count; } }//选中行总数
        //====样式
        public bool AlignCenter = false;
        protected ImageList imgList = new ImageList();
        public int RowHeight = 25;//行高
        public int[] ColumnWidths = new int[] { };//列宽
        public Color RowColor = Color.White;//行色
        public void SetStyle(string FontFamily = "微软雅黑", float Size = 11.5F, FontStyle Style = FontStyle.Regular)
        {
            LV.Font = new Font(FontFamily, Size, Style);
            LV.FullRowSelect = true;
            LV.GridLines = true;
            LV.View = View.Details;
            imgList.ImageSize = new Size(1, RowHeight);
            LV.SmallImageList = imgList;//只能通过ImageList设置行高
        }
        protected void RenderRowColor()//渲染行色
        {
            for (int i = 0; i < LV.Items.Count; i++)
                if (i % 2 == 0) LV.Items[i].BackColor = RowColor;
        }
        public void Focus()
        {
            LV.Focus();
        }
        //====分页
        public double PerPage { get; set; }//每页显示的条数
        public double TotalPages { get; set; }//页码总数，Math.Ceiling(总条数/每页条数)
        public double NowPage { get; set; }//当前页码
        public object DataAmount { set; get; }//数据总数，统计用
        public string KeyColumn { get; set; }//主键列(排除用)
        protected MyModel M;
        protected double TopN;//分页SQL用
    }
}
