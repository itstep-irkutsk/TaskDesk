using System.Net.NetworkInformation;

namespace DataBaseLib
{
    public class DBTasks
    {
        public int id;
        public string name;
        public string description;
        public string creation_date;
        public string execution_date;
        public int status;
        public Priority priority;
        public bool is_deleted;

        public DBTasks()
        {
            //status = Status.NotTheWork;
            priority = Priority.Low;
            is_deleted = false;
        }

        //public List<DBData> Tasks = new();

        //public List<String> Priority = new();

        //public Lsit<String> Status = new();
    }


}