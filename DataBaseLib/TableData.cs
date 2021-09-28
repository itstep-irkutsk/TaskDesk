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
        public bool is_deleted;

        public TableData()
        {
            status = Status.NotTheWork;
            priority = Priority.Low;
            is_deleted = false;
        }
    }
}