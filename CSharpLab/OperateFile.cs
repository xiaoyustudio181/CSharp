using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class OperateFile
    {            
        static string Root = Environment.CurrentDirectory;
        static string path;
        static string Parent;
        static OperateFile()
        {
            path = Root + @"\Record.txt";
            Parent = Path.GetDirectoryName(Root);
        }
        public static void Test1()
        {
            //Paths();
            //Files();
            //Infos();
            Streams();
        }
        static void Paths()
        {
            /*Path类：
             * 通过它可以访问和操作文件或目录路径的每个段，
             * 包括驱动器盘符、目录名、文件名、文件扩展名以及路径段分隔符。
             * 该类的方法均为静态方法。*/
            Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}",
                path,//文件完整路径
                Path.GetFullPath(path),//文件完整路径
                Path.GetFileName(path),//文件名带扩展名
                Path.GetFileNameWithoutExtension(path),//文件名不带扩展名
                Path.GetExtension(path),//文件扩展名，如.txt
                Path.GetDirectoryName(path),//文件所在目录的完整路径
                Path.HasExtension(path),//文件是否有扩展名
                Path.GetRandomFileName(),//返回随机文件或文件夹名
                Path.GetTempPath(),//返回用户的临时文件夹路径
                Path.GetTempFileName());
        }
        static void Files()
        {
            /*File类：
             * 用于操作文件的静态类，不需创建实例，常用于对文件的单步操作。
             * 调用File类的每个方法时都需要执行安全性检查，以确认用户被授予操作权限。
             * 使用File创建文件时，只能在已存在的文件夹内创建文件，不可自动创建文件夹。*/
            //File.Create(path).Close();//返回FileStream，已存在则覆盖，关闭FileStream
            //File.Delete(path);//若文件不存在，不报异常
            //File.CreateText(path).Close();//返回StreamWriter，已存在则覆盖，关闭StreamWriter
            File.Move(path, Parent + @"\RecordMoved.txt");//若未找到源文件，则报异常；若目标目录已存在同名文件，则报异常
            File.Move(Parent + @"\RecordMoved.txt", path);

            //File.Open(path, FileMode.Open).Close();//返回FileStream，文件不存在则报异常
            //File.Open(path, FileMode.Create).Close();//返回FileStream
            //File.Open(path, FileMode.OpenOrCreate).Close();//返回FileStream
            //File.Open(path, FileMode.CreateNew).Close();//返回FileStream
            //File.Open(path, FileMode.Append).Close();//返回FileStream
            //File.Open(path, FileMode.Truncate).Close();//清空文件，返回FileStream，文件不存在则报异常

            File.WriteAllText(path, "Sophie\r\nSienna", Encoding.UTF8);//覆盖写入，文件不存在则创建
            File.WriteAllLines(path, new string[] { "Jill", "Spencer" }, Encoding.UTF8);//覆盖写入，文件不存在则创建
            File.AppendText(path).Close();//返回StreamWriter，文件不存在则创建
            File.AppendAllText(path, "Allen\r\nMilla\r\n", Encoding.UTF8);
            File.AppendAllLines(path, new string[] { "Ayanami", "Shikinami" }, Encoding.UTF8);
            File.ReadAllText(path);//文件不存在则报异常
            if (File.Exists(path)) Console.WriteLine(File.ReadAllText(path));
            foreach (string line in File.ReadAllLines(path))
                Console.WriteLine(line);
            Console.WriteLine("创建时间：{0},\n最后进入时间：{1},\n最后写入时间：{2}", 
                File.GetCreationTime(path),
                File.GetLastAccessTime(path),
                File.GetLastWriteTime(path));
            //文件不存在将得到错误的结果
        }
        static void Infos()
        {
            FileInfo file1 = new FileInfo(path);
            //file1.MoveTo(Parent + @"\RecordMoved.txt");
            //Console.WriteLine(file1.FullName);
            //file1.MoveTo(path);
            
            foreach (string each in Directory.GetFiles(Root))
                Console.WriteLine(each);

            DirectoryInfo dir1 = new DirectoryInfo(Root);
            Console.WriteLine(dir1.FullName);
        }
        static void Streams()
        {
            StreamWriter writer = new StreamWriter(path, true);//true为追加，false为覆盖；不存在则创建。
            writer.WriteLine("Scarlett");
            writer.WriteLine("Ali");
            writer.Close();//覆盖写入时，只有当writer调用.Close()时才真正写入本次所有内容。

            StreamReader reader = new StreamReader(path);
            //string content = reader.ReadToEnd();
            //Console.WriteLine(content);
            while (!reader.EndOfStream)
                Console.WriteLine(reader.ReadLine());
            Console.WriteLine(reader.EndOfStream);
        }
    }
}
