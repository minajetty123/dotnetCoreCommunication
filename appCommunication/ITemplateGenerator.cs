using System;
using System.Collections.Generic;
using System.Text;

namespace appCommunication
{
    interface ITemplateGenerator
    {
        void GenerateTemplateFile(string content, string path);
    }
}
