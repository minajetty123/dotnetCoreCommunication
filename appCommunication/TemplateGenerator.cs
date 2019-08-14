using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace appCommunication
{
    class TemplateGenerator : ITemplateGenerator
    {
        public void GenerateTemplateFile(string content, string path)
        {
            string[] nameList = content.Split("\r");
            for(int i=0;i< nameList.Length;i++)
            {
                var authorName = nameList[i].Trim(new Char[] { '\n' });
                SyncWriteToFile(path, authorName);
            }
        }

        private void SyncWriteToFile(string filePath, string name)
        {
            if (name == "")
                return;
            var templateGenenated = String.Format("hello, good morning {0},\r\nit is so glad to meet you, hope to receive your reply soon\r\nbest regards\r\nxxx",name);
            var path = filePath + name + "_template.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            System.IO.File.WriteAllText(path, templateGenenated);
            Console.WriteLine("Finished the file " + path);
        }
    }
}
