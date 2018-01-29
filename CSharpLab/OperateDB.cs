using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFrame;
namespace CSharpLab
{
    public class OperateDB
    {
        public static void Access()
        {
            new Access();
        }
        public static void Excel()
        {
            new Excel();
        }
    }
    public class Access
    {
        string DirPath = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;//ACCESS数据库所在目录
        OleDbConnection con;
        //SQL Server用法与此同（SqlConnection......）
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader reader;//需要Connection的Open状态
        OleDbDataAdapter adapter;//不需要Connection的Open状态

        DataSet dataset = new DataSet();//与Adapter配合
        DataTable table;
        DataRow row;
        object result;//用来装查询结果
        public Access()
        {
            con = MyDB.GetAccessConnection(DirPath + @"\Northwind.mdb");
            cmd.Connection = con;
            adapter = new OleDbDataAdapter(cmd);
            //con.Dispose();//此操作将清空Connection的数据源和连接字符串，之后不能再用.Open()打开数据库。
            Query();
            //Operate();
        }
        void Query()
        {
            con.Open();
            //一，查询单个值。
            //普通查询。优点是简单快捷。
            cmd.CommandText = "select Title from Employees where EmployeeID=2";
            result = cmd.ExecuteScalar();
            Console.WriteLine(result+", "+result.GetType());//输出值与值类型
            //带参数查询（同样可用于增、删、改的语句中）。优点是参数赋值清晰。
            cmd.CommandText = "select BirthDate from Employees where EmployeeID=@EmployeeID";
            cmd.Parameters.Clear();//使用参数前先清空参数，以免之前有值造成异常
            cmd.Parameters.AddWithValue("@EmployeeID", 2);
            result = cmd.ExecuteScalar();
            Console.WriteLine(result + ", " + result.GetType());//输出值与值类型

            //二，Reader查询单行多个值。
            cmd.CommandText = "select * from Employees where EmployeeID=2";
            reader = cmd.ExecuteReader();
            //当一个Command的DataReader未关闭时，不可使用同一个Connection的其他Command进行reader查询（Access可以进行单值查询，SQLServer不可），除非使用另一个实例化的Connection。
            reader.Read();
            Console.WriteLine(reader[0]+", "+reader["Address"]);//可用数字索引或字段名取出数据
            reader.Close();

            //三，Reader查询多行多个值。
            cmd.CommandText = "select * from Employees where EmployeeID in(3,4)";
            reader = cmd.ExecuteReader();
            //当一个Command的DataReader未关闭时，不可使用同一个Connection的其他Command进行reader查询（Access可以进行单值查询，SQLServer不可），除非使用另一个实例化的Connection。
            while(reader.Read())
                Console.WriteLine(reader[0] + ", " + reader["Address"]);
            reader.Close();
            con.Close();

            //四，Adapter查询。
            cmd.CommandText = "select * from Employees where EmployeeID in(5,6)";
            if (dataset.Tables["Tab1"] != null) dataset.Tables["Tab1"].Clear();//使用前先清空
            adapter.Fill(dataset, "Tab1");
            table = dataset.Tables["Tab1"];
            foreach (DataRow row in table.Rows)
                Console.WriteLine(row[1] + ", " + row["PostalCode"]);
        }
        void Operate()
        {
            //一，普通修改数据。
            con.Open();
            cmd.CommandText = "update Employees set City='纽约',Country='美国' where EmployeeID=2";
            cmd.ExecuteNonQuery();//同样可用于增、删的语句。
            con.Close();

            //二，Adapter修改数据。
            cmd.CommandText = "select * from Employees";
            if (dataset.Tables["Tab2"] != null) dataset.Tables["Tab2"].Clear();//使用前先清空
            adapter.Fill(dataset, "Tab2");
            table = dataset.Tables["Tab2"];
            table.PrimaryKey = new DataColumn[] { table.Columns[0] };//为DataTable设置主键（第一列，即EmployeeID）
            row = table.Rows.Find(3);//只有设置了主键才能使用的方法
            row["City"] = "东京";
            row["Country"] = "日本";
            new OleDbCommandBuilder(adapter);
            adapter.Update(table);//执行更新

            //三，Adapter删除数据。
            table.Rows.Find(5).Delete();
            new OleDbCommandBuilder(adapter);
            //adapter.Update(table);//执行删除

            //四，Adapter新增数据
            cmd.CommandText = "select * from Employees where EmployeeID<0";//新增数据不需要取其他数据，所以查询不存在的数据（只为取出数据表结构）
            if (dataset.Tables["Tab3"] != null) dataset.Tables["Tab3"].Clear();//使用前先清空
            adapter.Fill(dataset, "Tab3");
            table = dataset.Tables["Tab3"];
            row = table.NewRow();
            row["EmployeeID"] = 10;
            row["FirstName"] = "冬雨";
            row["LastName"] = "周";
            table.Rows.Add(row);
            new OleDbCommandBuilder(adapter);
            //adapter.Update(table);//执行新增
        }
    }
    public class Excel
    {
        string dir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        string path;
        DataTableCollection tables;
        DataRow row;
        public Excel()
        {
            path = dir+@"\Book1.xls";
            tables = MyDB.GetExcelDataTables(path,true);
            if (tables == null) return;
            //DataTable tab = tables[2];//可通过数字索引分别访问DataTable（按载入数据的先后顺序）
            //for (int r = 0; r < tab.Rows.Count; r++)
            //{
            //    row = tab.Rows[r];
            //    for (int c = 0; c < tab.Columns.Count; c++)
            //            Console.Write(row[c] + "  ");
            //    Console.WriteLine();
            //}
            foreach (DataTable table in tables)
            {
                //if (table.TableName != "Sheet1$") continue;
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    row = table.Rows[r];
                    for (int c = 0; c < table.Columns.Count; c++)
                        Console.Write(row[c] + "  ");
                    Console.WriteLine();
                }
                Console.WriteLine("===============");
            }
        }
    }
}
#region ADO.NET
/*ADO.NET是一种用于数据访问的数据模型，是连接程序和数据的重要桥梁。
             * 数据访问模型有ODBC,DAO,LOE DB,ADO等，ADO.NET是ADO最新发展的产物，更具有通用性。
             * ADO.NET对象模型由两部分组成：
             * 1，.NET数据提供程序(Data Provider)；
             * 2，数据集(DataSet)。
             * 前者用于连接数据源、执行命令以及获取结果；后者实现独立于数据源的数据访问，是数据在本地的缓存。
             * Microsoft SQL Server.NET四种相应的对象：SqlConnection, SqlCommand, SqlDataReader, SqlDataAdapter。其余的提供程序与此类似，只是前缀名不同。
             * 1，Connection对象：用于创建与数据源的连接；
             * 2，Command对象：用于对数据源执行操作并返回结果；
             * 3，DataReader对象：一个快速、只读、只向前的游标，用于以最快的速度检索并检查查询所返回的行；
             * 4，DataAdapter对象：数据源和数据集之间的桥梁，用于实现填充数据集和更新数据源的作用。
             * ================================================
             * ConnectionString属性是Connection对象最重要的属性，它包含创建连接对象所必需的信息，以字符串的形式提供。常用的连接字符串：
             * 1，Data Source(Server)，包含数据库的位置和文件，默认值为空(或用.表示本地，可填IP地址)；
             * 2，Initial Catalog(database)，数据库的名称，默认值为空；
             * 3，Integrated Security(Trusted_Connection)，集成安全性，当该值为真时，数据源使用当前身份验证的Microsoft Windows账户凭据。该值的取值可为true/false,yes/no以及sspi(与true等价，推荐使用)，默认值为false；
             * 4，User ID(UID)，要使用的数据源的登录账户；
             * 5，Password(PWD)，要使用的数据源的登录账户密码；
             * 6，Provider，设置或返回连接的OLE DB数据提供程序(仅适用于OLE DB .NET数据提供程序)，默认值为空；
             * 7，Connection Timeout(Connect Timeout)，在数据源终止尝试和返回错误之前，连接到服务器所需等待的时间，单位为秒，默认值为15s；
             * 8，Persist Security Info，当值为false时，数据源不返回安全敏感信息，如密码等，默认值为false。
             * ================================================
             * Connection的方法：
             * 1，Open()，利用ConnectionString属性的信息连接数据源并打开该连接；
             * 2，Close()，显式地关闭一个已经打开的连接；
             * 3，Dispose()，释放Connection对象所使用的资源，调用该方法时会自动调用Close()来关闭连接。*/
