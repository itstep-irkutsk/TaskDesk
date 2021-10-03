namespace DataBaseLib
{
    public class DBTask
    {
        public int _id;
        public string _name;
        public string _description;
        public string _creation_date;
        public string _execution_date;
        public int _status;
        public int _priority;
        public DBTask() {}
    }
    public class DBPriority
    {
        public int _id;
        public string _priority;
    }
    public class DBStatus
    {
        public int _id;
        public string _status;
    }


}