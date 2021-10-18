using System.Collections.Generic;

namespace DataBaseLib
{
    public class TaskData
    {
        static public Dictionary<int, string>statusDic;
        static public Dictionary<int, string>priorityDic;
        
        public int id {get; set;}
        public string name {get; set;}
        public string description {get; set;}
        public string creation_date {get; set;}
        public string execution_date {get; set;}
        public int status {get; set;}
        public int priority {get; set;}
        public bool is_deleted {get; set;}

        public TaskData() 
        {
            var taskData = new DataBase();
            /*statusDic = taskData.ReqvestDict(taskData.namesBdAndTables.nameTableStatus);
            priorityDic = taskData.ReqvestDict(taskData.namesBdAndTables.nameTablePriority);*//**/
        }
    }
}