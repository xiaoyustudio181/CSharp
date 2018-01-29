using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    public class Json
    {
        public static void Test1()
        {
            //using System.Runtime.Serialization.Json;//手动添加引用时搜索Serialization
            Person person1 = new Person(11, "Sophie", "Female");
            Person person2 = new Person(15, "Sienna", "Female");
            string result = Serialize(person1);//序列化一个对象
            Console.WriteLine(result);
            result = Serialize(person2);//序列化一个对象
            Console.WriteLine(result);
            Person temp = Deserialize<Person>(result);//反序列化
            Console.WriteLine("ID: {0}, Name: {1}, Gender: {2}\n", temp.ID, temp.Name, temp.Gender);

            List<Person> persons = new List<Person>();
            persons.Add(person1);
            persons.Add(person2);
            result = Serialize(persons);//序列化多个对象
            Console.WriteLine(result);

            List<Person> persons2 = Deserialize<List<Person>>(result);//反序列化
            Console.WriteLine("ID: {0}, Name: {1}, Gender: {2}", persons2[1].ID, persons2[1].Name, persons2[1].Gender);
        }
        static string Serialize<T>(T obj)//Json序列化
        {
            MemoryStream stream = new MemoryStream();
            new DataContractJsonSerializer(typeof(T)).WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return result;
        }
        static T Deserialize<T>(string json)//反序列化Json
        {
            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json.ToCharArray()));
            T obj = (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            stream.Close();
            return obj;
        }
    }
    public class Person//JSON序列化用
    {
        public Person(int id, string name, string gender)
        {
            this.ID = id;
            this.Name = name;
            this.Gender = gender;
        }
        public Person() { }//若要JSON化，此类不能有构造函数；如果要使用构造函数，则需添加一个空构造函数。
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public void Talk() { Console.WriteLine(); }
    }

}
