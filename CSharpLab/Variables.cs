using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Variables
    {
        public static void Test1()//值类型：整型、浮点型、布尔型、字符型
        {
            #region 整型
            sbyte sbyteMin = -128, sbyteMax = 127;
            byte byteMin = 0, byteMax = 255;
            short shortMin = -32768, shortMax = 32767;
            ushort ushortMin = 0, ushortMax = 65535;
            int intMin = -2147483648, intMax = 2147483647;
            uint uintMin = 0, uintMax = 4294967295;
            long longMin = -9223372036854775808, longMax = 9223372036854775807;
            ulong ulongMin = 0, ulongMax = 18446744073709551615;
            Console.WriteLine("{0}, {1}, {2}", sbyteMin, sbyteMin.GetType(), typeof(sbyte));
            /* 若超过取值范围会发生数据溢出，得到不正确的结果。*/
            #endregion
            #region 浮点型
            /*小数默认是double类型，double比float精确；
             * decimal用于统计、金融和货币方面的计算，有很高的精确度，但取值范围远小于double。*/
            float float1 = -123456789.123456789f, float2 = 123456789.123456789f;
            double double1 = -123456789.123456789, double2 = 123456789.123456789;
            decimal decimal1 = -123456789.123456789m, decimal2 = 123456789.123456789m;
            Console.WriteLine("{0}, {1}, {2}", float1, float1.GetType(), typeof(float));
            #endregion
            #region 布尔型
            bool bool1 = true, bool2 = false;//和其他变量类型不存在转换关系
            Console.WriteLine("{0}, {1}, {2}", bool1, bool1.GetType(), typeof(bool));
            #endregion
            #region 字符型
            char char1 = 'a', char2 = 'b';
            Console.WriteLine("{0}, {1}, {2}", char1, char1.GetType(), typeof(char));
            Console.WriteLine("转义字符：\'  \"");//字符类型还有转义字符
            #endregion
            #region 字符串型
            string s1 = "Sophie";
            String s2 = "Sophie";
            StringBuilder s3 = new StringBuilder("Sophie");//用此类对象操作字符串比用string省内存
            string s4 = " Jill "; 
            
            Console.WriteLine("{0}, {1}, {2}", typeof(string), typeof(string) == typeof(String), typeof(StringBuilder));
            Console.WriteLine("Length: {0}. {1}, {2}. Length2: {3}", s4.Length, s4.TrimEnd('i', ' ', 'e'), s4.TrimStart('S', ' '), s4.Trim().Length);
            Console.WriteLine("{0}, {1}, {2}", String.Compare("a", "b"), String.Compare("b", "a"), String.Compare("a", "a"));//-1,1,0
            Console.WriteLine("{0}, {1}, {2}", s1.Equals(s2), s1.Equals("Sophie"), s1.Equals(s3));//true,true,false
            Console.WriteLine("{0}, {1}, {2}", s1 == s2, s1 == "Sophie", s1.GetType() == s3.GetType());//true,true,false
            Console.WriteLine("{0}, {1}, {2}", s1.StartsWith("So"), s1.EndsWith("ie"), s1.Contains("ph"));//true,true,true
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", s4.IndexOf('l'), s4.IndexOf('l', 4), s4.IndexOf('l', 5), s4.IndexOf("ll"), s4.LastIndexOf('l'));//3,4,-1,3,4
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", s1.Remove(2), s1.Remove(2, 1), s1.Replace("e", "a"), s1.Substring(2), s1.Substring(2, 2));//So,Sohie,Sophia,phie,ph
            Console.WriteLine("{0}, {1}, {2}, {3}", s1.Insert(0, "2"), s1.Insert(1, "2"), s1.Insert(5, "2"), s1.Insert(6, "2"));//2Sophie,S2ophie,Sophi2e,Sophie2
            foreach (string str in "Sophie,Jill,Ada,Claire".Split(','))
                Console.Write(str + " | ");
            Console.WriteLine();
            string[] strs = { "Sophie", "Jill", "Ada", "Claire" };
            Console.WriteLine(String.Join(", ", strs));
            Console.WriteLine("{0}, {1}", s2.PadLeft(12, '0'), s2.PadRight(12, '0'));
            Console.WriteLine(new char[] { 'H', 'a', 'r', 'r', 'y', ' ', 'P', 'o', 't', 't', 'e', 'r' });
            #endregion
            const int CONST1 = 1;//定义常量（非变量，不可二次赋值）
        }
        public static void Test2()//引用类型：数组型、枚举类型、集合类型、结构类型、特殊类型、类型转换
        {
            #region 数组型
            byte[] bytes1 = new byte[2];
            bytes1[0] = 10;
            bytes1[1] = 20;
            byte[] bytes2 = new byte[] { 30, 40 };//可省略new byte[]
            byte[,] bytes3 = new byte[2, 2];//二维数组
            bytes3[0, 0] = 1;
            bytes3[0, 1] = 100;
            bytes3[1, 0] = 2;
            bytes3[1, 1] = 200;
            byte[,] bytes4 = new byte[,] { { 1, 100 }, { 2, 200 } };//可省略new byte[,]
            byte[][] bytes5 = new byte[2][];//不规则数组
            bytes5[0] = new byte[] { 1, 2, 3 };
            bytes5[1] = new byte[] { 4, 5, 6, 7, 8 };
            Console.WriteLine("{0}, {1}, {2}, {3}", bytes3, bytes3.GetType(), typeof(byte[,]), bytes3.Length);
            #endregion
            #region 枚举类型
            Console.WriteLine("{0}, {1}, {2}", typeof(Day), (int)Day.Monday, Day.Monday.ToString());
            Console.WriteLine("{0}, {1}, {2}", typeof(Day), (int)Day.Tuesday, Day.Tuesday.ToString());
            Console.WriteLine("{0}, {1}, {2}", typeof(Day), (int)Day.Friday, Day.Friday.ToString());
            Console.WriteLine("{0}, {1}, {2}", typeof(Day), (int)Day.Saturday, Day.Saturday.ToString());
            Console.WriteLine("{0}, {1}, {2}", typeof(Day), (int)Day.Sunday, Day.Sunday.ToString());
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", Enum.Parse(typeof(Day), "0"), Enum.Parse(typeof(Day), "Monday"), Enum.GetValues(typeof(Day)).GetValue(0), Enum.GetNames(typeof(Day))[0], Enum.GetName(typeof(Day), 0));//获取枚举的五个方法
            int[] days = (int[])Enum.GetValues(typeof(Day));
            foreach (int day in days)//枚举遍历1
                Console.Write("{0}, {1} | ", day, Enum.Parse(typeof(Day), day.ToString()));
            Console.WriteLine();
            string[] days2 = Enum.GetNames(typeof(Day));
            foreach (string day in days2)//枚举遍历2
                Console.Write("{0} | ", day);
            Console.WriteLine();
            Day day1 = Day.Tuesday;
            switch (day1)
            {
                case Day.Monday: Console.WriteLine("星期一"); break;
                case Day.Tuesday: Console.WriteLine("星期二"); break;
                case Day.Wednesday: Console.WriteLine("星期三"); break;
            }
            #endregion
            #region 集合类型1：哈希表
            //集合类型SortedList与之大同，不同的是它按键排序（从小到大）
            Hashtable table = new Hashtable();
            table.Add("Name", "Sophie");
            table.Add("Age", 24);
            table.Add("Gender", false);
            table.Add("Word1", new StringBuilder("Hello."));
            table.Add("Word2", new StringBuilder("Hi."));
            table.Add(5, 100);
            table.Add(6, 200);
            table.Remove("Word2");
            table.Remove(5);
            foreach (DictionaryEntry each in table)
                Console.Write("{0}: {1}  ||  ", each.Key, each.Value);
            Console.WriteLine();
            foreach (object each in table.Keys)
                Console.Write(each + " | ");
            Console.WriteLine();
            foreach (object each in table.Values)
                Console.Write(each + " | ");
            Console.WriteLine();
            Console.WriteLine("{0}, {1}, {2}, {3}", table.Contains("Word1"), table.Contains(6), table.Contains("Word2"), table.Contains(5));//true,true,false,false
            Console.WriteLine("{0}, {1}, {2}, {3}", table.ContainsKey("Word1"), table.ContainsKey(6), table.ContainsKey("Word2"), table.ContainsKey(5));//true,true,false,false
            Console.WriteLine("{0}, {1}, {2}, {3}", table.ContainsValue("Sophie"), table.ContainsValue(200), table.ContainsValue(false), table.ContainsValue(25));//true,true,true,false
            Console.WriteLine("Count: {0}.", table.Count);
            Console.WriteLine("{0} : {1}", table["Age"], table["Age"].GetType());
            Console.WriteLine("{0} : {1}", table["Word1"], table["Word1"].GetType());
            table.Clear();
            Console.WriteLine("Count: {0}.", table.Count);
            #endregion
            #region 集合类型2：List泛型
            List<string> list1 = new List<string>() { "Apple", "Banana", "Apple2" ,"Chips"};
            //List<string> list1 = new List<string>();
            //list1.Add("Apple");
            //list1.Add("Banana");
            //list1.Add("Apple2");
            //list1.Add("Chips");
            //list1.Remove("Chips");
            list1.RemoveAt(3);
            list1.Reverse();
            string result1 = list1.Find(result => { return result.StartsWith("A"); });
            string result2 = list1.FindLast(result => { return result.StartsWith("A"); });
            bool result3 = list1.Exists(result => { return result.StartsWith("B"); });
            bool result4 = list1.Exists(result => { return result.StartsWith("C"); });
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", list1[0], list1[1], result1, result2, result3, result4);
            foreach (string each in list1)
                Console.Write(each + " | ");
            Console.WriteLine();
            List<int> list2 = new List<int>() { 3, 2, 1 };
            Console.WriteLine(list2.Count);
            List<bool> list3 = new List<bool>() { true, false, true };
            List<Student> list4 = new List<Student>();
            Console.WriteLine();
            #endregion
            #region 集合类型3：Queue（先进先出，foreach遍历的顺序与此同）
            //集合类型Stack与Queue大同，不同的是它后进先出。stack.push()新增/.pop()移除)
            Queue queue = new Queue();
            for (int i = 0; i < 5; i++)
                queue.Enqueue(i);
            foreach (int each in queue)
                Console.Write(each + ", ");
            Console.WriteLine();
            while (queue.Count != 0)
                Console.Write("{0} 被移除。", queue.Dequeue());
            Console.WriteLine();
            #endregion
            #region 集合类型4：ArrayList（先进先出，也可以倒序遍历）
            ArrayList list5 = new ArrayList();
            for (int i = 0; i < 5; i++)
                list5.Add(i);
            foreach (int each in list5)
                Console.Write(each + ", ");
            Console.WriteLine("===========");
            #endregion
            #region 结构类型
            //Student Student1 = new Student(1, "Jerry", 22);
            Student Student1;
            Student1.ID = 1;
            Student1.Name = "Tom";
            Student1.Age = 24;
            Student1.SayHi();
            Console.WriteLine("{0}, {1}, {2}", Student1, Student1.GetType(), typeof(Student));
            #endregion
            #region 特殊类型：Nullable<?>
            string s1 = null;
            StringBuilder s2 = null;
            StringBuilder s3 = new StringBuilder();
            /* 因为变量不能用0代替未赋值，因此欲用null表示未赋值，但整数类型变量不能赋值为null，
             * Framwork 2.0中提供Nullable类型，可以为值类型变量赋值为null。
             * HasValue是Nullable一个重要属性，用来判断是否为null值。*/
            Nullable<byte> byte1 = null;
            byte? byte2 = null;
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", s1 == null, s2 == null, s3 == null, byte1 == null, byte1.HasValue, byte2 == null, byte2.HasValue);//t,t,f,t,f,t,f
            #endregion
            #region 类型转换
            #region 隐式类型转换
            /*由编译器自动进行，不需要做任何工作，一般由取值范围小的类型转化为取值范围大的类型，因此在转换过程中不会丢失数据。例如：
            byte——[ushort,short,uint,int,ulong,long,float,double,decimal]
            sbyte——[short,int,long,float,double,decimal]
            short——[int,long,float,double,decimal]
            ushort——[uint,int,ulong,long,float,double,decimal]
            int——[long,float,double,decimal]
            uint——[ulong,long,float,double,decimal]
            long——[float,double,decimal]
            ulong——[float,double,decimal]
            char——[ushort,uint,int,ulong,long,float,double,decimal]
            float——[double]*/
            #endregion
            byte byte3 = 10;
            short short1 = byte3;//byte型 隐式转换 为short型
            #region 显示类型转换
            /*又叫强制类型转换，使变量值在要转换的类型取值范围内，只要原类型的取值范围比要转换的类型的取值范围大，就不可以隐式转换。由于显示类型转换是从大取值范围向小取值范围转换，因此可能出现数据丢失的情况。*/
            #endregion
            short1 = 255;
            byte3 = (byte)short1;//强制转换
            #region 装箱拆箱
            /*除了值类型之间的转换，值类型和引用类型也可以转换。
             * 装箱(Boxing)，指从值类型到引用类型的转换，系统会从堆中分配一块内存，将值类型数据复制到这块内存，再使引用类型数据指向这块内存。
             * 拆箱(Unboxing)，指从引用类型到值类型的显示转换，只能将已装箱的数据拆箱为装箱前的类型。
             * 多次进行装箱拆箱对程序性能有影响，需谨慎。*/
            #endregion
            object obj1 = short1;//装箱
            short1 = (short)obj1;//拆箱
            #endregion
        }
        public static void Test3()//引用类型：对象
        {
            #region 面向对象概念
            #region 面向对象
            /*对象是具有属性和操作的实体。
             * 类是对象的抽象，它为属于该类的全部对象提供了统一的抽象描述。
             * 类是对象的模板，对象是类的实例。
             * 面向对象三原则：封装，继承，多态。
             * 封装：指用一个框架将数据和代码组合在一起，形成一个对象。一般情况下数据都是被封装起来的，不能直接被外部访问。C#中的类是支持对象封装的工具，对象是封装的基本单元。
             * 继承：一种机制，可使新定义的派生类继承基类的特征和能力，而且可以对基类进行扩展和专用化。它可以提高软件模块的可复用性、可扩充性和软件开发效率。
             * 多态：指同一个消息或操作作用于不同的对象，产生不同的结果。它包括静态多态和动态多态。
             * ==============================================
             * 类的访问修饰符：
             * public，表明不限制对类的访问；
             * internal，同一程序集中的任何代码都可以访问该类，但其他程序集不可访问；
             * protected，表明这个类只能被这个类的成员或派生类成员访问；
             * private，表明这个类只能被本身的成员访问；
             * abstract，抽象类，该类含有抽象成员，不能被实例化，只能用作基类；
             * sealed，密封类，不能从这个类派生其他类，不允许被继承，不能同时为抽象类；
             * new，仅允许在嵌套类声明时使用，表明类中隐藏了由其基类中继承而来的、与基类同名的成员。
             * ==============================================
             * 类成员：
             * 一，在类中以类成员声明形式引入的成员；
             * 二，从基类继承来的成员。
             * 类成员包括常量、字段、方法、构造函数、属性、事件、索引等。
             * 类成员的访问修饰符：
             * public，公有成员，类的使用者可以从外部访问该成员；
             * protected，保护成员，可以被派生类访问，对于其他类隐藏；
             * internal，同一程序集中的代码可以访问，其他程序集不可访问；
             * protected internal，访问仅限于当前程序集或从包含类派生的类型；
             * private，私有成员，仅限于类中的成员访问，从类的外部不可访问，声明类成员不指定访问修饰符则默认为private。
             * 另外，加了readonly的字段表示只读不能写，该字段的赋值只能在声明的同时进行，或者在类的构造函数中赋值；加了volatile的字段是不调用lock语句跨多线程修改的字段。
             * ==============================================
             * 方法的返回类型默认为void。
             * 方法的访问修饰符：
             * new，在一个继承结构中，用于隐藏基类同名的方法；
             * public，表明不限制对方法的访问；
             * protected，表明该方法只能在类或派生类中被访问，不能在类外访问；
             * internal，表明该方法只能被处于同一程序集中的代码访问；
             * private，私有方法，表明这个方法只能在这个类中访问；
             * static，静态方法，说明该方法属于类本身，而不属于特定对象；
             * virtual，表示该方法是虚方法，可在派生类中重写；
             * abstract，抽象方法，该方法仅定义了方法的名称，没有具体的实现，包含这样的方法的类是抽象类，有待于派生类的实现；
             * override，表示该方法是将从基类继承的virtual方法的新实现；
             * sealed，表示这是一个密封方法，它必须同时包含override修饰符，以防它的派生类进一步重写该方法；
             * extern，表示该方法从外部实现。
             * ==============================================
             * 方法的参数：
             * 一，普通参数，形参前不加任何修饰的参数。若传递的是值类型的参数，则进行值传递，若是引用类型，则进行引用传递。
             * 二，引用类型参数(ref)，在调用方法时希望对形参值的改变能影响实参的结果，在值类型参数前加上ref(调用方法时也需要加ref)，表明实参与形参的传递是引用的，对形参的修改会影响实参的值。
             * 三，输出型参数(out)，是引用类型参数，与ref相似，不同的是ref要求变量在作为参数传递之前必须初始化，out无须初始化。当希望方法对多个值初始化时out参数很有用。
             * 四，数组型参数(params)，若希望传递任意个数的参数，params可以使用个数不定的参数(多个参数用逗号隔开，或用一个数组代替，或不传递任何参数)，而普通的数组参数必须传递一个数组。
             * 注意：一个方法只能声明一个params参数；params参数需要放在所有参数后面；params参数所定义的数组必须是一维数组；params参数不能同时与ref或out使用。*/
            #endregion
            #region 方法重载
            /*方法签名是由方法名称和一个参数列表（方法的参数顺序和类型）组成。
             * 在C#中同一个类中的两个或两个以上的方法可以有同一个名字，只要他们的参数声明不同即可，即方法的签名不同，这种情况称为方法重载。
             * 参数不同指参数的个数不同或参数的类型不同。
             * 方法重载是为了实现类中功能相似但需要不同参数的方法的方式。*/
            #endregion
            #region 构造函数，析构函数
            /*构造函数用于执行类实例的初始化。
             * 每个类都有构造函数，构造函数的名称与类名一致。
             * 字段可分为静态字段和实例字段，构造函数也有静态构造函数和实例构造函数。
             * 实例构造函数：
             * 1，修饰符：public,protected,internal,private和extern。前4个与前面介绍的含义一致，extern表该构造函数为外部构造函数，不提供任何实际的实现，函数体中仅由一个分号组成。一般情况下构造函数总是public类型，若为私有类型，表明类不能被实例化；
             * 2，标识符是构造函数名，与类名一致；
             * 3，构造函数中可以没有参数，也可以有一个或多个参数，一个类可以有多个构造函数重载；
             * 4，:base(params...)表调用基类中相应的构造函数；
             * 5，:this(params...)表调用该类本身所声明的其他构造函数；
             * 6，构造函数中既可以对静态字段赋值，也可以对非静态字段初始化；
             * 7，实例构造函数不能被继承，若一个类没有声明任何实例构造函数，系统会自动提供一个不带参数的默认构造函数。
             * ==============================================
             * 静态构造函数：用于初始化一些静态变量。
             * 注意：
             * 1，在创建此类的第一个实例或引用任何静态成员之前，由.NET自动调用，且静态构造函数没有访问修饰符；
             * 2，静态构造函数没有参数；
             * 3，一个类中只能有一个静态构造函数；
             * 4，静态构造函数只能运行一次；
             * 5，静态构造函数不能被继承；
             * 6，若没有写静态构造函数，而类中包含带有初始值设定的静态成员，则编译器会自动生成默认的静态构造函数；
             * 7，静态构造函数与无参的实例构造函数不冲突，可同时出现。
             * ==============================================
             * 析构函数：用于释放类的实例(释放所占内存)，没有参数与修饰符，不能被调用，名称与类名一致，使用时加前缀“～”加以区别。C#提供自动内存管理机制。*/
            #endregion
            #region 属性
            /*若希望其他类访问成员变量的值，就必须定义为public，但public变量可以被其他类任意读取和修改，不利于保护数据的安全。
             * 若仅用Set()和Get()方法来实现字段的读写，每次访问字段将会调用两个方法，这样很麻烦。
             * C#通过属性特性读取和写入字段，而不直接进行字段的读取和写入。
             * 属性是类内部封装性的体现。*/
            #endregion
            #region 继承、多态、虚方法
            /*继承：
             * 1，继承是可传递的；
             * 2，派生类应是对基类的扩展；
             * 3，类可以定义虚方法、虚属性等。
             * base关键字用于调用基类的方法。
             * 多态：
             * C#的多态包括两类：编译时的多态性和运行时的多态性。
             * 编译时的多态性是方法重载所实现的，运行时的多态性是通过虚方法来实现的。
             * 虚方法：
             * 若希望基类中的某个方法能够在派生类中进一步得到改进，就可以把这个方法定义为虚方法。
             * 定义虚方法要用到virtual关键字，在派生类中重写基类的方法要用override关键字。
             * 当程序调用某个虚方法，将调用最底层的派生方法，如果原始虚方法没有被重写，最底层的派生方法就是原始虚方法，否则最底层的派生方法就是相应对象中的重写方法。*/
            #endregion
            #region 抽象方法
            /*抽象方法：创建一个类时，有时需要让该类包含一些特殊方法，该类对这些方法不提供实现，该类的派生类必须实现这些方法，这些方法称为抽象方法（没有被实现的空方法）。
             * 抽象类：能包含抽象成员的类称为抽象类，包含抽象成员的类一定是抽象类，抽象类也可包含非抽象成员。
             * 抽象类不能直接实例化，也不能被密封，只能作为其他类的基类。
             * 抽象方法不能加{}。定义抽象类使用abstract，继承抽象类的类必须使用override重写抽象方法。*/
            #endregion
            #region 接口
            /* 接口是引用类型。
             * 一个接口的定义相当于一个约定，实现某接口的类或结构必须遵循该接口定义的约定。
             * 在某种程度上，接口像一个抽象类，可以定义接口的方法、属性、索引器和事件等，但是接口不提供成员的实现，仅指定实现该接口的类或结构所必须提供的成员，继承接口的类必须提供接口成员的实现。
             * 类无法实现多继承，即一个类只能继承于一个基类。C#中允许一个接口继承多个接口，当继承多个接口时，各个接口之间用","隔开。
             * 接口的访问修饰符：new,public,protected,internal和private。new仅允许在类中定义的接口内使用，它指定接口隐藏同名的继承成员。其他修饰符与前面介绍的相同。
             * 接口名称通常以I开头，要实现该接口，必须有类继承该接口。
             * 接口的成员可以使方法、属性、事件和索引器，但不能包含常数、字段、运算符、实例构造函数、析构函数或类型，也不能包括任何种类的静态成员。
             * 所有接口成员都隐式地具有public访问属性。
             * 在接口成员声明中不能包含任何修饰符，也就是说不能用abstract,public,protected,internal,virtual,override或static来声明接口成员。
             * 快捷操作：当输入完要继承的接口名字后，用光标点击接口名，这时出现一根短横线，短横线变为一个小方框，点击小方框，选择实现接口，即可快速生成代码。*/
            #endregion
            #region 显式接口成员
            /* 当在具体的实现类中指明接口成员所在的接口时，这种称为显式接口成员。
             * 显示接口成员没有public修饰符，这是因为这些方法有着双重身份。
             * 当在一个类使用显示接口成员时，该方法被认为是私有方法，因此不能用类的实例来调用它，但是当类的引用类型转换为接口时，接口中定义的方法就可以被调用，这时它又是一个公共方法。接口成员只能通过接口来调用。
             * ==============================================
             * 有时需要用接口继承而不是类继承的原因：
             * 1，有时程序需要许多彼此无关的对象来提供特定功能；
             * 2，接口可以在基类不同的类之间实现多态性；
             * 3，接口比基类灵活，可以定义实现多个接口的单个实现；
             * 4，若不需要从基类继承实现，接口是更好的选择；
             * 5，若不能使用类继承，可以使用接口。*/
            #endregion
            #region 委托
            /*C#委托和C/C++函数指针类似，与之不同的是，委托是引用类型，是面向对象的，在使用时要先定义后实例化，然后再调用。
             * 委托可以在运行时间接调用一个或多个方法。
             * 在声明委托类型或基于委托类型的变量时并不指定委托将要调用哪些方法，这一操作是在创建委托的实例时进行的，并且在运行时还可以将一个或多个方法与委托动态关联。
             * delegate委托关键字可以理解为定义了该委托类型所能封装的方法的原型（指示其参数和返回类型）。
             * 在判断一个方法与一个委托是否兼容时，主要看：
             * 1，参数列表(个数、顺序、类型、修饰符要一致)；
             * 2，返回值类型要相同。
             * 创建委托的方法：
             * 1，声明一个委托；
             * 2，写一个与委托有一样签名的方法；
             * 3，通过委托调用方法。
             * ==============================================
             * 一个委托只与一个方法关联，称为单路广播委托。
             * 一个委托与多个方法关联，称为多路广播委托。
             * 多路方波委托通过使用“+”把方法加入执行队列。只调用一个委托就可以完成对多个方法的调用，并且传递给委托的参数会传递到委托所包含的每一个方法。
             * 一般情况下多路委托的返回值类型为void，因为一个委托只能有一个返回值。
             * 如果一个返回值不为空的委托封装了多个方法，只能得到最后封装的方法的返回值。
             * 如果想通过委托返回多个值，最好使用委托数组，让每个委托封装一个方法，各自返回一个值。*/
            #endregion
            #region 事件
            /*事件主要为类和类的实例定义发出通知的能力，从而将事件和可执行代码捆绑在一起。
             * 事件最常见于窗体编程，如点击按钮事件、鼠标移动事件等。
             * C#事件是按照“发布-预定”的方式工作。
             * 先在一个类中公布事件，然后就可以在任意数量的类中对事件进行预订。
             * 创建并使用自定义事件：
             * 1，在类中声明事件并确定将要使用的委托和参数；
             * 2，定义在触发事件时要调用的委托；
             * 3，设计事件参数类，该参数类的实例会将信息传递给被调用的方法。如果使用内置的EventArgs对象和EventHandler委托，则可以不执行该步骤。
             * 可以通过使用继承自EventArgs的类将参数传递给处理事件的方法。
             * 换句话说，每当想要将来自事件发布者的特定数据传递给预定者方法，就必须使用继承自EventArgs基类的自定义事件参数类。*/
            #endregion
            #endregion
            Robot.Say();
            Robot.Say("Hello world.");
            Console.WriteLine(Robot.GetRoot());
            int Sum = Robot.Sum(new int[] { 3, 4, 3 });
            Console.WriteLine("总和：{0}", Sum);
            string s1 = "Sophie";
            StringBuilder s2 = new StringBuilder("Sophie");
            Robot.Append123(s1);
            Console.WriteLine(s1);
            Robot.Append123(ref s1);
            Console.WriteLine(s1);
            Robot.Append123(s2);
            Console.WriteLine(s2);
            int[] ints = new int[] { 20, 10, 300, 50, 5 };
            Console.WriteLine("{0}, {1}", ints[0], ints[1]);
            Robot.ModifyArray(ints);//测试数组是否为引用类型
            Console.WriteLine("{0}, {1}", ints[0], ints[1]);
            int Max, Min;
            Robot.MinMax(ints, out Min, out Max);
            Console.WriteLine("Min: {0}, Max: {1}", Min, Max);
            Robot r1 = new Robot(10, "R1", "");
            r1.PrintRoot();
            Console.WriteLine("ID: {0}, Name: {1}, Gender: {2}", r1.ID, r1.Name, r1.Gender);
            r1.Introduce();
            Robot2 r2 = new Robot2(10, "R2", "Male");
            r2.PrintRoot();
            Console.WriteLine("ID: {0}, Name: {1}, Gender: {2}", r2.ID, r2.Name, r2.Gender);
            r2.Introduce();
            r2[0] = 5;
            r2[1] = 3;
            r2[2] = 2;
            Console.WriteLine("Indexes: {0}, {1}, {2}", r2[0], r2[1], r2[2]);
            //=========================
            new Liquid().Change();
            new Gas().Change();
            Console.WriteLine();
            //====自定义泛型类
            new GenericParadigmA<string>("Sophie");
            new GenericParadigmA<bool>(false);
            new GenericParadigmA<StringBuilder>(new StringBuilder("Rebecca"));
            new GenericParadigmB<int, string[]>(200, new string[] { "Sophie", "Rebecca" });
        }
        public static void Test4()//引用类型：接口、委托、事件
        {
            //=========================接口
            CDPlayer player1 = new CDPlayer();
            player1.NextTrack();
            player1.Open();
            player1.CurrentTrack();
            player1.NextTrack();
            player1.NextTrack();
            player1.CurrentTrack();
            player1.PreviousTrack();
            player1.CurrentTrack();
            player1.Close();
            Console.WriteLine();
            //=========================显式接口
            Computer cpt = new Computer();
            ISystem sys = cpt;
            sys.Run();
            sys.Calculate();
            ICalculator cal = cpt;
            cal.Calculate();
            Console.WriteLine();
            //=========================委托
            Say say1 = ASay;
            Say say2 = BSay;
            Say say3 = say1 + say2;
            say1("Hi");
            say2("Hi");
            say3("Hello");
            Console.WriteLine();
            //=========================事件
            Button button1 = new Button("Button1");
            Button button2 = new Button("Button2");
            button1.Click += Button1_Click;//订阅
            button2.Click += Button1_Click;//订阅
            //event1.myEvent += new MyEvent1.Handler(PrintParam);//订阅
            button1.OnClick();
            button2.OnClick();
            //总过程：订阅，触发。事件调用委托，委托调用方法（方法参数是定义好的对象）。
            button1.Click -= Button1_Click;//卸载订阅
            button1.OnClick();//已无效果
        }
        static void ASay(string Words)
        {
            Console.WriteLine("A said, \"{0}.\"", Words);
        }
        static void BSay(string Words)
        {
            Console.WriteLine("B said, \"{0}.\"", Words);
        }
        static void Button1_Click(object obj, MyParam e)
        {
            Console.WriteLine("Object: {0}. Parameter: {1}", (obj as Button).Name, e.Param1);
            Console.WriteLine("You have just clicked me.");
        }
    }
    enum Day : int
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday = 100, Sunday
        /*枚举类型是一组命名的常量集合，其中每一个元素称为枚举成员列表。
         * 枚举成员的类型可以是long, int, short或byte等整数类型。
         * 如果一个变量有几种可能的值，就可以定义枚举类型，声明一个枚举时，要指定该枚举可以包含的一组可接受的实例值。
         * 枚举不能定义自己的方法，不能实现接口，不能定义属性或事件。声明enum时不写类型默认为int类型，不能为非整数类型*/
    }
    struct Student
    {
        public Student(ushort ID, string Name, byte Age)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
        }
        public ushort ID;
        public string Name;
        public byte Age;
        public void SayHi()
        {
            Console.WriteLine("Hi, I'm {0}.", Name);
        }
        /*结构类型：用来组合一些相关信息，形成一种新的复合数据类型。
         * 其元素可由不同的值类型变量构成，这些变量称为结构的成员。
         * 其成员没有类型限制，可以是任何值类型，甚至包括结构类型本身。
         * 它可以包含字段、方法、属性、事件、索引等成员，结构也可以实现多个接口。
         * 它在主函数外声明。*/
    }
    class Robot//含静态成员（可静态调用），也含非静态成员（需实例化后调用）
    {
        private static string Root = Environment.CurrentDirectory;//静态字段
        int? id;//实例字段（默认private）
        string gender;
        static Robot()//静态构造函数
        {
            Console.WriteLine("静态构造函数！只执行一次。");
        }
        public Robot()//实例构造函数
        {
            Console.WriteLine("实例构造函数！");
        }
        public Robot(int ID)//实例构造函数重载1
            : this()//调用无参构造函数
        {
            this.ID = ID;
        }
        public Robot(int ID, string Name)//实例构造函数重载2
            : this(ID)//调用有参构造函数1
        {
            this.Name = Name;
            Console.WriteLine("{0} has been created!", Name);
        }
        public Robot(int ID, string Name, string Gender)//实例构造函数重载3
            : this(ID, Name)//调用有参构造函数2
        {
            this.Gender = Gender;
        }
        public int? ID//实例属性（绑定字段）
        {
            set { id = value; }//set块，用来设置私有字段的值
            get { return id; }//get块，用来返回私有字段的值
        }
        public string Name { set; get; }//实例属性（无绑定字段）
        public string Gender//实例属性（绑定字段，有判断）
        {
            get { return gender; }
            set
            {
                if (value == "Male" || value == "Female")
                    gender = value;
                else
                {
                    Console.WriteLine("Wrong gender!");
                    gender = "Default";
                }
            }
        }
        int[] data = new int[3];
        public int this[int index]//索引器
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
        public static string GetRoot()//静态方法：无参数，有返回值
        {
            return Root;
        }
        //在方法前输入三个斜杠可自动生成注释结构
        /// <summary>
        /// 输出根目录路径。
        /// </summary>
        public void PrintRoot()//实例方法：无参数，无返回值
        {
            Console.WriteLine(Root);
        }
        void Print()//私有方法(可加private)，不可在外部或子类调用
        {
            Console.WriteLine("Print");
        }
        protected void Print2()//受保护方法，不可在外部调用，但可在子类调用
        {
            Console.WriteLine("Print2");
        }
        public virtual void Introduce()//虚方法（可由子类重写）
        {
            Console.WriteLine("Hello, I'm {0}, I'm {1}.", Name, gender);
        }
        public static void Say()//静态方法：无参数，无返回值
        {
            Console.WriteLine("\"Hi.\"");
        }
        public static void Say(string Words)//重载静态方法1：无参数，无返回值
        {
            Console.WriteLine("\"{0}\"", Words);
        }
        public static void Say(string Words1, string Words2 = "")//重载静态方法2：有参数（带默认值），有默认值，无返回值
        {
            Console.WriteLine("{0}... {1}", Words1, Words2);
        }
        public static void Append123(string Text)//普通形式参数
        {
            Text += "123";
        }
        public static void Append123(ref string Text)//重载1：ref 引用参数
        {
            Text += "123";
        }
        public static void Append123(StringBuilder Text)//重载2：对象引用参数
        {
            Text.Append("123");
        }
        public static int Sum(params int[] Array)//数组参数，有返回值
        {
            int Sum = 0;
            foreach (int each in Array)
                Sum += each;
            return Sum;
        }
        public static void ModifyArray(int[] Array)//测试数组是否是引用类型
        {
            Array[1] = 100;
        }
        public static void MinMax(int[] Array, out int Min, out int Max)//out型输出参数
        {
            Max = Array[0];
            Min = Array[0];
            foreach (int each in Array)
            {
                if (Max < each) Max = each;
                if (Min > each) Min = each;
            }
        }
    }
    class Robot2 : Robot//子类，调用基类构造方法，重写基类虚方法
    {
        string gender = "NoValue";//与基类同名的字段，不冲突
        public Robot2() : base() { }//调用基类构造函数
        public Robot2(int ID) : base(ID) { }//调用基类有参构造函数
        public Robot2(int ID, string Name) : base(ID, Name) { }
        public Robot2(int ID, string Name, string Gender) : base(ID, Name, Gender) { }
        public override void Introduce()//重写基类虚方法
        {
            base.Introduce();//调用基类方法
            Console.WriteLine("That's all.");
        }
        new public void PrintRoot()//用new隐藏继承的成员
        {
            Console.WriteLine("What is root?");
        }
        public void Test()
        {
            Console.WriteLine(gender);
        }
    }
    abstract class Existence//抽象类：存在
    {
        public abstract void Change();
    }
    class Gas : Existence//抽象子类：气体
    {
        public override void Change()
        {
            Console.WriteLine("Gas changes.");
        }
    }
    class Liquid : Existence//抽象子类：固体
    {
        public override void Change()
        {
            Console.WriteLine("Liquid changes.");
        }
    }
    class GenericParadigmA<T>//泛型类
    {
        T param;
        public GenericParadigmA(T param)
        {
            this.param = param;
            Console.WriteLine("{0}, {1}", this.param, this.param.GetType());
        }
    }
    class GenericParadigmB<T, R>//泛型类2
    {
        T param;
        R param2;
        public GenericParadigmB(T param, R param2)
        {
            this.param = param;
            this.param2 = param2;
            Console.WriteLine("{0}, {1}", this.param, this.param.GetType());
            Console.WriteLine("{0}, {1}", this.param2, this.param2.GetType());
        }
    }
    public interface ICDPlayer//CDPlayer接口
    {
        void Open();
        void Close();
        void PreviousTrack();
        void NextTrack();
        void CurrentTrack();
    }
    public class CDPlayer : ICDPlayer//CDPlayer
    {
        int currentTrack = 1;
        bool isOpen = false;
        public void Open()
        {
            isOpen = true;
            Console.WriteLine("CD player is open.");
        }
        public void CurrentTrack()
        {
            if (isOpen) Console.WriteLine("Current track: {0}.", currentTrack);
            else Warn();
        }
        public void Close()
        {
            if (isOpen) Console.WriteLine("CD player is closed.");
            else Warn();
        }
        public void PreviousTrack()
        {
            if (isOpen)
            {
                Console.WriteLine("Previous track.");
                if (currentTrack > 1) currentTrack--;
            }
            else Warn();
        }
        public void NextTrack()
        {
            if (isOpen)
            {
                Console.WriteLine("Next track.");
                currentTrack++;
            }
            else Warn();
        }
        void Warn()
        {
            Console.WriteLine("CD player is not open yet.");
        }
    }
    interface ICalculator//计算器接口
    {
        void Calculate();
    }
    interface ISystem : ICalculator//系统接口，继承计算器接口
    {
        void Run();
    }
    class Computer : ISystem//电脑接口，继承系统接口
    {
        void ISystem.Run()//定义系统接口方法
        {
            Console.WriteLine("System runs.");
        }
        void ICalculator.Calculate()//定义计算器接口方法
        {
            Console.WriteLine("Calculator calculates.");
        }
    }
    delegate void Say(string sentence);//委托    
    class Button//模拟按钮控件类
    {
        MyParam param = new MyParam("ButtonEvent");
        public Button(string name)
        {
            Name = name;
        }
        public string Name { set; get; }
        public delegate void Handler(object obj, MyParam e);
        public event Handler Click;
        public void OnClick()
        {
            if (Click != null) Click(this, param);
            else Console.WriteLine("There is no method.");
        }
    }
    public class MyParam : EventArgs//我的参数：可不继承
    {
        public string Param1 { set; get; }
        public string Param2 { set; get; }
        public MyParam(string param)
        {
            this.Param1 = param;
        }
        public MyParam(string param1, string param2)
        {
            this.Param1 = param1;
            this.Param2 = param2;
        }
    }
}
