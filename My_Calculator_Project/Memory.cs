using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Calculator_Project
{

    public class Memory
    {
        public struct StructMemory
        {
            public string example, result;
            public StructMemory(string _example, string _result)
            {
                example = _example;
                result = _result;
            }
            public string Example { get { return example; } }
            public string Result { get { return result; } }
        }

        public List<StructMemory> list_memory = new();
        public Memory() { LoadMemory(); }






        public void AddMemory(string example, string result)
        {
            list_memory.Add(new StructMemory(example, result));
            SaveMemory();
        }
        public void RemoveMemory(StructMemory _element)
        {
            list_memory.Remove(_element);
            SaveMemory();
        }
        public string GetExample(StructMemory _element)
        {
            return _element.Example;
        }
        public string GetResult(StructMemory _element)
        {
            return _element.Result;
        }





        public string name_file = "ThisIsFile.txt";
        public void SaveMemory()
        {
            if(File.Exists(name_file))
            {
                StreamWriter file_write = new(name_file);
                foreach (StructMemory element in list_memory)
                {
                    file_write.WriteLine(element.Example + " = " + element.Result);
                }
                file_write.Close();
            }
        }
        public void LoadMemory()
        {
            string? arr_str;
            if(File.Exists(name_file))
            {
                StreamReader file_read = new(name_file);
                while ((arr_str = file_read.ReadLine()) != null)
                {
                    list_memory.Add(new StructMemory(arr_str.Split(" = ")[0], arr_str.Split(" = ")[1]));
                }
                file_read.Close();
            }
        }
    }
}
