using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace appCommunication
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<INotepadReader, NotepadReader>()
            .AddTransient<ITemplateGenerator, TemplateGenerator>()
            .BuildServiceProvider();

            try
            {
                var fileSavePath = ""; //by default, save file under current folder
                var notepadReader = serviceProvider.GetService<INotepadReader>();
                var content = notepadReader.ReadContentFromNotepad();
                var templateGenerator = serviceProvider.GetService<ITemplateGenerator>();
                templateGenerator.GenerateTemplateFile(content, fileSavePath);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("IO exception Happened " + ioEx);
            }
            catch(Exception ex)
            {
                Console.WriteLine("expcetion happened " + ex);
            }
        }
    }
}