#endregion
#region Connection，Command
/*创建Connection对象后可以使用Command对象来执行对数据源的增、删、查、改。
             * Command对象的属性：
             * 1，CommandType，指定命令的类型，以指明如何书写CommandText属性。主要有3中类型：Text、StoredProcedure、TableDirect，分别代表SQL语句、存储过程和直接操作表，默认类型为Text。SQL Server .NET数据提供程序不存在TableDirect类型。
             * 2，CommandText，获取或设置Command对象所要执行的SQL语句或存储过程，其值随CommandType变化。当CommandType为Text时，该属性设置为相应的SQL语句；当CommandType为StoredProcedure时，该属性设置为存储过程的名称；当CommandType为TableDirect时，该属性设置为表名。
             * 3，Prameters，参数集合属性。用来设置SQL语句或存储过程中的参数，以便能够正确地处理输入、输出和返回值类型的参数。Prameters用来存放所有的参数对象，如果为Microsoft SQL Server.NET数据提供程序，则参数对象为SqlParameter，参数对象的主要属性如下：1，ParameterName，参数名称，如@stu_id；2，SqlDbType，参数的数据类型；3，Size，参数中数据的最大字节数；
             * 4，Direction，指定参数的方向，可以是如下值：ParameterDirection.Input指明为输入参数(默认)，ParameterDirection.Output指明为输出参数，ParameterDirection.InputOutput既可为输入也可为输出，ParameterDirection.ReturnValue指明为返回值类型参数；
             * 5，Value，指明输入参数的值。*/
#endregion
#region Command方法
/*1，ExecuteScalar，执行查询，返回查询所返回的结果集的第一行第一列的值；
             * 2，ExecuteReader，执行查询，返回一个或多个结果集，存放于DataReader对象；
             * 3，ExecuteNonQuery，用于执行插入、修改、删除等操作，返回受影响的行数；
             * 4，ExecuteXmlReader，返回DMLReader对象，并要求SQL语句或存储过程以XML格式返回结果。
             * ====================================================
             * DataReader的方法和属性：
             * 1，Get(XXX)，以指定类型获取指定列的信息，需提供一个Int32类型的参数来指定要获取行的列序号，参数为从0开始；
             * 2，Close()，关闭DataReader对象；
             * 3，GetOrdinal()，获取指定列的名称，参数为从0开始的列序号；
             * 4，GetValue()，获取以本机格式表示的指定列的值，参数为从0开始的列序号；
             * 5，NextResult()，当读取批处理Transact-SQL语句的结果时，使数据读取器前进到下一个结果；
             * 6，IsDBNull()，获取一个值，用于指示列中是否包含不存在的或缺少的值，参数为从0开始的列序号；
             * 7，Read()，使DataReader前进道下一条记录，如果下一条存在则返回true，否则返回false；
             * 8，HasRows属性，指明DataReader对象是否包含一行或多行；
             * 9，IsClosed属性，指明是否已关闭指定的DataReader对象。*/
#endregion