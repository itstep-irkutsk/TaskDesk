using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib.BaseClasses
{
    public abstract class Logger : ILogger
    {
        private static string _path { get; set; }
        public bool MakeLog(string taskInfo)
        {
            return true;
        }
    }
}
