using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib.BaseClasses
{
    public class Logger : ILogger
    {
        private static string _path { get; set; }
        public void MakeLog(string taskInfo)
        {
            taskInfo = $"{DateTime.Today} : {taskInfo}";
            _path = @"\Logs";
            DirectoryInfo dirInfo = new DirectoryInfo(_path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            using FileStream fstream = new FileStream($@"{_path}\logs.txt", FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes(taskInfo);
            fstream.Write(array, 0, array.Length);
        }
    }
}
