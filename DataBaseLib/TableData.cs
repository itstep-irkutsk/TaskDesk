using System.Net.NetworkInformation;

namespace DataBaseLib
{
    public class TableData
    {
        public int id;
        public string name;
        public string description;
        public string creation_date;
        public string execution_date;
        public Status status;
        public Priority priority;
    }
}